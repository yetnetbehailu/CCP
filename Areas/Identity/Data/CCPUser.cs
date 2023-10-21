using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CCP.Models.BreederModels;
using CCP.Models.DogModels;
using CCP.Models.KennelModels;
using Microsoft.AspNetCore.Identity;

namespace CCP.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CCPUser class
public class CCPUser : IdentityUser
{
    public Breeder Breeder { get; set; }
    public Kennel Kennel { get; set; }

    public ICollection<Dog> DogOwner { get; set; }
    public ICollection<Dog> DogBreeder { get; set; }
    public ICollection<Dog> DogKennel { get; set; }
}

