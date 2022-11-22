using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TTEC.EMPOWER.Common.API_Models;
using TTEC.EMPOWER.Common.Helpers;
using TTEC.EMPOWER.Data.Interfaces;

namespace TTEC.EMPOWER.Business.Features.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<OperationResult<GetUserResponse>>
    {
        //Define Model request property here 
        //[Required]
        public int EmployeeId { get; set; }

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OperationResult<GetUserResponse>>
        {
            private IUserRepository _userRepository;
            private IMapper _mapper;
            private ILogger _logger;
            public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper, ILogger logger)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _logger = logger;
            }
            /// <summary>
            /// Query Handler to get user details
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<OperationResult<GetUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("From get user details Mediator Call123");
                

                //use of mapper example
                var model = _mapper.Map<GetUserResponse>(request);

                var getUserResponse = await _userRepository.GetUserDetailsById(request.EmployeeId.ToString());

                var response = new OperationResult<GetUserResponse>
                {
                    Data = getUserResponse.FirstOrDefault(),
                    Status = "Success",
                    HttpStatus = HttpStatusCode.OK
                };
                if (response.Data == null)
                {
                    response.Status = "Failed";
                    response.HttpStatus = HttpStatusCode.NotFound;
                }


                return response;
            }
        }
    }
}
