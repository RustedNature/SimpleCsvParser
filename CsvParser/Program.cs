using System.Text;

namespace CsvParser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CsvReader cr = new(@"C:\Users\Nico\source\repos\C#\CsvParser\CsvParser\TestDaten.csv");

            cr.ReadCsv();

            Console.WriteLine(cr.ToString());
        }
    }
}