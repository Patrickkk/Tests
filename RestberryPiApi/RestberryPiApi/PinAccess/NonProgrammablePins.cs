using System.Collections.Generic;

namespace RestberryPiApi.PinAccess
{
    public class NonProgrammablePins
    {
        public static readonly List<GpioPin> All = new List<GpioPin>
        {
            new GpioPin{ PinType = GpioPinGroup.ThreeVolt,PhysicalPinNumber = 1, Name = "3.3V", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.FiveVolt,PhysicalPinNumber = 2, Name = "5V", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.FiveVolt,PhysicalPinNumber = 4, Name = "5V", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Ground,PhysicalPinNumber = 6, Name = "Ground", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Reserved,PhysicalPinNumber = 8, Name = "Reserved", WiringPinNumber = 15 },
            new GpioPin{ PinType = GpioPinGroup.Ground,PhysicalPinNumber = 9, Name = "Ground", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Reserved,PhysicalPinNumber = 10, Name = "Reserved", WiringPinNumber = 16 },
            new GpioPin{ PinType = GpioPinGroup.Ground,PhysicalPinNumber = 14, Name = "Ground", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.ThreeVolt,PhysicalPinNumber = 17, Name = "3.3V", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Ground,PhysicalPinNumber = 20, Name = "Ground", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Ground,PhysicalPinNumber = 25, Name = "Ground", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Ground,PhysicalPinNumber = 30, Name = "Ground", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Ground,PhysicalPinNumber = 34, Name = "Ground", WiringPinNumber = -1 },
            new GpioPin{ PinType = GpioPinGroup.Ground,PhysicalPinNumber = 39, Name = "Ground", WiringPinNumber = -1 },
        };
    }
}