using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;

namespace RestberryPiApi.PinAccess
{
    public class UnoSquarePinsService : IPiPinsService
    {
        private static Unosquare.RaspberryIO.Gpio.GpioPin PhysicalPinOrDefault(int physicalPinNumber)
        {
            return Pi.Gpio.SingleOrDefault(x => x.HeaderPinNumber == physicalPinNumber);
        }

        private static Unosquare.RaspberryIO.Gpio.GpioPin PhysicalPin(int physicalPinNumber)
        {
            var pin = PhysicalPinOrDefault(physicalPinNumber);
            if (pin == null)
            {
                throw new NonExistingPinException($"No pin with pin number {physicalPinNumber} exsists");
            }
            return pin;
        }

        public bool ReadModeAndRead(int physicalPinNumber)
        {
            var pin1 = PhysicalPin(physicalPinNumber);
            pin1.PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Input;
            return pin1.Read();
        }

        public bool Read(int physicalPinNumber)
        {
            SetToReadMode(physicalPinNumber);
            return PhysicalPin(physicalPinNumber).Read();
        }

        public void SetToReadMode(int physicalPinNumber)
        {
            PhysicalPin(physicalPinNumber).PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Input;
        }

        public void SetPinOutputValue(int physicalPinNumber, bool value)
        {
            var pin1 = PhysicalPin(physicalPinNumber);
            pin1.PinMode = Unosquare.RaspberryIO.Gpio.GpioPinDriveMode.Output;
            pin1.Write(value);
        }

        public IEnumerable<GpioPin> GetAllPins()
        {
            return Pi.Gpio.Select(MapUnosquarePin).Concat(NonProgrammablePins.All).OrderBy(x => x.PhysicalPinNumber);
        }

        private GpioPin MapUnosquarePin(Unosquare.RaspberryIO.Gpio.GpioPin pin)
        {
            return new GpioPin
            {
                WiringPinNumber = (int)pin.WiringPiPinNumber,
                PhysicalPinNumber = pin.HeaderPinNumber,
                Name = pin.Name
            };
        }

        public GpioPin GetPin(int physicalPinNumber)
        {
            return MapUnosquarePin(PhysicalPin(physicalPinNumber));
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