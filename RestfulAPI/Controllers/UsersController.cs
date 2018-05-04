using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Models;
using RestfulAPI.Repositories;

namespace RestfulAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private UsersRepository repo = new UsersRepository();

        // GET api/users
        [HttpGet]
        public List<User> Get()
        {
            return repo.GetData();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
