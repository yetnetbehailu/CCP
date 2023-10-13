using System;
using CCP.Areas.Identity.Data;

namespace CCP.Models.BreederModels
{
	public class Breeder
	{
        public int ID { get; set; }
        public string UserID { get; set; }
        public int? CountryID { get; set; }
        public string Name { get; set; }

        public CCPUser User { get; set; }
        public Country? Country { get; set; }
    }
}

