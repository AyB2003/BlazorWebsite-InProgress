using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackerDeFavorisApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackerDeFavorisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavorisController : ControllerBase
    {
        private readonly FavorisContext _Favoriscontext;


        public FavorisController(FavorisContext ctx)
        {
            _Favoriscontext = ctx;
        }

        [HttpPost("Add favorite")]
        public async Task<ActionResult<Favoris>> PostFavoris(Favoris favoris)
        {
            _Favoriscontext.Favoriss.Add(favoris);
            await _Favoriscontext.SaveChangesAsync();
            return Ok("Film added");
        }

    }
}