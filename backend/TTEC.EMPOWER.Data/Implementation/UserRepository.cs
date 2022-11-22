using Dapper;
using Dapper.Oracle;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTEC.EMPOWER.Common.API_Models;
using TTEC.EMPOWER.Data.Interfaces;

namespace TTEC.EMPOWER.Data.Implementation
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ICommandText _commandText;
        private readonly IConfiguration _configuration;


        public UserRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetUserResponse>> GetUserDetailsById(string employeeId)
        {
            var param = new OracleDynamicParameters();
            param.Add("@VUSER_ID");
            param.Add("@VUSERNAME", employeeId);

            param.Add(name: "@CUROUT", dbType:OracleMappingType.RefCursor, direction:ParameterDirection.Output);

            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<GetUserResponse>(_commandText.GetUserById,
                                                               param: param,
                                                                commandType: CommandType.StoredProcedure);

                return query.ToList();
            });

        }
    }
}
