﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Threading;
using System.Text;
using Hangfire;
using System.IO;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;

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

            services.AddHangfire( configuration => {
                configuration.UseSqlServerStorage("Server=(localdb)\\mssqllocaldb; Database = LibraryDB");
            });
          
            services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Automatically perform database migration
            services.BuildServiceProvider().GetService<DatabaseContext>().Database.Migrate();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options => {
                options.AddPolicy("AllowAllMethods",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DatabaseContext context)
        {
            app.UseCors("AllowAllMethods");
            app.UseHangfireServer();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // Vaste measurement per anchor aanmaken indien de db leeg is
            DatabaseInitialiser.Initialize(context);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        // Variabelen voor MQTT
        string connectionString;
        MqttClient client;
        string[] dataArray;
        string data;
        string topic;
        string payload;

        void SetupMQTT()
        {
            // Uiltezen MqttSetup.txt file
            using (StreamReader sr = new StreamReader("Setup/MqttSetup.txt"))
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
            }
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
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LibraryDB");
            // Aanmaken nieuw databasecontext object
            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                // Verglijken mac van de anchors en opslaan in measure indien ze overeenkomen
                Measurement measure = context.Measurements.Where(a => a.Mac_Anchor == data[1]).FirstOrDefault();
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
                // Opslaan van veranderingen
                context.SaveChanges();
            }

        }
    }
}
