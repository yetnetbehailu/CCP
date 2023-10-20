using CCP.Areas.Identity.Data;
using CCP.Models.DogModels;
using CCP.Models.KennelModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCP.Models
{
    public class ImagesMetaData
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ImagePath { get; set; }
        public required DateTime UploadDate { get; set; } = DateTime.Now;
        public int KennelId { get; set; }
        [ForeignKey("KennelId")]
        public Kennel? Kennel { get; set; }
        public Dog? Dog { get; set; }
        public CCPUser? User { get; set; }
    }
}
