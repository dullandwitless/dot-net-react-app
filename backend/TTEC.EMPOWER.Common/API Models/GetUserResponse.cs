using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTEC.EMPOWER.Common.API_Models
{
    public class GetUserResponse
    {
        public int USER_ID { get; set; }
        public string? FIRST_NAME { get; set; }
        public string? LAST_NAME { get; set; }
        public string? Email { get; set; }
        public int USER_NAME { get; set; }


    }
}
