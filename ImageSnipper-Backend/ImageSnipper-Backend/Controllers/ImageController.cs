using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ImageSnipper_Backend.Controllers
{
    [Route("api/images")]
    public class ImageController : Controller
    {
        // GET api/images
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/images/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/images
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/images/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/images/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}