using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Newtonsoft.Json;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace Portfolio.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
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
                    message.ReplyTo.Add(from);

                    MailboxAddress to = new MailboxAddress("John Weisz", Configuration["EmailInfo:Email"]);
                    message.To.Add(to);

                    if(contactform.Phone == null)
                    {
                        contactform.Phone = "No number included.";
                    }
                    message.Subject = $"{contactform.Name} Wants to Connect!";
                    
                    if(contactform.Message == null)
                    {
                        contactform.Message = "No message included.";
                    }
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = $"<p>{contactform.Message}</p><p>{contactform.Email}</p><p>{contactform.Phone}</p>";
                    bodyBuilder.TextBody = $"{contactform.Message} \n{contactform.Email} \n{contactform.Phone}";
                    message.Body = bodyBuilder.ToMessageBody();

                    message.Body = bodyBuilder.ToMessageBody();

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate(Configuration["EmailInfo:Email"], Configuration["EmailInfo:Password"]);

                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();

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
            return Ok(JsonConvert.SerializeObject(Configuration["EmailInfo:Cell"]));
            }
            return BadRequest(401);
        }

    }
}
