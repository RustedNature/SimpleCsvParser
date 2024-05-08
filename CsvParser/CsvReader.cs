using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CsvParser
{
    internal class CsvReader
    {
        private string pathToFile = string.Empty;
        private char delimiter;
        private string[,]? content;
        private int numberOfRows = 0;
        private int numberOfColumns = 0;

        public CsvReader(string pathToFile, char delimiter = ';')
        {
            CheckIsCsv(pathToFile);
            PathToFile = pathToFile;
            Delimiter = delimiter;
        }

        public string PathToFile { get => pathToFile; set => pathToFile = value; }
        public char Delimiter { get => delimiter; set => delimiter = value; }

        public string[,] Content { get => content!; }
        public int NumberOfRows { get => numberOfRows; set => numberOfRows = value; }
        public int NumberOfColumns { get => numberOfColumns; set => numberOfColumns = value; }

        private static void CheckIsCsv(string pathToFile)
        {
            string fileExtension = Path.GetExtension(pathToFile);

            if (".csv" != fileExtension)
            {
                throw new Exception("Please insert .csv files only!");
            }
        }

        public void ReadCsv()
        {
            SetNumberOfRows();
            SetNumberOfColumn();
            Create2DArray();
            ParseCells();
            Console.WriteLine("fertsch");
        }

        private void Create2DArray()
        {
            content = new string[NumberOfRows, NumberOfColumns];
        }

        private void SetNumberOfColumn()
        {
            using StreamReader sr = new(PathToFile);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine()!;
                string[] splits = line.Split(Delimiter);
                NumberOfColumns = splits.Length;
            }
        }

        private void SetNumberOfRows()
        {
            using StreamReader sr = new(PathToFile);
            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                NumberOfRows++;
            }
        }

        private void ParseCells()
        {
            int readingColumn = 0;
            int readingRow = 0;
            using StreamReader sr = new(PathToFile);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine()!;

                foreach (string split in line.Split(Delimiter))
                {
                    content![readingRow, readingColumn] = split.Trim();

                    readingColumn++;
                }
                readingRow++;
                readingColumn = 0;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            for (int i = 0; i < content!.GetLength(0); i++)
            {
                for (int j = 0; j < content.GetLength(1); j++)
                {
                    sb.Append(content[i, j]);
                    sb.Append(" | ");
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}