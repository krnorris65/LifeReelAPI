using System.Collections.Generic;
using System.Linq;
using LifeReelAPI.Data;
using LifeReelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeReelAPI.Controllers
{
    [Route("api/[controller]")]
    public class FriendController : Controller
    {
        //instance of the application context
        private ApplicationDbContext _context;

        public FriendController(ApplicationDbContext ctx){
            _context = ctx;
        }
        // GET api/friend
        //returns list of friends
        [HttpGet]
        public IActionResult Get()
        {
            List<Friend> _friends = _context.Friend.ToList();
            if (_friends == null)
            {
                return NotFound();
            }
            return Ok(_friends);
        }

        // GET api/friend/5
        //gets single friend
        [HttpGet("{id}", Name = "GetSingleFriend")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Friend _friend = _context.Friend.Single(p => p.Id == id);

                if (_friend == null)
                {
                    return NotFound();
                }

                return Ok(_friend);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/friend
        /* POST event to database 
        Arguments: Friend {
            "Sender": required User,
            "Receiver": required User,
            "Date": required string (formatted: YYYY-MM-DD),
            "Pending": bool
        }*/
        [HttpPost]
        public IActionResult Post([FromBody]Friend _friend)
        {
            if (!ModelState.IsValid)
            {
                //if not valid data according to conditions then return the error
                return BadRequest(ModelState);
            }

            _context.Friend.Add(_friend);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FriendExists(_friend.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return the friend that was just added
            return CreatedAtRoute("GetSingleFriend", new { id = _friend.Id }, _friend);
        }

        /* PUT update the information on an event that already exists
        api/friend/5
        Arguments: Friend {
            "Sender": required User,
            "Receiver": required User,
            "Date": required string (formatted: YYYY-MM-DD),
            "Pending": bool
        } */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Friend _friend)
        {
            //checks to see if input is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != _friend.Id)
            {
                return BadRequest();
            }

            //try to update the specfic event
            _context.Friend.Update(_friend);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if id does not exist return BadRequest
                if (!FriendExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return the friend that was just updated
            return CreatedAtRoute("GetSingleFriend", id, _friend);
        }

        // DELETE api/friend/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Friend _friend = _context.Friend.Single(p => p.Id == id);

            if (_friend == null)
            {
                return NotFound();
            }
            
            _context.Friend.Remove(_friend);
            _context.SaveChanges();
            return Ok(_friend);

        }
            
        //checks to see if the Friend exists
        private bool FriendExists(int id)
        {
            return _context.Friend.Any(p => p.Id == id);
        }
    }
}