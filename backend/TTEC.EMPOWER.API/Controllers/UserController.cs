using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTEC.EMPOWER.Business.Features.Queries.GetUserById;

namespace TTEC.EMPOWER.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger<UserController> _logger;

        /// <summary>
        /// UserController Constructor
        /// </summary>
        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get User Details By UserId
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetUserDetailsById")]
        public async Task<IActionResult> GetUser([FromQuery] GetUserByIdQuery model)
        {
            _logger.LogInformation("From get user details API Call123  - UserController");
            var result = await _mediator.Send(model);
            return Ok(result);
        }
    }
}
