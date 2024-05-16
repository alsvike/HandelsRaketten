using System.ComponentModel.DataAnnotations;

namespace HandelsRaketten.Models.AdModels.SubCategories.Plants
{
    public class IndoorPlantAd : Ad
    {
        [StringLength(50, ErrorMessage = "Må ikke være mere end 50 tegn")]
        public string? Species { get; set; }
        [StringLength(50, ErrorMessage = "Må ikke være mere end 50 tegn")]
        public string? RecommendedTemperature { get; set; }
        [StringLength(50, ErrorMessage = "Må ikke være mere end 50 tegn")]
        public string? WateringNeeds { get; set; }
        [StringLength(50, ErrorMessage = "Må ikke være mere end 50 tegn")]
        public string? SunlightNeeds { get; set; }
        [StringLength(50, ErrorMessage = "Må ikke være mere end 50 tegn")]
        public string? RecommendedSoil { get; set; }
        [StringLength(50, ErrorMessage = "Må ikke være mere end 50 tegn")]
        public string? Size { get; set; }
        [StringLength(50, ErrorMessage = "Må ikke være mere end 50 tegn")]
        public string? PotSize { get; set; }
        public IndoorPlantAd()
        {
            Active = true;
            CreatedOn = DateTime.Now;
            Category = "IndoorPlant";

        }

    }
}
