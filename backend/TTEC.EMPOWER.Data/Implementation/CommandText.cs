using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTEC.EMPOWER.Data.Interfaces;

namespace TTEC.EMPOWER.Data.Implementation
{
    public class CommandText: ICommandText
    {
        string ICommandText.GetUserById => "USER_SEARCH";
    }
}
