using System.Threading;
using System.Threading.Tasks;
using Xunit;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace FileEtl.TwoWayNamedPipes.Tests
{
    public class NamedPipesTwoWayConnectionTests
    {
        [Fact]
        public void Test()
        {
            var allText = "";
            var recieveCount = 0;
            var message = "";

            Task.Run(() =>
            {
                using (var connection1 = new TwoWayNamedPipeConnection<string>("Test2", new NampedPipesLineReader(), new NamedPipesLineWriter()))
                {
                    connection1.Connect();
                    connection1.StartRecieving(x => { recieveCount += 1; message = x; allText += $"connection1Recieved:'{x}'"; });
                    while (recieveCount < 5)
                    {
                        connection1.Send("connection1");
                    }
                }
            });

            Task.Run(() =>
            {
                using (var connection2 = new TwoWayNamedPipeConnection<string>("Test2", new NampedPipesLineReader(), new NamedPipesLineWriter()))
                {
                    connection2.Connect();
                    connection2.StartRecieving(x => { recieveCount += 1; message = x; allText += $"connection2Recieved:'{x}'"; });
                    while (recieveCount < 5)
                    {
                        connection2.Send("connection2");
                    }
                }
            });

            while (recieveCount < 5)
            {
                Thread.Sleep(10);
            }
            var b = message;
            var result = allText;
        }
    }
}

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed