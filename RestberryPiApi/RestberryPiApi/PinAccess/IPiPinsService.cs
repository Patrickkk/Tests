using System.Collections.Generic;

namespace RestberryPiApi.PinAccess
{
    public interface I2cService
    {
    }

    public interface IPiPinsService
    {
        void SetToReadMode(int physicalPinNumber);

        bool Read(int physicalPinNumber);

        bool ReadModeAndRead(int physicalPinNumber);

        void SetPinOutputValue(int physicalPinNumber, bool value);

        IEnumerable<GpioPin> GetAllPins();

        GpioPin GetPin(int physicalPinNumber);
    }

    public class GpioPin
    {
        public int PhysicalPinNumber { get; set; }

        public int HeaderPinNumber { get { return PhysicalPinNumber; } }

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