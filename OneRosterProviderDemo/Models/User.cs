﻿using Newtonsoft.Json;
using OneRosterProviderDemo.Validators;
using OneRosterProviderDemo.Vocabulary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneRosterProviderDemo.Models
{
    public class User : BaseModel
    {
        internal override string ModelType()
        {
            return "user";
        }

        internal override string UrlType()
        {
            return "users";
        }

        [Required]
        public string Username { get; set; }

        [NotMapped]
        public UserId[] UserIds
        {
            get => _userIds == null ? null : JsonConvert.DeserializeObject<UserId[]>(_userIds);
            set => _userIds = JsonConvert.SerializeObject(value);
        }
        private string _userIds { get; set; }

        [Required]
        public bool EnabledUser { get; set; }

        [Required]
        public string GivenName { get; set; }

        [Required]
        public string FamilyName { get; set; }
        public string MiddleName { get; set; }

        [Required]
        public RoleType Role { get; set; }

        public string Identifier { get; set; }
        public string Email { get; set; }
        public string SMS { get; set; }
        public string Phone { get; set; }

        public List<UserAgent> UserAgents { get; set; }
        [NotEmptyCollection]
        public List<UserOrg> UserOrgs { get; set; }
        public List<Enrollment> Enrollments { get; set; }

        [NotMapped]
        [Grades]
        public string[] Grades
        {
            get => _grades == null ? null : JsonConvert.DeserializeObject<string[]>(_grades);
            set => _grades = JsonConvert.SerializeObject(value);
        }
        private string _grades { get; set; }
        public string Password { get; set; }

        public new void AsJson(JsonWriter writer, string baseUrl)
        {
            writer.WriteStartObject();
            base.AsJson(writer, baseUrl);

            writer.WritePropertyName("username");
            writer.WriteValue(Username);

            if (UserIds != null && UserIds.Length > 0)
            {
                writer.WritePropertyName("userIds");
                writer.WriteStartArray();
                foreach (var userId in UserIds)
                {
                    userId.AsJson(writer, baseUrl);
                }
                writer.WriteEndArray();
            }

            writer.WritePropertyName("enabledUser");
            writer.WriteValue(EnabledUser.ToString());

            writer.WritePropertyName("givenName");
            writer.WriteValue(GivenName);

            writer.WritePropertyName("familyName");
            writer.WriteValue(FamilyName);

            if (!String.IsNullOrEmpty(MiddleName))
            {
                writer.WritePropertyName("middleName");
                writer.WriteValue(MiddleName);
            }

            writer.WritePropertyName("role");
            writer.WriteValue(Enum.GetName(typeof(Vocabulary.RoleType), Role));

            if (!String.IsNullOrEmpty(Identifier))
            {
                writer.WritePropertyName("identifier");
                writer.WriteValue(Identifier);
            }

            if (!String.IsNullOrEmpty(Email))
            {
                writer.WritePropertyName("email");
                writer.WriteValue(Email);
            }

            if (!String.IsNullOrEmpty(SMS))
            {
                writer.WritePropertyName("sms");
                writer.WriteValue(SMS);
            }

            if (!String.IsNullOrEmpty(Phone))
            {
                writer.WritePropertyName("phone");
                writer.WriteValue(Phone);
            }

            if (UserAgents.Count > 0)
            {
                writer.WritePropertyName("agents");
                writer.WriteStartArray();
                UserAgents.ForEach(ua => ua.Agent.AsJsonReference(writer, baseUrl));
                writer.WriteEndArray();
            }

            writer.WritePropertyName("orgs");
            writer.WriteStartArray();
            UserOrgs.ForEach(uo => uo.Org.AsJsonReference(writer, baseUrl));
            writer.WriteEndArray();

            if (Grades != null && Grades.Length > 0)
            {
                writer.WritePropertyName("grades");
                writer.WriteStartArray();
                foreach (var grade in Grades)
                {
                    writer.WriteValue(grade);
                }
                writer.WriteEndArray();
            }

            if (!String.IsNullOrEmpty(Password))
            {
                writer.WritePropertyName("password");
                writer.WriteValue(Password);
            }

            writer.WriteEndObject();
            writer.Flush();
        }
    }
}
