using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestberryPiApi.PinAccess
{
    public interface IPiPinsService
    {
        void SetToReadMode(int pinNumber);

        bool Read(int pinNumber);

        bool ReadModeAndRead(int pinNumber);

        void SetPinOutputValue(int id, bool value);

        IEnumerable<GpioPin> GetAllPins();

        GpioPin GetPin(int id);
    }

    public class GpioPin
    {
        public int HeaderPinNumber { get; set; }

        public int WiringPinNumber { get; set; }

        public string Name { get; set; }

        public GpioPinGroup PinType { get; set; }
    }

    public enum GpioPinGroup
    {
        Default,
        Gpio,
#pragma warning disable S4016 // Enumeration members should not be named "Reserved" reason: This is the name no the pi, it will not change
        Reserved,
#pragma warning restore S4016 // Enumeration members should not be named "Reserved"
        ThreeVolt,
        FiveVolt,
        Ground,
        SPI,
        I2C,
    }
}