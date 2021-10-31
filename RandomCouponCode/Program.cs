using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomCouponCode
{
    class Program
    {
        static RandomCouponCreate randomCouponCreate = new RandomCouponCreate();

        static void Main(string[] args)
        {
            List<Coupon> CouponCodes = randomCouponCreate.RandomCouponCodes(500000, 500000);
            DAL.BulkInsert(DatatableHelpers.ListToDataTable<Coupon>(CouponCodes),"CouponCodes");

        }
    }
}
