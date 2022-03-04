using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;




namespace Kamen.Utility
{
    public class EmailSender : IEmailSender
    {
        //public ApiVersion Version = ApiVersion.V3_1;

        private readonly IConfiguration _configuration;

        public  MailJetSettings _mailjetSettings { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        { 
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            _mailjetSettings = _configuration.GetSection("MailJet").Get<MailJetSettings>();

            MailjetClient client = new MailjetClient(_mailjetSettings.ApiKey, _mailjetSettings.SecretKey)
            {
                
            };

            MailjetRequest request = new MailjetRequest
            {
                    Resource = Send.Resource,
            }
            
            .Property(Send.Messages, new JArray { new JObject {
                  {  "From", new JObject
                    {
                        {"Email", "dunjoman@protonmail.com"},
                        {"Name", "Dunja"}
                    }
                  },

                  {   "To", new JArray
                    { new JObject
                      {
                         { "Email", email },
                         { "Name", "DotNetMastery" }
                      }
                    }
                  },

                  { "Subject", subject},
                  { "HTMLPart", body}
                 }
              });

            await client.PostAsync(request);
        }
    }

}

