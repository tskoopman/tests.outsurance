using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Outsurance.Assessment.App.Cli
{
    public class ContactList : List<Contact>
    {
        public ContactList()
        {
        }

        public bool Load(string filename)
        {
            bool result = false;
            if (File.Exists(filename))
            {
                Clear();

                string content = File.ReadAllText(filename);
                using (StringReader reader = new StringReader(content))
                {
                    CsvReader csv = new CsvReader(reader);
                    AddRange(csv.GetRecords<Contact>().ToList());
                }

                result = true;
            }
            return result;
        }

        public List<IGrouping<string, string>> NameFrequency()
        {
            List<string> names = new List<string>();
            names.AddRange(this.Select(c => c.FirstName));
            names.AddRange(this.Select(c => c.LastName));

            List<IGrouping<string, string>> result = names.GroupBy(name => name).ToList();
            return result;
        }

        public List<string> ToStrings(List<IGrouping<string, string>> list)
        {
            List<string> result = list.Select(item => new
            {
                Name = item.Key,
                Count = item.Count()
            })
            .OrderByDescending(item => item.Count)
            .ThenBy(item => item.Name)
            .Select(item => $"{item.Name}, {item.Count}")
            .ToList();
            return result;
        }

        public List<string> GetAddresses()
        {
            List<string> result = this.Select(item => new
            {
                HouseNumber = item.Address.Substring(0, item.Address.IndexOf(' ')),
                StreetName = item.Address.Substring(item.Address.IndexOf(' ') + 1)
            })
            .OrderBy(item => item.StreetName)
            .Select(item => $"{item.HouseNumber} {item.StreetName}")
            .ToList();
            return result;
        }
    }
}