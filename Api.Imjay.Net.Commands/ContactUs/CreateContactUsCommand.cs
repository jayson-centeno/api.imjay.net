using System;

namespace Api.Imjay.Net.Commands
{
    public class CreateContactUsCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Verified { get; set; }
        public string IPAddress { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
