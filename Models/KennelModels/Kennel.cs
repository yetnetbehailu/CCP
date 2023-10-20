using CCP.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCP.Models.KennelModels
{
	public class Kennel
	{
        public int ID { get; set; }
        public int CountryID { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string WebsiteURL { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string About { get; set; }

        public ImagesMetaData Logo { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public CCPUser User { get; set; }
        public Country Country { get; set; }
    }
}

