using System;
namespace CCP.Models.DogModels
{
	public class OfficialTitle
	{
        public int ID { get; set; }
        public string Title { get; set; }
        public string FullTitle { get; set; }
        public ICollection<ChampionshipTitle> Champions { get; set; }
    }
}

