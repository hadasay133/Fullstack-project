using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GeneralLogic
    {
        public static async Task<RestResponse> SendSimpleMessage()
        {


            MailjetClient client = new MailjetClient("886a208b3085c050898c00eb5ddaac7f", "8e4ef3cab37f806a38921386dccbfa3d")
            {
                
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,

            };
            // construct your email with builder
            var email = new TransactionalEmailBuilder()
                   .WithFrom(new SendContact("safe.driving.2022@gmail.com"))
                   .WithSubject("Test subject")
                   .WithHtmlPart("<h1>Heawwwder</h1>")
                   .WithTo(new SendContact("safe.driving.2022@gmail.com"))
                   .Build();

            // invoke API to send email
            var response = await client.SendTransactionalEmailAsync(email);
            var yt = 1;
            // check response
         //   Assert.AreEqual(1, response.Messages.Length);

     //        .Property(Send.Messages, new JArray {
     //new JObject {
     // {
     //  "From",
     //  new JObject {
     //   {"Email", "safe.driving.2022@gmail.com"},
     //   {"Name", "Safe Driving"}
     //  }
     // }, {
     //  "To",
     //  new JArray {
     //   new JObject {
     //    {
     //     "Email",
     //     "safe.driving.2022@gmail.com"
     //    }, {
     //     "Name",
     //     "Safe"
     //    }
     //   }
     //  }
     // }, {
     //  "Subject",
     //  "Greetings from Mailjet."
     // }, {
     //  "TextPart",
     //  "My first Mailjet email"
     // }, {
     //  "HTMLPart",
     //  "<h3>Dear passenger 1, welcome to <a href='https://www.mailjet.com/'>Mailjet</a>!</h3><br />May the delivery force be with you!"
     // }, {
     //  "CustomID",
     //  "AppGettingStartedTest"
     // }
     //}
     //        });
     //       MailjetResponse response = await client.PostAsync(request);
            var y = 5;





            //RestClient client = new RestClient(new Uri("https://api.mailgun.net/v3/sandboxbc117b471dee4329a86c8b00bae3cfc1.mailgun.org/messages"));



            //new HttpBasicAuthenticator("api",
            //     "bc60caec0269ca9ffcd7e1f20d36d753-100b5c8d-fe9c17f6");
            //RestRequest request = new RestRequest();
            //request.AddParameter("domain", "sandboxbc117b471dee4329a86c8b00bae3cfc1.mailgun.org", ParameterType.UrlSegment);
            //request.Resource = "{domain}/messages";
            //request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxbc117b471dee4329a86c8b00bae3cfc1.mailgun.org>");
            //request.AddParameter("to", "Safe Driving <safe.driving.2022@gmail.com>");


            ////   request.AddParameter("from", "Excited User <mailgun@YOUR_DOMAIN_NAME>");
            ////  request.AddParameter("to", "ayalay062@gmail.com");
            //request.AddParameter("subject", "Hello");
            //request.AddParameter("text", "Testing some Mailgun awesomness!");
            //request.Method = Method.Post;
            //var x = await client.ExecuteAsync(request);
            return null;
        }


        public static async Task SendEmailMessage(string to, string messageHTML, string subject)
        {
            MailjetClient client = new MailjetClient("886a208b3085c050898c00eb5ddaac7f", "8e4ef3cab37f806a38921386dccbfa3d")
            {

            };
                    
            // construct your email with builder
            var email = new TransactionalEmailBuilder()
                   .WithFrom(new SendContact("safe.driving.2022@gmail.com","Safe Driving"))
                   .WithSubject(subject)
                   .WithHtmlPart(messageHTML)
                   .WithTo(new SendContact(to))
                   .Build();

            // invoke API to send email
            var response = await client.SendTransactionalEmailAsync(email);
   
        }



        public static async Task sendEmailAsync(string to, string subject, string body)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://zerobounce1.p.rapidapi.com/v2/validate?ip_address=replace_the_IP_address_the_email_signed_up_from&email=ayalay062@gmsi"),
                Headers =
    {
        { "X-RapidAPI-Host", "zerobounce1.p.rapidapi.com" },
        { "X-RapidAPI-Key", "e39908cb93msh335c714f60c813bp10bd8ajsnec87131c54f5" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var bodyr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(bodyr);
            }
            //string fromaddr = "m0583202944@gmail.com";

            //string password = "matti2944";

            //MailMessage msg = new MailMessage();
            //msg.Subject = subject;
            //msg.From = new MailAddress(fromaddr);
            //msg.Body = body;
            //msg.IsBodyHtml = true;
            //msg.To.Add(new MailAddress(to));
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = true;
            //NetworkCredential nc = new NetworkCredential(fromaddr, password);
            //smtp.Credentials = nc;
            //smtp.Send(msg);
        }

    }
}
