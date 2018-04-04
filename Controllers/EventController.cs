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
    public class EventController : Controller
    {
        //instance of the application context
        private ApplicationDbContext _context;

        public EventController(ApplicationDbContext ctx){
            _context = ctx;
        }
        // GET api/event
        //returns list of events
        [HttpGet]
        public IActionResult Get()
        {
            List<Event> _events = _context.Event.ToList();
            if (_events == null)
            {
                return NotFound();
            }
            return Ok(_events);
        }

        // GET api/event/5
        //gets single event
        [HttpGet("{id}", Name = "GetSingleEvent")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Event _event = _context.Event.Single(p => p.Id == id);

                if (_event == null)
                {
                    return NotFound();
                }

                return Ok(_event);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/event
        /* POST event to database 
        Arguments: Event {
            "User": required User,
            "Title": required string,
            "Rating": required int,
            "Date": required string (formatted: YYYY-MM-DD),
            "Description": string
            "Private": bool
        }*/
        [HttpPost]
        public IActionResult Post([FromBody]Event _event)
        {
            if (!ModelState.IsValid)
            {
                //if not valid data according to conditions then return the error
                return BadRequest(ModelState);
            }

            _context.Event.Add(_event);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EventExists(_event.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //return the event that was just added
            return CreatedAtRoute("GetSingleEvent", new { id = _event.Id }, _event);
        }

        // PUT api/event/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/event/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
            
        //checks to see if the Event exists
        private bool EventExists(int id)
        {
            return _context.Event.Any(p => p.Id == id);
        }
    }
}