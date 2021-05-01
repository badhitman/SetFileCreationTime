using System;
using System.IO;
using System.Linq;

namespace SetFileCreationTime
{
    class Program
    {
        static Random gen = new Random();
        static readonly DateTime start = new DateTime(2005, 1, 1);
        static readonly int range = (DateTime.Today - start).Days;
        static string CurrentExeLocation => System.Reflection.Assembly.GetEntryAssembly().Location.ToLower();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            gen.Next(range);

            foreach (string file in Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories).Where(x => x.ToLower() != CurrentExeLocation))
            {
                DateTime creationTime = File.GetCreationTime(file);
                Console.WriteLine(creationTime.ToString("MM/dd/yyyy HH:mm:ss"));
                (DateTime, DateTime, DateTime) values = OrganicRndDateTime;
                try
                {
                    File.SetCreationTime(file, values.Item1);
                    File.SetLastWriteTime(file, values.Item2);
                    File.SetLastAccessTime(file, values.Item3);

                    Console.WriteLine(File.GetCreationTime(file));
                }
                catch
                {

                }
            }
        }

        static (DateTime, DateTime, DateTime) OrganicRndDateTime
        {
            get
            {
                DateTime c_rnd_dt = RndDateTime;
                DateTime w_rnd_dt = c_rnd_dt.AddDays(gen.Next(5, 25));
                DateTime r_rnd_dt = w_rnd_dt.AddDays(gen.Next(5, 25));
                return (c_rnd_dt, w_rnd_dt, r_rnd_dt);
            }
        }

        static DateTime RndDateTime => start.AddDays(gen.Next(365, range)).AddHours(gen.Next(1, 20)).AddMinutes(gen.Next(60, 500)).AddSeconds(gen.Next(60, 500));
    }
}
