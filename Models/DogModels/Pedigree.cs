using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCP.Models.DogModels
{
	public class Pedigree
	{
        public int ID { get; set; }
        public int? SireID { get; set; }
        public int? DamID { get; set; }
        public int LitterID { get; set; }


        [ForeignKey("SireID")]
        public Dog Sire { get; set; }

        [ForeignKey("DamID")]
        public Dog Dam { get; set; }

        [ForeignKey("LitterID")]
        public Dog Litter { get; set; }
    }
}

