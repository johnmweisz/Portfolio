using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Portfolio.Models
{
    public class Contact
    {
        [Required]
        public string Name {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Phone]
        public string Phone {get;set;}
        public string Message {get;set;}

    }
}