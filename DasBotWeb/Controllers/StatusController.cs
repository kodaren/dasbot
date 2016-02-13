using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DasBotWeb.Models;

namespace DasBotWeb.Controllers
{
    public class StatusController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Activity> Get()
        {
            return new [] {new Activity {Status = true, Name = ActivityType.Sailing, Description = "Segling"}};
        }

        
        // GET api/<controller>/5
        //public Activity Get(int id)
        //{
        //    return "value";
        //}

      
    }
}