using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Linq;
using TreatStore.Models;


namespace TreatStore.Controllers
{
  public class HomeController : Controller
  {
    private readonly TreatStoreContext _db;

    public HomeController(TreatStoreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      dynamic model = new ExpandoObject();
      model.Flavors = _db.Flavors.ToList();
      model.Treats = _db.Treats.ToList();
      return View(model);
    }
  }
}