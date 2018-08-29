using Newtonsoft.Json;

namespace OneRosterProviderDemo.Exceptions
{
    public class InvalidSortFieldException : OneRosterException
    {
        private readonly string _field;
        public InvalidSortFieldException(string field)
        {
            _field = field;
        }
        public override void AsJson(JsonWriter writer, string operation)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("imsx_codeMajor");
            writer.WriteValue("success");

            writer.WritePropertyName("imsx_severity");
            writer.WriteValue("warning");

            writer.WritePropertyName("imsx_operationRefIdentifier");
            writer.WriteValue(operation);

            writer.WritePropertyName("imsx_description");
            writer.WriteValue(_field);

            writer.WritePropertyName("imsx_codeMinor");
            writer.WriteValue("invalid_sort_field");

            writer.WriteEndObject();
        }
    }
}
