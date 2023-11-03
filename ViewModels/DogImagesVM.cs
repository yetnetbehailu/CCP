using CCP.Models.DogModels;

namespace CCP.ViewModels
{
    public class DogImagesVM
    {
        public Dog Dog { get; set; }
        public IFormFileCollection? Images { get; set; }
    }
}
