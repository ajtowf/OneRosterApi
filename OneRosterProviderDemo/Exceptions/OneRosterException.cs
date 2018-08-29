using Newtonsoft.Json;
using System;

namespace OneRosterProviderDemo.Exceptions
{
    public abstract class OneRosterException : Exception
    {
        public abstract void AsJson(JsonWriter writer, string operation);
    }
}
