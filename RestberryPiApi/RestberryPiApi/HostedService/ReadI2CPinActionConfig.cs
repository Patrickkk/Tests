﻿namespace RestberryPiApi.HostedService
{
    internal class ReadI2CPinActionConfig
    {
        public string Name { get; set; }
        public int I2cAddress { get; internal set; }
    }
}