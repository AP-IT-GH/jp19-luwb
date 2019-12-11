using System.Linq;

namespace Server.Models
{
    public class DatabaseInitialiser
    {
        public static void Initialize(DatabaseContext context)
        {
            //Create the db if not yet exists
            context.Database.EnsureCreated();

            //Are there already books present ?
            if (context.Measurements.Count() == 0)
            {

                var measure1 = new Measurement()
                {
                    Mac_Anchor = "ANCHOR1",
                    Mac_Tag = "TAG5",
                    Unix_Timestamp = "1570293770",
                    Distance = 100
                };
                var measure2 = new Measurement()
                {
                    Mac_Anchor = "ANCHOR2",
                    Mac_Tag = "TAG5",
                    Unix_Timestamp = "1570293770",
                    Distance = 200
                };

                var measure3 = new Measurement()
                {
                    Mac_Anchor = "ANCHOR3",
                    Mac_Tag = "TAG5",
                    Unix_Timestamp = "1570293770",
                    Distance = 300
                };

                context.Measurements.Add(measure1);
                context.Measurements.Add(measure2);
                context.Measurements.Add(measure3);
            }
            if (context.Tags.Count() == 0)
            {
                Tag[] tags = {
                    new Tag()
                    {
                        ProjecType = projectTypes.APLokalisatie,
                        Mac = "TAG5",
                        Description = "Dit is een test",
                        XPos = 0,
                        YPos = 0,
                        ZPos = 0
                    },
                    new Tag()
                    {
                        ProjecType = projectTypes.APLokalisatie, 
                        Mac = "TAG6",
                        Description = "Dit is een test",
                        XPos = 0,
                        YPos = 0,
                        ZPos = 0
                    },
                    new Tag()
                    {
                        ProjecType = projectTypes.Pozyx,
                        Mac = "TAG1",
                        Description = "Pozyx Tag",
                        XPos = 3500,
                        YPos = 1000,
                        ZPos = 0
                    }
                };
                foreach (var item in tags)
                {
                    context.Tags.Add(item);
                }
                Anchor[] anchors = {
                    new Anchor()
                    {
                        Mac = "Anchor1",
                        Description = "Anchor 1 POZYX",
                        XPos = 711,
                        YPos = 0,
                        ZPos = 1954
                    },
                    new Anchor()
                    {
                        Mac = "Anchor2",
                        Description = "Anchor 2 POZYX",
                        XPos = 10,
                        YPos = 5998,
                        ZPos = 2504
                    },
                    new Anchor()
                    {
                        Mac = "Anchor3",
                        Description = "Anchor 3 POZYX",
                        XPos = 4398,
                        YPos = 5930,
                        ZPos = 2206
                    },
                    new Anchor()
                    {
                        Mac = "Anchor4",
                        Description = "Anchor 4 POZYX",
                        XPos = 4153,
                        YPos = 0,
                        ZPos = 2524
                    }
                };
                foreach (var item in anchors)
                {
                    context.Anchors.Add(item);
                }
            }
            context.SaveChanges();
        }
    }
}
