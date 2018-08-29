using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace OneRosterProviderDemo.Serializers
{
    public class OneRosterSerializer
    {
        private readonly StringBuilder _sb;

        public JsonWriter Writer { get; }
        public string Finish()
        {
            Writer.WriteEndObject();
            return _sb.ToString();
        }

        public OneRosterSerializer(string root)
        {
            _sb = new StringBuilder();
            Writer = new JsonTextWriter(new StringWriter(_sb));
            Writer.WriteStartObject();
            Writer.WritePropertyName(root);
        }
    }
}
