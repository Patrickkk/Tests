using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace RestberryPiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/GPIO")]
    public class GPIOPinController : Controller
    {
        private static IEnumerable<GpioPin> GpioPins = Pi.Gpio.Pins.Where(x => x.Capabilities.Contains(PinCapability.GP));

        // GET: api/GPIO
        [HttpGet]
        public IEnumerable<GpioPin> Get()
        {
            return Pi.Gpio.Pins.Where(x => x.Capabilities.Contains(PinCapability.GP));
        }

        // GET: api/GPIO/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            throw new NotImplementedException();
            return "value";
        }

        // POST: api/GPIO
        [HttpPost]
        public void Post([FromBody]int value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/GPIO/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]int value)
        {
            throw new NotImplementedException();
        }
    }
}