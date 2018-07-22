using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Linq;

using Unosquare.RaspberryIO;

namespace RestberryPiApi.PinAccess
{
    public class FakePinsService : IPiPinsService
    {
        private readonly Dictionary<int, IFaKePinValueProvider> pinValueProviders = Enumerable.Range(0, 40).ToDictionary(x => x, x => (IFaKePinValueProvider)new RandomPinProvider());

        private readonly List<GpioPin> allPins = new List<GpioPin>
        {
            new GpioPin{ PinType = GpioPinGroup.I2C,PhysicalPinNumber = 3, Name = "GPIO 02 I2C1 SDA1", WiringPinNumber = 8 },
            new GpioPin{ PinType = GpioPinGroup.I2C,PhysicalPinNumber = 5, Name = "GPIO 03 I2C1 SCL", WiringPinNumber = 9 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 7, Name = "GPIO 7", WiringPinNumber = 7 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 11, Name = "SPI1 CS0", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 12, Name = "GPIO 18", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 13, Name = "GPIO 27", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 15, Name = "GPIO 22", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 16, Name = "GPIO 23", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 18, Name = "GPIO 24", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.SPI,PhysicalPinNumber = 19, Name = "SPIO MOSI", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.SPI,PhysicalPinNumber = 21, Name = "SPIO MISO", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 22, Name = "GPIO 25", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.SPI,PhysicalPinNumber = 23, Name = "SPIO SCLK", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.SPI,PhysicalPinNumber = 24, Name = "SPIO CS0", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.SPI,PhysicalPinNumber = 26, Name = "SPIO CS1", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 27, Name = "GPIO 0", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 28, Name = "GPIO 1", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 29, Name = "GPIO 5", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 31, Name = "GPIO 6", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 32, Name = "GPIO 12", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 33, Name = "GPIO 13", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.SPI,PhysicalPinNumber = 35, Name = "SPI1 MISO", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 36, Name = "GPIO 16", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Gpio,PhysicalPinNumber = 37, Name = "GPIO 26", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.SPI,PhysicalPinNumber = 38, Name = "SPI1 MOSI", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.SPI,PhysicalPinNumber = 40, Name = "SPI1 SLCK", WiringPinNumber = -1 },
        };

        public FakePinsService(IOptions<List<FakePinConfiguration>> pinConfiguration)
        {
            var setupDictionairy = pinConfiguration.Value.ToDictionary(x => x.pinNumber, x => (IFaKePinValueProvider)new RandomPinProvider());
            setupDictionairy.ToList().ForEach(x => pinValueProviders[x.Key] = x.Value);
        }

        public void ConfigureProviderForPin(int pinNumber, IFaKePinValueProvider valueProvider)
        {
            pinValueProviders[pinNumber] = valueProvider;
        }

        public IEnumerable<GpioPin> GetAllPins()
        {
            return allPins.Concat(NonProgrammablePins.All).OrderBy(x => x.PhysicalPinNumber);
        }

        public GpioPin GetPin(int id)
        {
            throw new NotImplementedException();
        }

        public bool Read(int pinNumber)
        {
            return this.pinValueProviders[pinNumber].Read();
        }

        public bool ReadModeAndRead(int pinNumber)
        {
            return this.pinValueProviders[pinNumber].Read();
        }

        public void SetPinOutputValue(int id, bool value)
        {
            Console.WriteLine($"set pin {id} to {value}");
        }

        public void SetToReadMode(int pinNumber)
        {
            // no pins to set since this is all fake.
        }
    }

    public interface IFaKePinValueProvider
    {
        bool Read();
    }

    public class RandomPinProvider : IFaKePinValueProvider
    {
        private readonly Random random;

        public RandomPinProvider()
        {
            this.random = new Random();
        }

        public bool Read()
        {
            return Convert.ToBoolean(random.Next(2) - 1);
        }
    }
}