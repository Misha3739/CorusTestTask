using CORUS_TestTask.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CORUS_TestTask
{
    class Program
    {
      static  string s_fileName = $@"C:\CorusTest\File_{new DateTime(2017,11,15,14,20,00).ToString("yyyy.MM.dd_HH.mm.ss")}.xml";
        static void Main(string[] args)
        {
            var collection = GetCollection();
            //WriteToFile(collection);
            //var collectionFromFile = GetFromFile();
            //PrintCollection(collectionFromFile);

            PrintGrouped();
            Console.ReadLine();
        }
        #region File
        private static void WriteToFile(IEnumerable<Pallet> collection)
        {
            

            try
            {
                using (FileStream fs = new FileStream(s_fileName, FileMode.Create, FileAccess.Write, FileShare.None)) {

                    XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Pallet>));
                    xmlFormat.Serialize(fs, collection);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static IEnumerable<Pallet> GetFromFile()
        {
            try
            {
                using (FileStream fs = new FileStream(s_fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {

                    XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Pallet>));
                  var result =  xmlFormat.Deserialize(fs);
                    return result as List<Pallet>;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

        }

        #endregion

        private static void PrintCollection(IEnumerable<Pallet> collection)
        {
            foreach(var pal in collection)
            {
                Console.WriteLine(ReflectionUtils.ToString(pal));
            }
        }

        private static void PrintGrouped()
        {
            var collection = GetCollection();

            var result = collection.GroupBy(p => p.EndDate).Select(p => new Pallet()
            {
                Boxes = collection.
                Where(c => c.EndDate == p.Key.Value).
                SelectMany(b => b.Boxes).OrderBy(b => b.Weight).ToList()
            });

            foreach (var item in result)
            {
                Console.WriteLine($"Pallet with end date: {item.EndDate} and boxes:");
                foreach (var box in item.Boxes)
                {
                    Console.WriteLine(ReflectionUtils.ToString(box));
                }
            }
        }

        private static IEnumerable<Pallet> GetCollection()
        {
            Random rnd = new Random();
            return new List<Pallet>()
            {
                new Pallet()
                {
                    Id = 1,
                    Height =30,
                    Width = 20,
                    Depth = 55,
                    Weight = 700,
                    Boxes = new List<Box>
                    {
                        new Box() {
                            Id = 11,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                        ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-20)
                    },
                       new Box() {
                            Id = 12,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                        ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-40)
                    },

                        new Box() {
                            Id = 13,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                        ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-70)
                    }
                }

                },
                 new Pallet()
                {
                      Id = 2,
                    Height = 24.5f,
                    Width = 22.3f,
                    Depth = 43.8f,
                    Weight = 671.3f,
                    Boxes = new List<Box>()
                    {
                          new Box() {
                            Id = 21,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                        ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-20)
                    },
                       new Box() {
                            Id = 22,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                         ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-50)
                    },

                        new Box() {
                            Id = 23,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                         ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-70)
                    }
                    }
                },
                  new Pallet()
                {
                       Id = 3,
                    Height =13.5f,
                    Width = 22.4f,
                    Depth = 11,
                    Weight = 315.4f,
                     Boxes = new List<Box>()
                    {
                          new Box() {
                            Id = 31,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                         ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-10)
                    },
                       new Box() {
                            Id =32,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                         ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-3)
                    },

                        new Box() {
                            Id = 33,
                          Height = rnd.NextDouble(),
                        Width =  rnd.NextDouble(),
                        Depth =  rnd.NextDouble(),
                        Weight =  rnd.NextDouble(),
                         ProductionDate = new DateTime(2017,11,15,14,20,00).AddDays(-70)
                    }
                    }
                  }
            };
        }
    }
}
