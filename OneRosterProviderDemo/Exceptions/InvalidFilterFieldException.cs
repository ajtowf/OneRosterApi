using Newtonsoft.Json;

namespace OneRosterProviderDemo.Exceptions
{
    public class InvalidFilterFieldException : OneRosterException
    {
        private readonly string _field;
        public InvalidFilterFieldException(string field)
        {
            _field = field;
        }
        public override void AsJson(JsonWriter writer, string operation)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("imsx_codeMajor");
            writer.WriteValue("failure");

            writer.WritePropertyName("imsx_severity");
            writer.WriteValue("error");

            writer.WritePropertyName("imsx_operationRefIdentifier");
            writer.WriteValue(operation);

            writer.WritePropertyName("imsx_description");
            writer.WriteValue(_field);

            writer.WritePropertyName("imsx_codeMinor");
            writer.WriteValue("invalid_filter_field");

            writer.WriteEndObject();
        }
    }
}
