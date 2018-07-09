using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestberryPiApi.PinAccess;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace RestberryPiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/GPIO")]
    public class GpioPinController : Controller
    {
        private readonly IPiPinsService pinsService;

        public GpioPinController(IPiPinsService pinsService)
        {
            this.pinsService = pinsService;
        }

        [HttpGet]
        public IEnumerable<GpioPin> Get()
        {
            return pinsService.GetAllPins();
        }

        // GET: api/GPIO/5
        [HttpGet("{id}", Name = "Get")]
        public GpioPin Get(int id)
        {
            return Pi.Gpio.Pins[id];
        }

        [HttpGet("Read/{id}")]
        public bool Read(int id)
        {
            return pinsService.ReadModeAndRead(id);
        }

        // POST: api/GPIO
        [HttpPost("{id}")]
        public void Post(int id, [FromBody]bool value)
        {
            pinsService.SetPinOutputValue(id, value);
        }

        [HttpPost("toggle/{id}")]
        public void Toggle(int id)
        {
            var pin1 = Pi.Gpio[id];
            var currentvalue = pin1.Read();
            pin1.PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Output;
            pin1.Write(!currentvalue);
        }

        // PUT: api/GPIO/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]int value)
        {
            throw new NotImplementedException();
        }
    }
}