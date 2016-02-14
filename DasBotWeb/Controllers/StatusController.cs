using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DasBotModels;

namespace DasBotWeb.Controllers
{
    public class StatusController : ApiController
    {
        private readonly Activity[] data = {
                new Activity {Status = true, Name = ActivityType.Sailing, Description = "Segling"},
                new Activity {Status = false, Name = ActivityType.Fishing, Description = "Fiske"}
            };

        // GET api/<controller>
        public IEnumerable<Activity> Get()
        {
            return data;
        }


        //[Route("status/{statusName}")]
        //public Activity Get(string statusName)
        //{
        //    return data.Where(x => x.Name == (ActivityType)(statusName));
        //}


    }
}