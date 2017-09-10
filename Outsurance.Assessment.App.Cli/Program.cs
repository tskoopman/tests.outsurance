using System;
using System.IO;

namespace Outsurance.Assessment.App.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                ContactList contactList = new ContactList();
                contactList.Load(args[0]);

                Console.WriteLine($"Loaded {contactList.Count} rows.");

                string filename = "sorted_name_frequencies.txt";
                File.WriteAllLines(filename, contactList.ToStrings(contactList.NameFrequency()));
                Console.WriteLine($"Wrote sorted name frequencies to: {filename}");

                filename = "sorted_addresses.txt";
                File.WriteAllLines(filename, contactList.GetAddresses());
                Console.WriteLine($"Wrote sorted addresses to: {filename}.");
            }
            else
            {
                Console.WriteLine("Syntax: ./Outsurance.Assessment.App.Cli <filepath>");
            }
        }
    }
}
