﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace OneRosterProviderDemo.Models
{
    public class LineItem : BaseModel
    {
        internal override string ModelType()
        {
            return "lineItem";
        }

        internal override string UrlType()
        {
            return "lineItems";
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime AssignDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string IMSClassId { get; set; }
        public IMSClass IMSClass { get; set; }

        [Required]
        public string LineItemCategoryId { get; set; }
        public LineItemCategory LineItemCategory { get; set; }

        [Required]
        public string AcademicSessionId { get; set; }
        public AcademicSession AcademicSession { get; set; }

        [Required]
        public float ResultValueMin { get; set; }

        [Required]
        public float ResultValueMax { get; set; }

        public new void AsJson(JsonWriter writer, string baseUrl)
        {
            writer.WriteStartObject();
            base.AsJson(writer, baseUrl);

            writer.WritePropertyName("title");
            writer.WriteValue(Title);

            if (!string.IsNullOrEmpty(Description))
            {
                writer.WritePropertyName("description");
                writer.WriteValue(Description);
            }

            writer.WritePropertyName("assignDate");
            writer.WriteValue(AssignDate.ToString("yyyy-MM-dd"));

            writer.WritePropertyName("dueDate");
            writer.WriteValue(DueDate.ToString("yyyy-MM-dd"));

            writer.WritePropertyName("category");
            LineItemCategory.AsJsonReference(writer, baseUrl);

            writer.WritePropertyName("class");
            IMSClass.AsJsonReference(writer, baseUrl);

            writer.WritePropertyName("gradingPeriod");
            AcademicSession.AsJsonReference(writer, baseUrl);

            writer.WritePropertyName("resultValueMin");
            writer.WriteValue(ResultValueMin.ToString(CultureInfo.InvariantCulture));

            writer.WritePropertyName("resultValueMax");
            writer.WriteValue(ResultValueMax.ToString(CultureInfo.InvariantCulture));

            writer.WriteEndObject();
            writer.Flush();
        }

        public bool UpdateWithJson(JObject json)
        {
            try
            {
                Status = (Vocabulary.StatusType)Enum.Parse(typeof(Vocabulary.StatusType), GetOptionalJsonProperty(json, "status", "active"));
                Metadata = GetOptionalJsonProperty(json, "metadata", null);
                UpdatedAt = DateTime.Now;
                Title = (string)json["title"];
                Description = (string)json["description"];
                AssignDate = DateTime.Parse((string)json["assignDate"]);
                DueDate = DateTime.Parse((string)json["dueDate"]);
                IMSClassId = (string)json["class"]["sourcedId"];
                LineItemCategoryId = (string)json["category"]["sourcedId"];
                AcademicSessionId = (string)json["gradingPeriod"]["sourcedId"];
                ResultValueMin = (float)json["resultValueMin"];
                ResultValueMax = (float)json["resultValueMax"];
            }
            catch (NullReferenceException)
            {
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            return true;
        }
    }
}
