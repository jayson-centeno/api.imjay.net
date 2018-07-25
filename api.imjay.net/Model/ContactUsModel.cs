using System;

namespace Api.Imjay.Net.Model
{
    public class ContactUsModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Verified { get; set; }
    }
}
