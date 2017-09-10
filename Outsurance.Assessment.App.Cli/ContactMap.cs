using CsvHelper.Configuration;

namespace Outsurance.Assessment.App.Cli
{
    public class ContactMap : CsvClassMap<Contact>
    {
        public ContactMap()
        {
            Map(m => m.FirstName).Index(0);
            Map(m => m.LastName).Index(1);
            Map(m => m.Address).Index(2);
            Map(m => m.PhoneNumber).Index(3);
        }
    }
}