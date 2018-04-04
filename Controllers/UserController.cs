using System.Collections.Generic;
using System.Linq;
using LifeReelAPI.Data;
using LifeReelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LifeReelAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        //instance of the application context
        private ApplicationDbContext _context;

        public UserController(ApplicationDbContext ctx){
            _context = ctx;
        }

        // GET api/user
        //returns list of users
        [HttpGet]
        public IActionResult Get()
        {
            List<User> users = _context.User.ToList();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET api/user/5
        //gets single user NOT SURE ABOUT ID BEING A STRING
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User user = _context.User.Single(p => p.Id == id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/user
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    
    }
}