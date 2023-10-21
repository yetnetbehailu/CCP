using System;
namespace CCP.Models.DogModels
{
	public class ChampionshipTitle
	{
        public int ID { get; set; }
        public int DogID { get; set; }
        public int OfficialTitleID { get; set; }
        public Dog Dog { get; set; }
        public OfficialTitle OfficialTitle { get; set; }
    }
}

