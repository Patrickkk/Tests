using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeAsCommandLine.Tests.TestInput
{
    public class InstanceTestClass
    {
        private readonly string echoValue;

        public InstanceTestClass(string echoValue)
        {
            this.echoValue = echoValue;
        }

        public void WithoutParameters()
        {
            Console.WriteLine(this.echoValue + " no parameters");
        }

        public async Task WithoutParametersAsync()
        {
            Console.WriteLine(this.echoValue + " no parameters async");
            await Task.Delay(1000);
            Console.WriteLine(this.echoValue + " no parameters post delay async");
        }

        public void WithParameters(string value)
        {
            Console.WriteLine(this.echoValue + " parameters " + value);
        }
    }
}