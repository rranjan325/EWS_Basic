using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using SearchEmailAPI.Models;

namespace SearchEmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SearchEmailController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(String.Empty);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SearchCriteria criteria)
        {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);

            service.Credentials = new WebCredentials(criteria.UserEmail, criteria.Password);
            
            service.Url = new Uri(criteria.ExchangeUrl);

            //Get all mail items
            FindItemsResults<Item> mailItems = service.FindItems(WellKnownFolderName.Inbox, new ItemView(int.MaxValue));

            //Get filtered result after applying date range and subject pattern
            List<Item> filtereditems = mailItems.Where(mail => mail.DateTimeReceived >= criteria.FromDate && 
            mail.DateTimeReceived <= criteria.ToDate &&
            Regex.IsMatch(mail.Subject, criteria.SubjectPattern) == true).ToList();

            //arranging in Descending order to show latest result because we are showing only 10 items
            filtereditems = filtereditems.OrderByDescending(item => item.DateTimeReceived).ToList();

            List<EmailItem> finalResult = new List<EmailItem>();

            foreach (Item message in filtereditems)
            {
               
                        finalResult.Add(new EmailItem
                        {
                            Id = message.Id.UniqueId,
                            DateTimeReceived = message.DateTimeReceived,
                            DisplayCc = message.DisplayCc,
                            DisplayTo = message.DisplayTo,
                            Subject = message.Subject,
                            Sender = ((EmailMessage) message).Sender.Name
                        }) ;

                //limit result to 10
                if (finalResult.Count == 10)
                    break;
            }            

            return Ok(finalResult);
        }
    }
}
