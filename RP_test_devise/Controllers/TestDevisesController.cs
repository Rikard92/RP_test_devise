#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RP_test_devise.Models;

namespace RP_test_devise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDevisesController : ControllerBase
    {
        private readonly TestContext _context;

        public TestDevisesController(TestContext context)
        {
            _context = context;
        }

        // GET: api/TestDevises
        // The GET enpoint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestDeviseDTO>>> GettestDevises()
        {
            //returns all TestDevises that exist in the context
            return await _context.testDevises.Select(x => TestToDTO(x)).ToListAsync();
        }

        // GET: api/TestDevises/5
        // The GET enpoint with id 
        [HttpGet("{id}")]
        public async Task<ActionResult<TestDeviseDTO>> GetTestDevise(long id)
        {
            //find the TestDevise that matches the id
            var testDevise = await _context.testDevises.FindAsync(id);
            //in case the TestDevise dont matches the id
            if (testDevise == null)
            {
                return NotFound();
            }
            //returns the TestDevise that matches the id
            return TestToDTO(testDevise);
        }

        // PUT: api/TestDevises/5
        // The PUT enpoint using id to update a singular object
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestDevise(long id, TestDeviseDTO testDeviseDTO)
        {
            if (id != testDeviseDTO.Id)
            {
                return BadRequest();
            }
            //Check if the TestDevise allready exist
            var testDevise = await _context.testDevises.FindAsync(id);
            if(testDevise == null)
            {
                return NotFound();
            }
            //changing all variables to the new data
            testDevise.Amount = testDeviseDTO.Amount;
            testDevise.Name = testDeviseDTO.Name;
            testDevise.IsComplete = testDeviseDTO.IsComplete;
            //Save the changes in the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestDeviseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TestDevises
        // The POST enpoint to create object
        [HttpPost]
        public async Task<ActionResult<TestDevise>> PostTestDevise(TestDeviseDTO testDeviseDTO)
        {
            //Create the New TestDevise with the relevant data
            var testDevise = new TestDevise()
            {
                Id = testDeviseDTO.Id,
                Amount = testDeviseDTO.Amount,
                Name = testDeviseDTO.Name,
                IsComplete = testDeviseDTO.IsComplete

            };
            //Add the New TestDevise to the context
            _context.testDevises.Add(testDevise);
            await _context.SaveChangesAsync();
            //Returns the testDevise that was created
            return CreatedAtAction(nameof(GetTestDevise), new { id = testDevise.Id }, TestToDTO(testDevise));
        }

        // DELETE: api/TestDevises/5
        // The DELETE enpoint using id to remove a singular object
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestDevise(long id)
        {
            var testDevise = await _context.testDevises.FindAsync(id);
            if (testDevise == null)
            {
                return NotFound();
            }

            _context.testDevises.Remove(testDevise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestDeviseExists(long id)
        {
            return _context.testDevises.Any(e => e.Id == id);
        }
        //This functions converts a TestDevise into a DTO in order to prevent over-posting.
        private static TestDeviseDTO TestToDTO(TestDevise testDevise) => 
            new TestDeviseDTO            
            {
                Id = testDevise.Id,
                Name = testDevise.Name,
                IsComplete = testDevise.IsComplete,
                Amount = testDevise.Amount
            };
        
    }
}
