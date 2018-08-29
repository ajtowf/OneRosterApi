﻿using Newtonsoft.Json;
using OneRosterProviderDemo.Vocabulary;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneRosterProviderDemo.Models
{
    public class Enrollment : BaseModel
    {
        internal override string ModelType()
        {
            return "enrollment";
        }

        internal override string UrlType()
        {
            return "enrollments";
        }

        [Required]
        public RoleType Role { get; set; }

        public Boolean? Primary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string IMSClassId { get; set; }
        public IMSClass IMSClass { get; set; }

        [Required]
        public string SchoolOrgId { get; set; }
        [ForeignKey("SchoolOrgId")]
        public Org School { get; set; }

        public new void AsJson(JsonWriter writer, string baseUrl)
        {
            writer.WriteStartObject();
            base.AsJson(writer, baseUrl);

            writer.WritePropertyName("user");
            User.AsJsonReference(writer, baseUrl);

            writer.WritePropertyName("class");
            IMSClass.AsJsonReference(writer, baseUrl);

            writer.WritePropertyName("school");
            School.AsJsonReference(writer, baseUrl);

            writer.WritePropertyName("role");
            writer.WriteValue(Enum.GetName(typeof(RoleType), Role));

            if (Primary != null)
            {
                writer.WritePropertyName("primary");
                writer.WriteValue(Primary.ToString());
            }

            if (BeginDate != null)
            {
                writer.WritePropertyName("beginDate");
                writer.WriteValue(BeginDate.ToString("yyyy-MM-dd"));
            }

            if (EndDate != null)
            {
                writer.WritePropertyName("endDate");
                writer.WriteValue(EndDate.ToString("yyyy-MM-dd"));
            }

            writer.WriteEndObject();
            writer.Flush();
        }
    }
}
