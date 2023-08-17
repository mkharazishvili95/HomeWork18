using HomeWork18.Data;
using HomeWork18.Domain;
using HomeWork18.Models;
using HomeWork18.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork18.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _contex;
        public PersonController(PersonContext contex)
        {
            _contex = contex;
        }
        [Authorize]
        [HttpPost("CreatePerson")]
        public ActionResult<Person> CreatePerson(Person person)
        {
            var validator = new PersonValidator();
            var validatorResult = validator.Validate(person);
            if(!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            else
            {
                _contex.Add(person);
                _contex.SaveChanges();
                return Ok("Person has successfully created!");
            }
        }
        [Authorize]
        [HttpGet("GetPersonById/{id}")]
        public IActionResult GetPersonById(int id)
        {
            var person = _contex.Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound("There is no person with this ID!");
            }

            return Ok(person);
        }

        [Authorize]
        [HttpGet("GetAllPersons")]
        public ActionResult<IEnumerable<Person>> GetAllPersons()
        {
            return Ok(_contex.Persons);
        }
        [Authorize]
        [HttpGet("FilterByCity")]
        public ActionResult<IEnumerable<Person>> FilterByCity([FromBody] CityFilterModel filterModel)
        {
            if (string.IsNullOrEmpty(filterModel.City))
            {
                return BadRequest("City parameter is missing or empty.");
            }

            var filtered = _contex.Persons.Where(x => x.PersonAddress != null && x.PersonAddress.City != null && x.PersonAddress.City.ToUpper() == filterModel.City.ToUpper());

            return Ok(filtered);
        }



        [Authorize(Roles = Roles.Admin)]
        [HttpPut("UpdatePerson/{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
        {
            var person = _contex.Persons.Include(x => x.PersonAddress).SingleOrDefault(x => x.Id == id);
            if(person == null)
            {
                return NotFound("There is no any person by this ID to update!");
            }
            var validator = new PersonValidator();
            var validatorResult = validator.Validate(updatedPerson);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors!);
            }
            if(person.PersonAddress != null)
            {
                person.PersonAddress.Country = updatedPerson.PersonAddress.Country;
                person.PersonAddress.City = updatedPerson.PersonAddress.City;
                person.PersonAddress.HomeNumber = updatedPerson.PersonAddress.HomeNumber;
            }
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.JobPosition = updatedPerson.JobPosition;
            person.Salary = updatedPerson.Salary;
            person.WorkExperience = updatedPerson.WorkExperience;

            _contex.Persons.Update(person);
            _contex.SaveChanges();
            return Ok("Person has successfully updated!");
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("DeletePerson")]
        public IActionResult DeletePerson([FromBody] DeletePersonModel deleteModel)
        {
            if (deleteModel == null || deleteModel.Id <= 0)
            {
                return BadRequest("Invalid delete data.");
            }

            var person = _contex.Persons.Include(x => x.PersonAddress).SingleOrDefault(x => x.Id == deleteModel.Id);
            if (person == null)
            {
                return NotFound("There is no person with this ID.");
            }

            if (person.PersonAddress != null)
            {
                _contex.Addresses.Remove(person.PersonAddress);
            }

            _contex.Persons.Remove(person);
            _contex.SaveChanges();

            return Ok("Person has been successfully deleted.");
        }

    }
}
