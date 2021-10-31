using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomCouponCode
{
    public static class FakeData
    {
        private static List<Person> _people;
        public static List<Person> GetPeople(int number)
        {
            _people = new Faker<Person>()
            .RuleFor(u => u.Id, f => f.IndexFaker)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u=> u.BirthDate, f=> f.Date.PastOffset(60,
                    DateTime.Now.AddYears(-18)).Date.ToShortDateString())
            .RuleFor(u => u.Address, f => f.Address.FullAddress())
            .Generate(number);
            return _people;
        }
    }
}
