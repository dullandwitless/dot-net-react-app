using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTEC.EMPOWER.Business.Features.Queries.GetUserById;
using TTEC.EMPOWER.Common.API_Models;

namespace TTEC.EMPOWER.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetUserByIdQuery, GetUserResponse>();

        }
    }
}
