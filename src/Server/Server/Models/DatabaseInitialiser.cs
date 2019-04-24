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
        }
    }
}
