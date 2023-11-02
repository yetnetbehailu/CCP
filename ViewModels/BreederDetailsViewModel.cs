using System;
using CCP.Models.BreederModels;
using CCP.Models.DogModels;

namespace CCP.ViewModels
{
    public class BreederDetailsViewModel
    {
        public Breeder Breeder { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}

