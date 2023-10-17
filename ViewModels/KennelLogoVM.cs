using CCP.Models.KennelModels;

namespace CCP.ViewModels
{
    public class KennelLogoVM
    {
        public Kennel Kennel { get; set; }
        public IFormFile? Logo { get; set; }
    }
}
