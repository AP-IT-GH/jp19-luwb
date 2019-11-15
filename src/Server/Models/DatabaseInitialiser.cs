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
                        Mac = "TAG5",
                        Description = "Dit is een test",
                        XPos = 0,
                        YPos = 0
                    },
                    new Tag()
                    {
                         Mac = "TAG6",
                         Description = "Dit is een test",
                         XPos = 0,
                         YPos = 0
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
                        Description = "Dit is een test",
                        XPos = 20,
                        YPos = 20
                    },
                    new Anchor()
                    {
                        Mac = "Anchor2",
                        Description = "Dit is een test",
                        XPos = 67,
                        YPos = 27
                    },
                    new Anchor()
                    {
                        Mac = "Anchor3",
                        Description = "Dit is een test",
                        XPos = 42,
                        YPos = 46
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
