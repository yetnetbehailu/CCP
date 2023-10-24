using CCP.Models.BreederModels;
using CCP.Models.DogModels;
using CCP.Models.KennelModels;

namespace CCP.ViewModels
{
    public class DogViewModel
    {
        public Dog Dog { get; set; }
        public Kennel Kennel { get; set; }
        public Breeder Breeder { get; set; }
    }
}
