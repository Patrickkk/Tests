using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FileEtl.TwoWayNamedPipes
{
    // possible improvements: BSON
    public class TwoWayNamedPipesBus
    {
        private readonly TwoWayNamedPipeConnection<TwoWayNamedPipesBusMessageContainer> connection;

        public Dictionary<Type, Action<object>> TypeRecieveActions { get; set; } = new Dictionary<Type, Action<object>>();

        public Dictionary<string, Type> TypeNameToType { get; set; } = new Dictionary<string, Type>();

        public TwoWayNamedPipesBus(string name)
        {
            this.connection = new TwoWayNamedPipeConnection<TwoWayNamedPipesBusMessageContainer>(name, new NamedPipesJsonReader<TwoWayNamedPipesBusMessageContainer>(), new NamedPipeJsonWriter<TwoWayNamedPipesBusMessageContainer>());
        }

        public void Send<T>(T value)
        {
            if (TypeNameToType.ContainsKey(typeof(T).GetType().FullName))
            {
                TypeNameToType.Add(typeof(T).GetType().FullName, typeof(T));
            }
            connection.Send(new TwoWayNamedPipesBusMessageContainer { Data = JsonConvert.SerializeObject(value), TypeName = typeof(T).GetType().FullName });
        }

        public TwoWayNamedPipesBus For<T>(Action<T> action)
        {
            // TODO move into method
            TypeRecieveActions.Add(typeof(T), x => action((T)x));
            if (TypeNameToType.ContainsKey(typeof(T).GetType().FullName))
            {
                TypeNameToType.Add(typeof(T).GetType().FullName, typeof(T));
            }
            return this;
        }

        public void StartRecieving()
        {
            connection.Connect();
            connection.StartRecieving(InvokeActionForType);
        }

        private void InvokeActionForType(TwoWayNamedPipesBusMessageContainer message)
        {
            if (TypeNameToType.ContainsKey(message.TypeName))
            {
                var type = TypeNameToType[message.TypeName];
                var messageData = JsonConvert.DeserializeObject(message.Data, type);
            }
            else
            {
                // Unkown type.... ignore? TODO
            }
        }
    }

    public class TwoWayNamedPipesBusMessageContainer
    {
        public string TypeName { get; set; }

        // TODO improve and turn into bytes
        public string Data { get; set; }
    }
}