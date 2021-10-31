using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandomCouponCode
{
    public class RandomCouponCreate
    {
        private  string StartCharacter;
        private  string CompanyCharacter;
        private  string RandomCharacter;
        private  Random r;
        private  Random n;
        private  int RandomNumber;
        private  string RandomLocation;
        private  string RandomCharacterLeft;
        private  string RandomCharacterRight;
        private  string RandomCharacterMiddle;
        private  string Miliseconds;
        private  string FirstName;
        private  string LastName;
        private  string BirthDate;
        private Dictionary<string, int> LocationCoordinate;
        private List<Coupon> CouponCodes;
        private List<Person> people;
        public RandomCouponCreate()
        {
            r = new Random();
            n = new Random();
            StartCharacter = "X";
            CompanyCharacter = "P";
            RandomCharacterLeft = "";
            RandomCharacterRight = "";
            RandomCharacterMiddle = "";
            RandomCharacter = "";
            LocationCoordinate = new Dictionary<string, int>();
            CouponCodes = new List<Coupon>();
            people = new List<Person>();
        }

        public List<Coupon> RandomCouponCodes(int FakeDataCount, int CouponCount)
        {
            people = FakeData.GetPeople(FakeDataCount);
            for (int i = 0; i < CouponCount; i++)
            {
                Coupon Coupon = new Coupon();
                RandomNumber = r.Next(0, 2);
                RandomLocation = Enum.GetName(typeof(Location), RandomNumber);
                Miliseconds = StringHelpers.GetLast(DateTime.Now.Ticks.ToString(), 3);

                switch (RandomLocation)
                {
                    case "Left":
                        LocationCoordinate.Add("StartLeft", 0);
                        LocationCoordinate.Add("EndLeft", 5);
                        LocationCoordinate.Add("StartMiddle", 5);
                        LocationCoordinate.Add("EndMiddle", 3);
                        LocationCoordinate.Add("StartRight", 10);
                        LocationCoordinate.Add("EndRight", 3);
                        break;

                    case "Middle":
                        LocationCoordinate.Add("StartLeft", 0);
                        LocationCoordinate.Add("EndLeft", 3);
                        LocationCoordinate.Add("StartMiddle", 5);
                        LocationCoordinate.Add("EndMiddle", 5);
                        LocationCoordinate.Add("StartRight", 10);
                        LocationCoordinate.Add("EndRight", 3);
                        break;
                    case "Right":
                        LocationCoordinate.Add("StartLeft", 0);
                        LocationCoordinate.Add("EndLeft", 3);
                        LocationCoordinate.Add("StartMiddle", 5);
                        LocationCoordinate.Add("EndMiddle", 3);
                        LocationCoordinate.Add("StartRight", 10);
                        LocationCoordinate.Add("EndRight", 5);
                        break;
                }

                RandomCharacterLeft = Guid.NewGuid().ToString().Replace("-", "").Substring(LocationCoordinate["StartLeft"], LocationCoordinate["EndLeft"]).ToUpper();
                RandomCharacterRight = Guid.NewGuid().ToString().Replace("-", "").Substring(LocationCoordinate["StartRight"], LocationCoordinate["EndRight"]).ToUpper();
                RandomCharacterMiddle = Guid.NewGuid().ToString().Replace("-", "").Substring(LocationCoordinate["StartMiddle"], LocationCoordinate["EndMiddle"]).ToUpper();
                FirstName = StringHelpers.FirstCharacter(people[i].FirstName).ToString();
                LastName = StringHelpers.FirstCharacter(people[i].LastName).ToString();
                BirthDate = StringHelpers.GetFirst(people[i].BirthDate.Replace(".", ""),3);
 
                RandomCharacter = RandomCharacterLeft + RandomCharacterRight + RandomCharacterMiddle + FirstName + LastName + BirthDate + Miliseconds;
                for(int x = 0; x<3;x++)
                {
                    RandomCharacter = RandomCharacter + StringHelpers.MixString(RandomCharacter);
                }

                Coupon.CouponCode = StartCharacter + CompanyCharacter + StringHelpers.MixString(RandomCharacter.Substring(n.Next(0, RandomCharacter.Length-9), 9));
                CouponCodes.Add(Coupon);
                LocationCoordinate.Clear();
            }
            return CouponCodes;
        }
    }
}
