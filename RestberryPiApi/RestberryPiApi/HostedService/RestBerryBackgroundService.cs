﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace RestberryPiApi.HostedService
{
    public class RestBerryBackgroundService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
        }
    }
}