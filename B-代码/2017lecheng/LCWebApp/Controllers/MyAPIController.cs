using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LCWebApp.Controllers
{
    public class MyAPIController : ApiController
    {
        public IHttpActionResult Hello()
        {
            return Json("hello");
        }
    }
}
