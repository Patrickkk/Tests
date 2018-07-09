using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;

namespace RestberryPiApi.PinAccess
{
    public class UnoSquarePinsService : IPiPinsService
    {
        public bool ReadModeAndRead(int pinNumber)
        {
            var pin1 = Pi.Gpio[pinNumber];
            pin1.PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Input;
            return pin1.Read();
        }

        public bool Read(int pinNumber)
        {
            return Pi.Gpio[pinNumber].Read();
        }

        public void SetToReadMode(int pinNumber)
        {
            Pi.Gpio[pinNumber].PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Input;
        }

        public void SetPinOutputValue(int id, bool value)
        {
            var pin1 = Pi.Gpio[id];
            pin1.PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Output;
            pin1.Write(value);
        }

        public IEnumerable<GpioPin> GetAllPins()
        {
            return Pi.Gpio.Select(MapUnosquarePin);
        }

        private GpioPin MapUnosquarePin(Unosquare.RaspberryIO.Gpio.GpioPin pin)
        {
            return new GpioPin
            {
                WiringPinNumber = (int)pin.WiringPiPinNumber,
                HeaderPinNumber = pin.HeaderPinNumber,
                Name = pin.Name
            };
        }
    }

    public class UnosquareI2CService
    {
        public IEnumerable<I2CDevice> ListDevies()
        {
            return Pi.I2C.Devices.Select(x => new I2CDevice { Id = x.DeviceId, FileDescriptor = x.FileDescriptor });
        }

        public void Write(int id, byte data)
        {
            Pi.I2C.GetDeviceById(id).Write(data);
        }

        public void WriteAddressByte(int id, int address, byte data)
        {
            Pi.I2C.GetDeviceById(id).WriteAddressByte(address, data);
        }

        public void WriteAddressWord(int id, int address, ushort word)
        {
            Pi.I2C.GetDeviceById(id).WriteAddressWord(address, word);
        }

        public byte Read(int id)
        {
            return Pi.I2C.GetDeviceById(id).Read();
        }

        public byte ReadAddressByte(int id, int address)
        {
            return Pi.I2C.GetDeviceById(id).ReadAddressByte(address);
        }

        public ushort Write(int id, int address)
        {
            return Pi.I2C.GetDeviceById(id).ReadAddressWord(address);
        }
    }

#pragma warning disable S101 // Types should be named in camel case

    public class I2CDevice
#pragma warning restore S101 // Types should be named in camel case
    {
        public int Id { get; set; }

        public int FileDescriptor { get; set; }
    }
}