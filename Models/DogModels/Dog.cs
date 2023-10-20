using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CCP.Areas.Identity.Data;

namespace CCP.Models.DogModels
{
	public class Dog
	{
        [Key]
        public int ID { get; set; }
        public string RegName { get; set; }
        public string? RegNo { get; set; }
        public string? PetName { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? YearOfDeath { get; set; }
        public Coats? Coat { get; set; } // Null allowed as an inital value requirement handled in view
        public Genders? Gender { get; set; }

        public string? Color { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }



        public ICollection<ChampionshipTitle> ChampionshipTitles { get; set; }
        public ICollection<Pedigree> SirePedigree { get; set; }
        public ICollection<Pedigree> DamPedigree { get; set; }
        public ICollection<Pedigree> LitterPedigree { get; set; }



        public string? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public CCPUser? Owner { get; set; }

        public string? BreederID { get; set; }
        [ForeignKey("BreederID")]
        public CCPUser? Breeder { get; set; }

        public string? KennelID { get; set; }
        [ForeignKey("KennelID")]
        public CCPUser? Kennel { get; set; }
    }

    public enum Coats
    {
        Hairless,
        Powderpuff
    }

    public enum Genders
    {
        Male,
        Female
    }
}

