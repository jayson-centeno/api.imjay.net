using System;
using System.Collections.Generic;
using Api.Imjay.Net.Domain.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Imjay.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;

        public ValuesController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        // GET api/values
        [HttpGet]
        [EnableCors("*")]
        public ActionResult<IEnumerable<string>> Get()
        {
            _contactUsService.CreateContactUs(new Commands.CreateContactUsCommand()
            {
                Email = "jj@j.com",
                Message = "test",
                Name = "test",
                Verified = true,
                IPAddress = GetUserIP(),
                DatePosted = DateTime.Now
            });

            return new string[] { "value1", "value2" };
        }

        private string GetUserIP()
        {
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
