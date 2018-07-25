using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Imjay.Net.Domain.Model
{
    [Table("ContactUs")]
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string IPAddress { get; set; }
        public bool? Approved { get; set; }
        public DateTime? DatePosted { get; set; }
    }
}
