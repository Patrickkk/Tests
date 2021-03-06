﻿using Microsoft.AspNetCore.Mvc;
using RestberryPiApi.PinAccess;
using System;
using System.Collections.Generic;

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
            return pinsService.GetPin(id);
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
            var currentvalue = this.pinsService.Read(id);
            this.pinsService.SetPinOutputValue(id, !currentvalue);
        }

        // PUT: api/GPIO/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]int value)
        {
            throw new NotImplementedException();
        }
    }
}