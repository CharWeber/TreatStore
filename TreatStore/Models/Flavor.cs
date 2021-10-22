using System;
using System.Collections.Generic;

namespace TreatStore.Models
{
  public class Flavor
  {
    public int FlavorId {get;set;}
    public string Name {get;set;}
    public virtual ApplicationUser User {get;set;}
    public virtual ICollection<FlavorTreat> FlavorTreatEntities {get;set;}

    public Flavor()
    {
      this.FlavorTreatEntities = new HashSet<FlavorTreat>();
    }
  }
}