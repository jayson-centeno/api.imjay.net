using Api.Imjay.Net.Domain.Interface;
using Api.Imjay.Net.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Api.Imjay.Net.Commands;

namespace JCLite.Controllers.API
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("*")]
    public class ContactController : Controller
    {
        private readonly IContactUsService _contactUsService;

        public ContactController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [HttpPost]
        public async Task<bool> Post([FromBody]ContactUsModel contact)
        {
            await _contactUsService.CreateContactUs(new CreateContactUsCommand()
            {
                Email = contact.Email,
                Message = contact.Message,
                Name = contact.Name,
                Verified = contact.Verified,
                IPAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                DatePosted = DateTime.Now
            });

            return true;
        }
    }
}