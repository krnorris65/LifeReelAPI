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

        /* PUT update the information on an event that already exists
        api/event/5
        Arguments: Event {
            "User": required User,
            "Title": required string,
            "Rating": required int,
            "Date": required string (formatted: YYYY-MM-DD),
            "Description": string
            "Private": bool
        } */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Event _event)
        {
            //checks to see if input is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != _event.Id)
            {
                return BadRequest();
            }

            //try to update the specfic event
            _context.Event.Update(_event);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if id does not exist return BadRequest
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return the event that was just updated
            return CreatedAtRoute("GetSingleEvent", id, _event);
        }

        // DELETE api/event/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Event _event = _context.Event.Single(p => p.Id == id);

            if (_event == null)
            {
                return NotFound();
            }
            
            _context.Event.Remove(_event);
            _context.SaveChanges();
            return Ok(_event);

        }
            
        //checks to see if the Event exists
        private bool EventExists(int id)
        {
            return _context.Event.Any(p => p.Id == id);
        }
    }
}