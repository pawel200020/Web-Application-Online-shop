﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop;
using ShopPortal.Entities;
using ViewModels.Rating;

namespace ShopPortal.Controllers
{
    [ApiController]
    [Route("api/ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RatingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
            _userManager = userManager ?? throw new NullReferenceException(nameof(userManager));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingViewModel rating)
        {
           
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            var userId  = (await _userManager.FindByNameAsync(email)).Id;
            var currentRate =  await _context.Rating.FirstOrDefaultAsync(x => x.ProductId == rating.ProductId && x.UserId == userId);

            if (currentRate == null)
            {
                var newRating = new Rating()
                {
                    ProductId = rating.ProductId,
                    Rate = rating.Rating,
                    UserId = userId,
                };
                _context.Add(newRating);
            }
            else
            {
                currentRate.Rate = rating.Rating;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}