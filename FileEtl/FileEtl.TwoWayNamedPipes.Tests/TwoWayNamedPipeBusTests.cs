using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FileEtl.TwoWayNamedPipes.Tests
{
    public class TwoWayNamedPipeBusTests
    {
        [Fact]
        public void X()
        {
            var allText = "";
            var recieveCount = 0;
            var message = "";

            Task.Run(() =>
            {
                var connection1 = new TwoWayNamedPipesBus("Bus1");
                connection1.For<string>(x => allText += $"string:{x}")
                    .For<int>(x => allText += $"string:{x}")
                    .StartRecieving();

                while (recieveCount < 50)
                {
                    connection1.Send("test");
                    connection1.Send(11);
                }
            });

            Task.Run(() =>
            {
                var connection1 = new TwoWayNamedPipesBus("Bus1");
                connection1.For<string>(x => allText += $"string:{x}")
                    .For<int>(x => allText += $"string:{x}")
                    .StartRecieving();

                while (recieveCount < 50)
                {
                    connection1.Send("test2");
                    connection1.Send(1133);
                }
            });

            while (recieveCount < 50)
            {
                Thread.Sleep(10);
            }
            var result = allText;
        }
    }
}