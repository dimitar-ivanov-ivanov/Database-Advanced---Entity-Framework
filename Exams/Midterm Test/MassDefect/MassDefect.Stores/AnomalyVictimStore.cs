namespace MassDefect.Stores
{
    using MassDefect.Data;
    using MassDefect.Dtos;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AnomalyVictimStore
    {
        public static void AddAnomalyVictims(ICollection<AnomalyVictimDto> anomalyVictimDtos)
        {
            using (var context = new MassDefectContext())
            {
                foreach (var anomalyVictimDto in anomalyVictimDtos)
                {
                    var person = PersonStore.GetPersonByName(anomalyVictimDto.Person, context);
                    if(anomalyVictimDto.Id == null || person == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var anomaly = AnomalyStore.GetAnomalyById((int)anomalyVictimDto.Id, context);
                    if(anomaly == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var anomalyVictim = new AnomalyVictim()
                    {
                        Anomaly = anomaly,
                        Victim = person
                    };

                    Console.WriteLine($"Person {person.Name} is now victim of anomaly with Id {anomaly.Id}");
                    context.AnomalyVictims.Add(anomalyVictim);
                    anomaly.AnomalyVictims.Add(anomalyVictim);
                    person.AnomalyVictims.Add(anomalyVictim);
                }

                context.SaveChanges();
            }
        }


        public static AnomalyVictim GetAnomalyVictimByIds(int anomalyId,int victimId,MassDefectContext context)
        {
            return context.AnomalyVictims.FirstOrDefault
                (av => av.AnomalyId == anomalyId && av.VictimId == victimId);
        }
    }
}
