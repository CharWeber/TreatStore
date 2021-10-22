namespace TreatStore
{
  public class Treat
  {
    public int TreatId {get;set;}
    public string Name {get;set;}
    public Virtual User user {get;set;}
    public virtual ICollection<FlavorTreat> FlavorTreatEntities {get;set;}
    
    public Treat()
    {
      this.FlavorTreatEntities = new HashSet <FlavorTreat>();
    }
  }
  
}