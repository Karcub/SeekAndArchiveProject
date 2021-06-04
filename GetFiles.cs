using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace SeekAndArchiveProject
{
    class GetFiles
    {
        private static string xmlPath = @"C:\Users\pocahontas\Documents\ADVANCED\week2\si\SeekAndArchiveProject\SearchHistory.xml";
        private XDocument doc = XDocument.Load(xmlPath);
        public void GetFilesBySearch(string[] args)
        {
            var filename = args[0];
            var directory = args[1];
            var counter = 0;

            try
            {
                var allFiles = Directory.GetFiles(directory, $"{filename}.*", SearchOption.AllDirectories);
                SerializeObjectToXml(args,allFiles);
                foreach (var file in allFiles)
                {
                    counter++;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Files found:" + counter);
            }
        }

        public void SerializeObjectToXml(string[] args, string[] files)
        {
            XElement element = doc.Element("Data");
            
            XElement newSearch = new XElement("Search",
                new XElement("SearchTerm", args[0] + " " + args[1]),
                new XElement("Folder", args[1]),
                new XElement("Date", DateTime.Now));
            element.Add(newSearch);
            foreach (var file in files)
            {
                newSearch.Add(new XElement("Results",
                    new XElement("FileName", Path.GetFileName(file)),
                    new XElement("Extension", Path.GetExtension(file)),
                    new XElement("Path", Path.GetFullPath(file))));
            }
            doc.Save(xmlPath);
        }

        public void DeserializeFromXmlToObject()
        {
            IEnumerable<XElement> searches = from search in doc.Descendants("Search") select search;
            int counter = 1;
            foreach (var search in searches)
            {
                StringBuilder output = new StringBuilder();
                output.Append(counter);
                output.Append(". ");
                output.Append(search.Element("Date").Value);
                output.Append(": ");
                IEnumerable<XElement> results = from result in search.Descendants("Results") select result;
                output.Append("'" + Regex.Match(search.Element("SearchTerm").Value, @"^([\w\-]+)") + "'");
                output.Append(" in ");
                output.Append(search.Element("Folder").Value);
                output.Append("', ");
                output.Append(results.Count());
                output.Append(" items found");
                
                counter++;
                Console.WriteLine(output);
            }
        }
    }
}