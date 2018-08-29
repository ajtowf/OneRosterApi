﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneRosterProviderDemo.Vocabulary;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneRosterProviderDemo.Models
{
    public class Result : BaseModel
    {
        internal override string ModelType()
        {
            return "result";
        }

        internal override string UrlType()
        {
            return "results";
        }

        [Required]
        public string LineItemId { get; set; }
        public LineItem LineItem { get; set; }

        [Required]
        public string StudentUserId { get; set; }
        [ForeignKey("StudentUserId")]
        public User Student { get; set; }

        [Required]
        public ScoreStatus ScoreStatus { get; set; }

        [Required]
        public float Score { get; set; }

        [Required]
        public DateTime ScoreDate { get; set; }

        public string Comment { get; set; }

        public new void AsJson(JsonWriter writer, string baseUrl)
        {
            writer.WriteStartObject();
            base.AsJson(writer, baseUrl);

            writer.WritePropertyName("lineItem");
            LineItem.AsJsonReference(writer, baseUrl);

            writer.WritePropertyName("student");
            Student.AsJsonReference(writer, baseUrl);

            writer.WritePropertyName("scoreStatus");
            writer.WriteValue(ScoreStatus.ToString().Replace('_', ' '));

            writer.WritePropertyName("score");
            writer.WriteValue(Score.ToString());

            writer.WritePropertyName("scoreDate");
            writer.WriteValue(ScoreDate.ToString("yyyy-MM-dd"));

            if (!string.IsNullOrEmpty(Comment))
            {
                writer.WritePropertyName("comment");
                writer.WriteValue(Comment);
            }

            writer.WriteEndObject();
            writer.Flush();
        }

        public bool UpdateWithJson(JObject json)
        {
            try
            {
                Status = (StatusType)Enum.Parse(typeof(StatusType), GetOptionalJsonProperty(json, "status", "active"));
                Metadata = GetOptionalJsonProperty(json, "metadata", null);
                UpdatedAt = DateTime.Now;
                LineItemId = (string)json["lineItem"]["sourcedId"];
                StudentUserId = (string)json["student"]["sourcedId"];
                ScoreStatus = (ScoreStatus)Enum.Parse(typeof(ScoreStatus), GetOptionalJsonProperty(json, "scoreStatus", "fully_graded").Replace(' ', '_'));
                Score = float.Parse((string)json["score"]);
                ScoreDate = DateTime.Parse((string)json["scoreDate"]);
                Comment = GetOptionalJsonProperty(json, "comment", null);
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
