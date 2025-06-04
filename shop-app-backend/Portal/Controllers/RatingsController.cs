using AppCore.Store;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Rating;

namespace ShopPortal.Controllers
{
    /// <summary>
    /// Controller responsible for ratings related with product
    /// </summary>
    [ApiController]
    [Route("api/ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsManager _ratingsManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RatingsController> _logger;

        /// <inheritdoc />
        public RatingsController(IRatingsManager ratingsManager, IMapper mapper, ILogger<RatingsController> logger)
        {
            _ratingsManager = ratingsManager ?? throw new ArgumentNullException(nameof(ratingsManager));
            _mapper = mapper ?? throw  new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Insert Rating
        /// </summary>
        /// <param name="rating"></param>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingViewModel rating)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            try
            {
                await _ratingsManager.Vote(_mapper.Map<Rating>(rating), email);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound();
            }

            return NoContent();
        }
    }
}
