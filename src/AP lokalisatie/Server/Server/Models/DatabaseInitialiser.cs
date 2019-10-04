using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class DatabaseInitialiser
    {
        public static void Initialize(DatabaseContext context)
        {
            //Create the db if not yet exists
            context.Database.EnsureCreated();

            //Are there already books present ?
            if (!context.Measurements.Any())
            {

                var measure1 = new Measurement()
                {
                    Mac_Anchor = "ANCHOR1",
                    Mac_Tag = "TAG5"
                };
                var measure2 = new Measurement()
                {
                    Mac_Anchor = "ANCHOR2",
                    Mac_Tag = "TAG5"
                };

                var measure3 = new Measurement()
                {
                    Mac_Anchor = "ANCHOR3",
                    Mac_Tag = "TAG5"
                };

                context.Measurements.Add(measure1);
                context.Measurements.Add(measure2);
                context.Measurements.Add(measure3);
                context.SaveChanges();
            }
            if (!context.Tags.Any())
            {
                Tag[] tags = {
                    new Tag()
                    {
                        Mac = "1.1.1.1",
                        Description = "Dit is een test",
                        XPos = 50,
                        YPos = 73
                    },
                    new Tag()
                    {
                         Mac = "2.2.2.2",
                        Description = "Dit is een test",
                        XPos = 32,
                        YPos = 40
                    }
                };
                foreach (var item in tags)
                {
                    context.Tags.Add(item);
                }
                context.SaveChanges();
                Anchor[] anchors = {
                    new Anchor()
                    {
                        Mac = "192.168.1.3",
                        Description = "Dit is een test",
                        XPos = 100,
                        YPos = 100
                    },
                    new Anchor()
                    {
                        Mac = "192.168.1.4",
                        Description = "Dit is een test",
                        XPos = 200,
                        YPos = 200
                    }
                };
                foreach (var item in anchors)
                {
                    context.Anchors.Add(item);
                }
                context.SaveChanges();
            }
        }
    }
}
