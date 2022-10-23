using System;
using System.Collections.Generic;

namespace ZaawansowaneProgramowanieWSB_3
{
    internal class Program
    {
        public class Group
        {
            public string Name { get; set; }
            public int StudentCount { get; set; }

            public Dictionary<string, List<DateTime>> TimeTable;

            public Group(string name, int studentCount, Dictionary<string, List<DateTime>> timeTable)
            {
                Name = name;
                StudentCount = studentCount;
                TimeTable = timeTable;

                Console.WriteLine("Tworzenie grupy o nazwie: {0} zakończone powodzeniem!", name);
            }
            public string WypiszPlanZajec()
            {
                string s = "";
                foreach (var item in TimeTable)
                {
                    s += "\nPrzedmiot: " + item.Key + " Terminy: ";
                    string dateList = "";
                        
                    foreach (DateTime dateTime in item.Value)
                            dateList += dateTime.ToString("g") + "; ";
                    s += dateList;
                }
                return s;
                   
            }
            public override string ToString()
            {
                string s = "\nGRUPA \"" + this.Name + "\":";
                s += "\nLiczba Studentów: " + this.StudentCount + ";";
                s += "\nPLAN ZAJĘĆ: ";
                s += this.WypiszPlanZajec();
                return s;
            }
        }
        public static class TimeTable
        {
            private static readonly string[] classNames = { "Matematyka Dyskretna", "Programowanie Komputerowe", "Bazy Danych", "Język Angielski", "Statystyka", "Podstawy Prawa" };
            private static int eachClassSessionsNumber = 2;
            private static readonly DateTime[] availableDateTimes =
            {
                new DateTime(2022 , 10 , 1 , 8 , 0 , 0 ),
                new DateTime(2022 , 10 , 1 , 11 , 20 , 0 ) ,
                new DateTime(2022 , 10 , 1 , 14 , 50 , 0 ) ,
                new DateTime(2022 , 10 , 1 , 18 , 05 , 0 ) ,
                new DateTime(2022 , 10 , 2 , 8 , 0 , 0 ),
                new DateTime(2022 , 10 , 2 , 11 , 20 , 0 ) ,
                new DateTime(2022 , 10 , 2 , 14 , 50 , 0 ) ,
                new DateTime(2022 , 10 , 2 , 18 , 05 , 0 ),
                new DateTime(2022 , 10 , 8 , 8 , 0 , 0 ),
                new DateTime(2022 , 10 , 8 , 11 , 20 , 0 ) ,
                new DateTime(2022 , 10 , 8 , 14 , 50 , 0 ) ,
                new DateTime(2022 , 10 , 8 , 18 , 05 , 0 ) ,
                new DateTime(2022 , 10 , 9 , 8 , 0 , 0 ),
                new DateTime(2022 , 10 , 9 , 11 , 20 , 0 ) ,
                new DateTime(2022 , 10 , 9 , 14 , 50 , 0 ) ,
                new DateTime(2022 , 10 , 9 , 18 , 05 , 0 ),
                new DateTime(2022 , 10 , 15 , 8 , 0 , 0 ),
                new DateTime(2022 , 10 , 15 , 11 , 20 , 0 ) ,
                new DateTime(2022 , 10 , 15 , 14 , 50 , 0 ) ,
                new DateTime(2022 , 10 , 15 , 18 , 05 , 0 ) ,
                new DateTime(2022 , 10 , 16 , 8 , 0 , 0 ),
                new DateTime(2022 , 10 , 16 , 11 , 20 , 0 ) ,
                new DateTime(2022 , 10 , 16 , 14 , 50 , 0 ) ,
                new DateTime(2022 , 10 , 16 , 18 , 05 , 0 ),
                new DateTime(2022 , 10 , 22 , 8 , 0 , 0 ),
                new DateTime(2022 , 10 , 22 , 11 , 20 , 0 ) ,
                new DateTime(2022 , 10 , 22 , 14 , 50 , 0 ) ,
                new DateTime(2022 , 10 , 22 , 18 , 05 , 0 ) ,
                new DateTime(2022 , 10 , 23 , 8 , 0 , 0 ),
                new DateTime(2022 , 10 , 23 , 11 , 20 , 0 ) ,
                new DateTime(2022 , 10 , 23 , 14 , 50 , 0 ) ,
                new DateTime(2022 , 10 , 23 , 18 , 05 , 0 )
            };
            public static Dictionary<string, List<DateTime>> GenerateRandom()
            {
                Dictionary<string, List<DateTime>> result = new Dictionary<string, List<DateTime>>();
                Random random = new Random();
                DateTime[] dateTimesCopyBefore = availableDateTimes;
                DateTime[] dateTimesCopyAfter = new DateTime[dateTimesCopyBefore.Length - 1];
                int dateTimeCopyIndexCounter = 0;

                foreach ( string className in classNames)
                {
                    List<DateTime> tempList = new List<DateTime>();
                    for( int i = 0; i < eachClassSessionsNumber; i++ )
                    {
                        int RandomDateTimeIndex = random.Next( 0, dateTimesCopyBefore.Length - 1 );
                        tempList.Add( dateTimesCopyBefore[RandomDateTimeIndex] );
                        for( int j = 0; j < dateTimesCopyBefore.Length; j++)
                        {
                            if( j != RandomDateTimeIndex) 
                            {
                                dateTimesCopyAfter[dateTimeCopyIndexCounter] = dateTimesCopyBefore[j];
                                dateTimeCopyIndexCounter++;
                            }
                        }
                        dateTimesCopyBefore = new DateTime[dateTimesCopyAfter.Length];
                        dateTimesCopyBefore = dateTimesCopyAfter;
                        dateTimesCopyAfter = new DateTime[dateTimesCopyBefore.Length - 1];
                        dateTimeCopyIndexCounter = 0;
                    }
                    result.Add( className, tempList );
                }
                return result;
            }
        }
        public static class CollisionDetection
        {
            private static string report;
            private static bool collisionDetectionStatus = false;
            public static void Detect(Group groupA , Group groupB)
            {
                ValueTuple<string, string> tempSetA = new ValueTuple<string, string>();
                ValueTuple<string, string> tempSetB = new ValueTuple<string, string>();
                bool messageFlag = false;

                foreach (var itemA in groupA.TimeTable)
                {
                    messageFlag = false;
                    foreach (var przedmiotA in itemA.Key)
                    {
                        foreach (DateTime dataA in itemA.Value)
                        {
                            tempSetA.Item1 = itemA.Key;
                            tempSetA.Item2 = dataA.ToString("g");

                            foreach (var itemB in groupB.TimeTable)
                            {
                                foreach (var przedmiotB in itemB.Key)
                                {
                                    foreach (DateTime dataB in itemB.Value)
                                    {
                                        tempSetB.Item1 = itemB.Key;
                                        tempSetB.Item2 = dataB.ToString("g");
                                        if (tempSetA.Item1.Equals(tempSetB.Item1) && tempSetA.Item2.Equals(tempSetB.Item2) && !messageFlag)
                                        {
                                            string s = "Przedmiot: " + tempSetA.Item1 + ", Data/Godzina: " + tempSetA.Item2;
                                            report += "\n" + s;
                                            collisionDetectionStatus = true;
                                            messageFlag = true;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            public static void Report()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\tRAPORT WYKRYTYCH KOLIZJI: ");
                if (collisionDetectionStatus)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(report);
                } 
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Nie wykryto żadnych kolizji w planach zajęć wybranych grup.");
                }
                Console.ForegroundColor= ConsoleColor.White;

            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Test Generowania Grup:");

            Group ININ4_PR1 = new Group("ININ4_PR1", 30, TimeTable.GenerateRandom());
            Group ININ4_PR2 = new Group("ININ4_PR2", 20, TimeTable.GenerateRandom());

            Console.WriteLine("\nTest Generowania Opisów Grup:");

            Console.WriteLine(ININ4_PR1.ToString());
            Console.WriteLine(ININ4_PR2.ToString());

            Console.WriteLine("\nTest Detektora Kolizji:");

                CollisionDetection.Detect(ININ4_PR1, ININ4_PR2);
                CollisionDetection.Report();
        }
    }
}
