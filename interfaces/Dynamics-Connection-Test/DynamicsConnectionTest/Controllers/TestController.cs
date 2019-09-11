using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Jag.Spice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicsConnectionTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDynamicsClient _dynamicsClient;

        public TestController(IDynamicsClient dynamicsClient)
        {
            _dynamicsClient = dynamicsClient;
        }


        // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> result = new List<string>();
            try
            {
                /*
                var list = _dynamicsClient.Govministries.Get().Value;
                foreach (var item in list)
                {
                    result.Add(item.SpiceName);
                }
                */

                var list = _dynamicsClient.Accounts.Get().Value;
                foreach (var item in list)
                {
                    result.Add(item.Name);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        
    }
}
