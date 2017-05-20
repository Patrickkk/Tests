using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FileEtl.TwoWayNamedPipes.Tests
{
    public class TwoWayNamedPipesConnectionwithJsonTest

    {
        [Fact]
        public void X()
        {
            var allText = "";
            var recieveCount = 0;
            var message = "";

            Task.Run(() =>
            {
                using (var connection1 = new TwoWayNamedPipeConnection<TestMessage>("Test1", new NamedPipesJsonReader<TestMessage>(), new NamedPipeJsonWriter<TestMessage>()))
                {
                    connection1.Connect();
                    connection1.StartRecieving(x => { recieveCount += 1; message += x.Name; allText += $"connection1Recieved:'{x.Number}-{x.Name}{Environment.NewLine}'"; });
                    while (recieveCount < 50)
                    {
                        connection1.Send(new TestMessage { Name = "SendFrom1", Number = recieveCount });
                    }
                }
            });

            Task.Run(() =>
            {
                using (var connection2 = new TwoWayNamedPipeConnection<TestMessage>("Test1", new NamedPipesJsonReader<TestMessage>(), new NamedPipeJsonWriter<TestMessage>()))
                {
                    connection2.Connect();
                    connection2.StartRecieving(x => { recieveCount += 1; message += x.Name; allText += $"connection2Recieved:'{x.Number}-{x.Name}{Environment.NewLine}'"; });
                    while (recieveCount < 50)
                    {
                        connection2.Send(new TestMessage { Name = "SendFrom2", Number = recieveCount });
                    }
                }
            });

            while (recieveCount < 50)
            {
                Thread.Sleep(10);
            }
            var b = message;
            var result = allText;
        }
    }

    public class TestMessage
    {
        public string Name { get; set; }

        public int Number { get; set; }
    }
}