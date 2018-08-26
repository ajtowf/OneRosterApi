using System.ComponentModel.DataAnnotations;

namespace OneRosterProviderDemo.ViewModels
{
    public class CreateRestProfileModel
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string ConnectionUrl { get; set; }
        [Required]
        public string SchoolIds { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string AzureDomain { get; set; }
    }
}
