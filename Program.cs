using System;
using System.Collections.Generic;
using System.Linq;

namespace ZaawansowaneProgramowanieWSB_3
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
            string s = "\nGRUPA \"" + Name + "\":";
            s += "\nLiczba Studentów: " + StudentCount + ";";
            s += "\nPLAN ZAJĘĆ: ";
            s += WypiszPlanZajec();
            return s;
        }
    }
    public static class TimeTable
    {
        private static readonly string[] classNames = { "Matematyka Dyskretna", "Programowanie Komputerowe", "Bazy Danych", "Język Angielski", "Statystyka", "Podstawy Prawa" };
        private static int eachClassSessionsNumber = 4;
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

            foreach (string className in classNames)
            {
                List<DateTime> tempList = new List<DateTime>();
                for (int i = 0; i < eachClassSessionsNumber; i++)
                {
                    int RandomDateTimeIndex = random.Next(0, dateTimesCopyBefore.Length - 1);
                    tempList.Add(dateTimesCopyBefore[RandomDateTimeIndex]);
                    for (int j = 0; j < dateTimesCopyBefore.Length; j++)
                    {
                        if (j != RandomDateTimeIndex)
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
                tempList.Sort();
                result.Add(className, tempList);
            }
            return result;
        }
    }

    public static class CollisionDetection
    {
        private static string report;
        private static bool collisionDetectionStatus = false;

        public static List<ValueTuple<string, string>> TransformTimetableToAList(Group group)
        {
            List<ValueTuple<string, string>> result = new List<ValueTuple<string, string>>();

            foreach (var item in group.TimeTable)
            {
                foreach (var przedmiot in item.Key)
                {
                    foreach (DateTime data in item.Value)
                    {
                        result.Add((item.Key, data.ToString("g")));
                    }
                }
            }
            return result;
        }

        public static void DetectCollisions(Group groupA, Group groupB)
        {
            var listaZajecGrupyA = from zajecia in TransformTimetableToAList(groupA)
                                   orderby zajecia.Item1 , zajecia.Item2
                                   select new { zajecia.Item1, zajecia.Item2 };

            var listaZajecGrupyB = from zajecia in TransformTimetableToAList(groupB)
                                   orderby zajecia.Item1 , zajecia.Item2
                                   select new { zajecia.Item1, zajecia.Item2 };

            var listaKolizji = listaZajecGrupyA.Intersect(listaZajecGrupyB);


            foreach (var zajecia in listaKolizji)
            {
                string s = "Przedmiot: " + zajecia.Item1 + " Data/Godzina: " + zajecia.Item2;
                report += "\n" + s;
                collisionDetectionStatus = true;
            }

        }

        public static void ReportCollisions()
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
            Console.ForegroundColor = ConsoleColor.White;

        }
    }

    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Test Generowania Grup:");

            Group ININ4_PR1 = new Group("ININ4_PR1", 30, TimeTable.GenerateRandom());
            Group ININ4_PR2 = new Group("ININ4_PR2", 20, TimeTable.GenerateRandom());

            Console.WriteLine("\nTest Generowania Opisów Grup:");

            Console.WriteLine(ININ4_PR1.ToString());
            Console.WriteLine(ININ4_PR2.ToString());

            Console.WriteLine("\nTest Detektora Kolizji:");

            CollisionDetection.DetectCollisions(ININ4_PR1, ININ4_PR2);
            CollisionDetection.ReportCollisions();
        }
    }
}
