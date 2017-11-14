using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static List<Person> persons = new List<Person> { new Person { Id=1, Name="Martin", Age=40, Height=1.85f, DriversLicense=true },
            new Person { Id=2, Name="Kalle", Age=12, Height=1.45f, DriversLicense=false } };

        // GET api/values
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return persons;
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "Values")]
        public IActionResult Get(int id)
        {
            var item = persons.Where(p => p.Id == id).FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Person value)
        {
            value.Id = persons.Count + 1;
            persons.Add(value);

            return CreatedAtRoute("Values", new { id = value.Id }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Person value)
        {
            Person old = persons.Where(p => p.Id == id).FirstOrDefault();
            if (old == null)
            {
                return NotFound();
            }

            old.Name = value.Name;
            old.Age = value.Age;
            old.Height = value.Height;
            old.DriversLicense = value.DriversLicense;

            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Person item = persons.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            persons.Remove(item);
            return new NoContentResult();
        }
    }
}
