using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackerDeFavorisApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

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
        [HttpDelete("Remove Favoris")]
        public async Task<IActionResult> DeleteFavoris(int id){
            var f = await _Favoriscontext.Favoriss.FindAsync(id);
            if (f == null){
                return NotFound("No film!!");
            }
            else{
                await _Favoriscontext.SaveChangesAsync();
                _Favoriscontext.Favoriss.Remove(f);
            }
            return Ok("Deleted!!");
        }
[HttpGet("List/{userid}")]
public async Task<ActionResult> GetFavorites(int userid)
{
    var arr = await _Favoriscontext.Favoriss
        .Where(f => f.Id == userid)
        .Select(f => f.Id)
        .ToListAsync();

    if (arr != null && arr.Count != 0)
    {
        return Ok(arr);
    }
    return NotFound("Aucun favori trouvé pour l'utilisateur spécifié.");
}


    }
}