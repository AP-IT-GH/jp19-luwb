using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Models;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // Het configureren van MQTT verbinding via file
            SetupMQTT();
            // Aparte thread starten die de data zal updaten indien er iets binnenkomt van data via MQTT
            new Thread(delegate ()
            {
                UpdateDataInBackground();
            }).Start();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfire(configuration => {
                configuration.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext context)
        {
            // Vaste measurement per anchor aanmaken indien de db leeg is
            DatabaseInitialiser.Initialize(context);

            app.UseHangfireServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                         .AllowAnyMethod());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // Variabelen voor MQTT
        string connectionString = "broker.mqttdashboard.com";
        MqttClient client;
        string[] dataArray;
        string data;
        string topic = "LUWB/TAG5";
        string payload;

        void SetupMQTT()
        {
            // ERROR BIJ AZURE 
            // Uiltezen MqttSetup.txt file
            /* using (StreamReader sr = new StreamReader("Setup/MqttSetup.txt"))
            {
                // Indien er een lijn leesbaar is in de file, wordt deze uitgelezen
                while ((data = sr.ReadLine()) != null)
                {
                    // Wat er zou uitgelezen moeten worden
                    // {host};{topic}
                    dataArray = data.Split(';');
                    connectionString = dataArray[0];
                    topic = dataArray[1];
                }
            }*/
            // Verbinding maken met MQTT Broker
            ConnectToBroker();

        }

        // Wordt getriggered wanneer er een message gepublished wordt
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // Opslagen payload van MQTT bericht
            payload = Encoding.UTF8.GetString(e.Message);
        }

        private void ConnectToBroker()
        {
            // Aanmaken MqttClient object
            client = new MqttClient(connectionString);
            try
            {
                // Verbinding maken MQTT broker
                client.Connect(Guid.NewGuid().ToString());
            }
            catch
            {
                // Indien de connectie niet mogelijk is, probeer opnieuw
                ConnectToBroker();
            }
            // Subscriben op topic
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }

        public void UpdateDataInBackground()
        {
            // Kijken of er data is, indien ja update deze
            while (true)
                if (payload != null)
                    SaveMeasurementInDB(payload);
        }

        private void SaveMeasurementInDB(string payload/*,DatabaseContext context*/)
        {
            // Wat er binnenkomt via MQTT
            // TAG{nr};ANCHOR{nr};{distance};{unix_timestamp}
            // TAG5;ANCHOR1;-4;1557475973
            string[] data = payload.Split(';');

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            // Aanmaken nieuw databasecontext object
            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                // Verglijken mac van de anchors en opslaan in measure indien ze overeenkomen
                try
                {
                    Measurement measure = context.Measurements.Where(a => a.Mac_Anchor == data[1]).Where(a => a.Mac_Tag == data[0]).FirstOrDefault();
                    if (measure != null)
                    {
                        // Opslaan van afstand en timestamp
                        measure.Distance = int.Parse(data[2]);
                        measure.Unix_Timestamp = data[3];
                    }
                    // Indien de mac anchor niet gevonden wordt, word er een nieuw object aangemaakt
                    else
                    {
                        context.Measurements.Add(
                            new Measurement()
                            {
                                Mac_Tag = data[0],
                                Mac_Anchor = data[1],
                                Distance = int.Parse(data[2]),
                                Unix_Timestamp = data[3]
                            }
                        );
                    }
                    // Alle measurements zoeken van de meegegeven mac tag
                    var measurements = context.Measurements.Where(a => a.Mac_Tag == data[0]).ToList();
                    string firstMeasurementTimestamp = measurements[0].Unix_Timestamp;
                    bool checkAllMeasurements = false;
                    // Controleren of alle measurements van hetzelfde tijdstip zijn
                    foreach (var measurement in measurements)
                    {
                        if (measurement.Unix_Timestamp != firstMeasurementTimestamp)
                            checkAllMeasurements = true;
                        if (checkAllMeasurements == true) break;
                    }
                    // Indien alle measurements van hetzelfde tijdstip zijn, wordt de locatie van de tag berekend
                    List<Data> dataList = new List<Data>();
                    if (!checkAllMeasurements)
                    {
                        foreach (var measurement in measurements)
                        {
                            Anchor anchor = context.Anchors.Where(a => a.Mac == measurement.Mac_Anchor).LastOrDefault();
                            if (anchor != null)
                                dataList.Add(new Data { Distance = measurement.Distance, X_Pos = anchor.XPos, Y_Pos = anchor.YPos });
                        }
                        // Berekenen van de locatie van de tag
                        double[] pos = Algorithm.Algorithm.Calculate(dataList);
                        // Opslaan van de locatie van de tag
                        Tag tag = context.Tags.Where(a => a.Mac == data[0]).LastOrDefault();
                        if (tag != null)
                        {
                            tag.XPos = Convert.ToInt32(pos[0]);
                            tag.YPos = Convert.ToInt32(pos[1]);
                            context.Tags.Update(tag);
                        }
                    }
                    context.SaveChanges();
                }
                catch { }
                // Opslaan van veranderingen

            }
        }
    }
}
