using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Outsurance.Assessment.App.Cli;

namespace Outsurance.Assessment.Tests
{
    [TestClass]
    public class AssessmentTests
    {
        [TestMethod]
        public void Should_be_able_to_read_file()
        {
            // arrange
            bool expected = true;
            ContactList contactList = new ContactList();

            // act
            bool actual = contactList.Load("/home/theodore/Downloads/data.csv");

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_load_contact_list_and_decode_csv_file()
        {
            // arrange
            int expected = 8;
            ContactList contactList = new ContactList();

            // act
            contactList.Load("/home/theodore/Downloads/data.csv");
            int actual = contactList.Count;

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_get_frequencies_of_first_and_last_names()
        {
            // arrange
            ContactList contactList = new ContactList();
            contactList.Add(new Contact() { FirstName = "Jimmy", LastName = "Smith" });
            contactList.Add(new Contact() { FirstName = "Jimmy", LastName = "Smith" });
            contactList.Add(new Contact() { FirstName = "Clive", LastName = "Jimmy" });
            contactList.Add(new Contact() { FirstName = "Jimmy", LastName = "Owen" });

            // act
            var frequencies = contactList.NameFrequency();

            // assert
            Assert.IsTrue(frequencies[0].Key == "Jimmy" && frequencies[0].Count() == 4);
            Assert.IsTrue(frequencies[1].Key == "Clive" && frequencies[1].Count() == 1);
            Assert.IsTrue(frequencies[2].Key == "Smith" && frequencies[2].Count() == 2);
            Assert.IsTrue(frequencies[3].Key == "Owen" && frequencies[3].Count() == 1);
        }

        [TestMethod]
        public void Should_list_names_ordered_by_frequency_descending_and_then_alphabetically()
        {
            // arrange
            ContactList contactList = new ContactList();
            contactList.Add(new Contact() { FirstName = "Jimmy", LastName = "Smith" });
            contactList.Add(new Contact() { FirstName = "Jimmy", LastName = "Smith" });
            contactList.Add(new Contact() { FirstName = "Clive", LastName = "Jimmy" });
            contactList.Add(new Contact() { FirstName = "Jimmy", LastName = "Owen" });

            List<string> expected = new List<string>();
            expected.Add("Jimmy, 4");
            expected.Add("Smith, 2");
            expected.Add("Clive, 1");
            expected.Add("Owen, 1");

            // act
            List<string> actual = contactList.ToStrings(contactList.NameFrequency());

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_list_the_addresses_sorted_alphabetically_by_street_name()
        {
            // arrange
            ContactList contactList = new ContactList();
            contactList.Add(new Contact() { Address = "102 Long Lane" });
            contactList.Add(new Contact() { Address = "82 Stewart St" });
            contactList.Add(new Contact() { Address = "65 Ambling Way" });

            List<string> expected = new List<string>();
            expected.Add("65 Ambling Way");
            expected.Add("102 Long Lane");
            expected.Add("82 Stewart St");

            // act
            List<string> actual = contactList.GetAddresses();

            // assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
