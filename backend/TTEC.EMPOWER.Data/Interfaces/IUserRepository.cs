using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTEC.EMPOWER.Common.API_Models;

namespace TTEC.EMPOWER.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<GetUserResponse>> GetUserDetailsById(string employeeId);
    }
}
