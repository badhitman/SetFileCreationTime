using System;
using System.IO;
using System.Linq;

namespace SetFileCreationTime
{
    class Program
    {
        static void Main(string[] args)
        {
            Random gen = new Random();
            Console.WriteLine("Hello World!");
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            gen.Next(range);
            string current_exe = System.Reflection.Assembly.GetEntryAssembly().Location.ToLower();
            foreach (string file in Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories).Where(x => x.ToLower() != current_exe))
            {
                DateTime creationTime = File.GetCreationTime(file);
                Console.WriteLine(creationTime.ToString("MM/dd/yyyy HH:mm:ss"));
                gen.Next(range);
                try
                {
                    File.SetCreationTime(file, start.AddDays(gen.Next(365, range)).AddHours(gen.Next(1, 20)).AddMinutes(gen.Next(60, 500)).AddSeconds(gen.Next(60, 500)));
                    Console.WriteLine(File.GetCreationTime(file));
                }
                catch
                {

                }
            }
        }
    }
}
