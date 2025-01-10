using Microsoft.AspNetCore.Mvc;
using TrackerDeFavorisApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TrackerDeFavorisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _userManager;
        public UserController(UserContext userManager){
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id){
            var user = await _userManager.Users.FindAsync(id);
            if (user == null){
                return NotFound();
            }
            return Ok(user);
        }

    [HttpGet("User")]
    public async Task<ActionResult<List<string>>> GetUsers()
    {
        var usersprenom = await _userManager.Users.Select(u => u.Id).ToListAsync();

        if (usersprenom == null || usersprenom.Count == 0)
        {
            return NotFound("No users found.");
        }

        return Ok(usersprenom);
    }
    [HttpGet("Admin")]
    public async Task<ActionResult<string>> GetAdmins(){
        var admins = await _userManager.Users.Where(u => u.RoleType == 0).Select(u => u.Id).ToListAsync();
        if (admins == null || admins.Count == 0){
            return NotFound("No users Found.");
        }
        else{
            return Ok($"Voici les admins {admins}");
        }

    }
    [HttpPost("register")]
    public async Task<ActionResult<User>> PostUser(Userinfo userinfo) {
        var user = new User();
        user.Id = userinfo.Login;
        user.MotDePasse = userinfo.Password;
        _userManager.Users.Add(user);
        await _userManager.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { Id = user.Id }, user);
    }

    [HttpPost("login")]
    public async Task<ActionResult> PostLogin(Userinfo userinfo){
        var found = await _userManager.Users.FirstOrDefaultAsync(u => userinfo.Login == u.Id && userinfo.Password == u.MotDePasse);
        if (found != null){
            return Ok(new { message = "Login successful", userId = found.Id} );
        }
        else{
            return NotFound();
        }
    }

    [HttpPut(" ")]
    public async Task<ActionResult<User>> PutUser(Userinfo userinfo){
        var user = await _userManager.Users.FindAsync(userinfo.Login);
        if (user == null){
            return NotFound();
        }
        user.Prenom = userinfo.Login;
        user.MotDePasse = userinfo.Password;
        _userManager.Entry(user).State = EntityState.Modified;
        try
            {
                await _userManager.SaveChangesAsync();
            }
        catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erreur de concurrence");
            }
        return Ok(user);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id){
        var user = await _userManager.Users.FindAsync(id);
        if (user == null){
            return NotFound();
        }
        _userManager.Users.Remove(user);
        await _userManager.SaveChangesAsync();
        return NoContent();
    }
}
}