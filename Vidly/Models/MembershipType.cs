using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        // Id is PK column
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DuraionInMonths { get; set; }
        public byte DiscountRate { get; set; }
    }
}