using Microsoft.AspNetCore.Mvc;
using RestberryPiApi.PinAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestberryPiApi.Controllers
{
    [Route("api/unosquare/GPIO")]
    public class UnosquareGpiocontroller : Controller
    {
        private readonly UnoSquarePinsService unoSquarePinsService;

        public UnosquareGpiocontroller(UnoSquarePinsService unoSquarePinsService)
        {
            this.unoSquarePinsService = unoSquarePinsService;
        }

        [HttpGet()]
        public IEnumerable<Unosquare.RaspberryIO.Gpio.GpioPin> Get()
        {
            return this.unoSquarePinsService.AllUnosquarePins();
        }
    }
}