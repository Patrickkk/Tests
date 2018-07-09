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
        private readonly Dictionary<int, IFaKePinValueProvider> pinValueProviders;

        public FakePinsService(IOptions<List<FakePinConfiguration>> pinConfiguration)
        {
            pinValueProviders = pinConfiguration.Value.ToDictionary(x => x.pinNumber, x => (IFaKePinValueProvider)new RandomPinProvider());
        }

        public void ConfigureProviderForPin(int pinNumber, IFaKePinValueProvider valueProvider)
        {
            pinValueProviders[pinNumber] = valueProvider;
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
            return Convert.ToBoolean(random.Next(1));
        }
    }
}