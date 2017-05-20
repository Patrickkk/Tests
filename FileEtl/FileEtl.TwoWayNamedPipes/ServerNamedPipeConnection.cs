using System;
using System.IO.Pipes;

namespace FileEtl.TwoWayNamedPipes
{
    internal class ServerNamedPipeConnection<T> : IDisposable
    {
        public Action onRecieveAction { get; private set; }
        private readonly string name;
        private readonly NamedPipeServerStream connection;
        private readonly INamedPipesReader<T> reader;

        public ServerNamedPipeConnection(string name, INamedPipesReader<T> reader)
        {
            this.reader = reader;
            this.name = name;
            this.connection = new NamedPipeServerStream(name);
        }

        public void WaitForConnection()
        {
            connection.WaitForConnection();
        }

        public void StartRecieving(Action<T> action)
        {
            while (true)
            {
                var result = reader.Read(connection);
                action(result);
            }
        }

        internal void SendInitialMessage()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // will be used by fody jantitor
        }
    }
}