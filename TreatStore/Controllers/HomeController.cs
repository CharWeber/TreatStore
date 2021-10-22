using Microsoft.AspNetCore.Mvc;

namespace TreatStore.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}