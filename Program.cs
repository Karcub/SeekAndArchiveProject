using System;

namespace SeekAndArchiveProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GetFiles getFiles = new GetFiles();
            getFiles.GetFilesBySearch(args);

            Console.WriteLine("1. New Search\n"+
                              "2. Browse History\n"+
                              "3. Exit");
            var option = Console.ReadLine();

            while (option != "3")
            {
                if (option == "1")
                {
                    Console.WriteLine("Enter filename:");
                    string fileName = Console.ReadLine();
                    Console.WriteLine("Enter path:");
                    string path = Console.ReadLine();
                    getFiles.GetFilesBySearch(new []{fileName, path});
                    Console.WriteLine("Search is saved.");
                }
                if (option == "2")
                {
                    getFiles.DeserializeFromXmlToObject();
                }
                Console.WriteLine("1. New Search\n"+
                                  "2. Browse History\n"+
                                  "3. Exit");
                option = Console.ReadLine();
            }
        }
    }
}