using System;
using System.Collections.Generic;

namespace TreatStore.Models
{
  public class Treat
  {
    public int TreatId {get;set;}
    public string Name {get;set;}
    public virtual ApplicationUser User {get;set;}
    public virtual ICollection<FlavorTreat> FlavorTreatEntities {get;set;}
    
    public Treat()
    {
      this.FlavorTreatEntities = new HashSet <FlavorTreat>();
    }
  }
  
}