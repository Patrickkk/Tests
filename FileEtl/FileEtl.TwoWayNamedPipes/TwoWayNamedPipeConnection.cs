using System;
using System.IO;
using System.Threading.Tasks;

namespace FileEtl.TwoWayNamedPipes
{
    public class TwoWayNamedPipeConnection<T> : IDisposable
    {
        private readonly INamedPipesWriter<T> writer;
        private readonly INamedPipesReader<T> reader;
        private const string SecondairyConnectionName = "Secondairy";
        private const string InitialconnectionName = "Initial";

        public string Name { get; private set; }

        public Action<string> RecieveAction { get; private set; }

        internal ClientNamedPipeConnection<T> client { get; private set; }

        internal ServerNamedPipeConnection<T> Server { get; private set; }

        public TwoWayNamedPipeConnection(string name, INamedPipesReader<T> reader, INamedPipesWriter<T> writer)
        {
            this.Name = name;
            this.reader = reader;
            this.writer = writer;
        }

        public void Connect()
        {
            var connected = false;
            while (!connected)
            {
                try
                {
                    try
                    {
                        ConectToExsistingServerConnection();
                        connected = true;
                    }
                    catch (TimeoutException)
                    {
                        this.Server = null;
                        this.client = null;
                        SetupServerConnection();
                        connected = true;
                    }
                }
                catch (IOException ex)
                {
                    this.Server = null;
                    this.client = null;
                }
            }
            SetupSecondairyConnection();
        }

        private void SetupSecondairyConnection()
        {
            if (InitialConnectionStartedAsServer())
            {
                Server.WaitForConnection();

                // GiveSecondairyServerTimeToStart(); probably not even required...
                client = new ClientNamedPipeConnection<T>(GetSecondairyConnectionName(), this.writer);
                client.Connect();
            }
            else
            {
                Server = new ServerNamedPipeConnection<T>(GetSecondairyConnectionName(), this.reader);
                Server.WaitForConnection();
            }
        }

        //private static void GiveSecondairyServerTimeToStart()
        //{
        //    Thread.Sleep(100);
        //}

        private bool InitialConnectionStartedAsServer()
        {
            return client == null;
        }

        private string GetSecondairyConnectionName()
        {
            return $"{Name}-{SecondairyConnectionName}";
        }

        private void SetupServerConnection()
        {
            this.Server = new ServerNamedPipeConnection<T>(GetInitialConnectionName(), reader);
        }

        private string GetInitialConnectionName()
        {
            return $"{Name}-{InitialconnectionName}";
        }

        private void ConectToExsistingServerConnection()
        {
            this.client = new ClientNamedPipeConnection<T>(GetInitialConnectionName(), writer);
            client.Connect(100);
        }

        private bool TryConnectClient()
        {
            return true;
        }

        /// <summary>
        /// Starts recieving. Recieving happens on a different Task so sending can be done as well.
        /// </summary>
        /// <param name="action"></param>
        public void StartRecieving(Action<T> action)
        {
            Task.Run(() =>
            {
                Server.StartRecieving(action);
            });
        }

        public void Send(T value)
        {
            client.Send(value);
        }

        public void Dispose()
        {
            // dispose will be handled by fody janitor
        }
    }

    public class TwoWayMessageBag
    {
        public string Type { get; set; }

        public object Data { get; set; }
    }
}