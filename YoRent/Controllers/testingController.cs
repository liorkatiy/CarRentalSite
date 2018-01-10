using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YoRent.Controllers
{
    
    public class testingController : ApiController
    {
        // GET: api/testing
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/testing/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/testing
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/testing/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/testing/5
        public void Delete(int id)
        {
        }
    }
}
