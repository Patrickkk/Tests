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
    }

    public class GpioPin
    {
        public int HeaderPinNumber { get; set; }

        public int WiringPinNumber { get; set; }

        public string Name { get; set; }
    }
}