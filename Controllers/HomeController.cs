using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Newtonsoft.Json;
using MailKit.Net.Smtp;
using MimeKit;

namespace Portfolio.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpPost("[action]")]
        public IActionResult Inquire([FromBody] Contact contactform)
        {
            string isAuth = Request.Headers["Angular"];
            if(isAuth != null && isAuth == "secretkeylol")
            {
                if(ModelState.IsValid)
                {
                    MimeMessage message = new MimeMessage();

                    MailboxAddress from = new MailboxAddress($"{contactform.Name}", $"{contactform.Email}");
                    message.From.Add(from);

                    MailboxAddress to = new MailboxAddress("John Weisz", "johnmweisz@gmail.com");
                    message.To.Add(to);

                    if(contactform.Phone == null)
                    {
                        contactform.Phone = "No number included.";
                    }
                    message.Subject = $"New Contact from {contactform.Phone}";
                    
                    if(contactform.Message == null)
                    {
                        contactform.Message = "No message included.";
                    }
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = $"<p>{contactform.Message}</p>";
                    bodyBuilder.TextBody = $"{contactform.Message}";
                    message.Body = bodyBuilder.ToMessageBody();

                    message.Body = bodyBuilder.ToMessageBody();

                    //SmtpClient client = new SmtpClient();
                    //client.Connect("smtp.gmail.com", 465, true);
                    //client.Authenticate("johnmweisz@gmail.com", "xhrgwmcqnbpbfakm");

                    //client.Send(message);
                    //client.Disconnect(true);
                    //client.Dispose();

                    return Ok();
                }
                return BadRequest(Json(ModelState));
            }
            return BadRequest(401);
        }

        [HttpGet("[action]")]
        public IActionResult GetNumber()
        {
            string isAuth = Request.Headers["Angular"];
            if(isAuth != null && isAuth == "secretkeylol")
            {
            return Ok(JsonConvert.SerializeObject("949-874-0903"));
            }
            return BadRequest(401);
        }

    }
}
