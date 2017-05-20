using System;
using System.IO.Pipes;

namespace FileEtl.TwoWayNamedPipes
{
    internal class ClientNamedPipeConnection<T> : IDisposable
    {
        private readonly string connectionName;
        private readonly NamedPipeClientStream connection;
        private readonly INamedPipesWriter<T> writer;

        public ClientNamedPipeConnection(string connectionName, INamedPipesWriter<T> writer)
        {
            this.writer = writer;
            this.connectionName = connectionName;
            this.connection = new NamedPipeClientStream(connectionName);
        }

        public void Connect()
        {
            connection.Connect();
        }

        public void Connect(int timeout)
        {
            connection.Connect(timeout);
        }

        public void Dispose()
        {
            // will be used by fody jantitor
        }

        internal void Send(T value)
        {
            writer.Write(value, connection);
        }
    }
}