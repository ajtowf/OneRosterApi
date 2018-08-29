using Newtonsoft.Json;
using OneRosterProviderDemo.Vocabulary;
using System;

namespace OneRosterProviderDemo.Models
{
    public class Demographic : BaseModel
    {
        internal override string ModelType()
        {
            return "demographic";
        }

        internal override string UrlType()
        {
            return "demographic";
        }

        public DateTime BirthDate { get; set; }
        public Gender Sex { get; set; }
        public bool AmericanIndianOrAlaskaNative { get; set; }
        public bool Asian { get; set; }
        public bool BlackOrAfricanAmerican { get; set; }
        public bool NativeHawaiianOrOtherPacificIslander { get; set; }
        public bool White { get; set; }
        public bool DemographicRaceTwoOrMoreRaces { get; set; }
        public bool HispanicOrLatinoEthnicity { get; set; }
        public string CountryOfBirthCode { get; set; }
        public string StateOfBirthAbbreviation { get; set; }
        public string CityOfBirth { get; set; }
        public string PublicSchoolResidenceStatus { get; set; }

        public new void AsJson(JsonWriter writer, string baseUrl)
        {
            writer.WriteStartObject();
            base.AsJson(writer, baseUrl);

            if (BirthDate != null)
            {
                writer.WritePropertyName("birthDate");
                writer.WriteValue(BirthDate);
            }
            writer.WritePropertyName("sex");
            writer.WriteValue(Sex);

            writer.WritePropertyName("americanIndianOrAlaskaNative");
            writer.WriteValue(AmericanIndianOrAlaskaNative);
            
            writer.WritePropertyName("asian");
            writer.WriteValue(Asian);
            
            writer.WritePropertyName("blackOrAfricanAmerican");
            writer.WriteValue(BlackOrAfricanAmerican);
            
            writer.WritePropertyName("nativeHawaiianOrOtherPacificIslander");
            writer.WriteValue(NativeHawaiianOrOtherPacificIslander);
            
            writer.WritePropertyName("white");
            writer.WriteValue(White);
          
            writer.WritePropertyName("demographicRaceTwoOrMoreRaces");
            writer.WriteValue(DemographicRaceTwoOrMoreRaces);
            
            writer.WritePropertyName("hispanicOrLatinoEthnicity");
            writer.WriteValue(HispanicOrLatinoEthnicity);
            
            if (CountryOfBirthCode != null)
            {
                writer.WritePropertyName("countryOfBirthCode");
                writer.WriteValue(CountryOfBirthCode);
            }
            if (StateOfBirthAbbreviation != null)
            {
                writer.WritePropertyName("stateOfBirthAbbreviation");
                writer.WriteValue(StateOfBirthAbbreviation);
            }
            if (CityOfBirth != null)
            {
                writer.WritePropertyName("cityOfBirth");
                writer.WriteValue(CityOfBirth);
            }
            if (PublicSchoolResidenceStatus != null)
            {
                writer.WritePropertyName("publicSchoolResidenceStatus");
                writer.WriteValue(PublicSchoolResidenceStatus);
            }

            writer.WriteEndObject();
            writer.Flush();
        }
    }
}
