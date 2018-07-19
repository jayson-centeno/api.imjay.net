using Api.Imjay.Net.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Imjay.Net.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        [EnableCors("*")]
        [HttpGet("[action]")]
        public IEnumerable<PublicationModel> GetPublications()
        {
            return new List<PublicationModel>() {
                new PublicationModel { Id = 1, Title = "C# Useful Extension Methods", Description = DateTime.Now.ToString("dddd, dd MMMM yyy") },
                new PublicationModel { Id = 2, Title = "Simple Coding Technique", Description = DateTime.Now.ToString("dddd, dd MMMM yyy") },
                new PublicationModel { Id = 3, Title = "Knockout JS Component Builder", Description = DateTime.Now.ToString("dddd, dd MMMM yyy") },
                new PublicationModel { Id = 4, Title = "Reactjs Basics", Description = DateTime.Now.ToString("dddd, dd MMMM yyy") },
                new PublicationModel { Id = 5, Title = "ReactJS Forms and Validations", Description = DateTime.Now.ToString("dddd, dd MMMM yyy") },
                new PublicationModel { Id = 5, Title = "Typescript Simple Code Tricks", Description = DateTime.Now.ToString("dddd, dd MMMM yyy") }
            };
        }

        [HttpPost]
        public string PostData([FromBody] UserAuthenticate user)
        {
            return string.Empty;
        }

    }
}