using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using TreatStore.Models;

namespace TreatStore.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly TreatStoreContext _db;
    private readonly UserManager<ApplicationUser> _userManager;


    public TreatsController(TreatStoreContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Treats.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var treatExists = _db.Treats.FirstOrDefault(entry => entry.Name == treat.Name);

      if (treatExists != null)
      {
        ViewBag.ErrorMessage = "This treat is already in production";
        return View("Error");
      }
      else
      {
      treat.User = currentUser; 
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
      }
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      ViewBag.NoFlavors = _db.Flavors.ToList().Count == 0;
      ViewBag.FlavorId = new SelectList (_db.Flavors, "FlavorId", "Name");
      var thisTreat = _db.Treats
        .Include(treat => treat.FlavorTreatEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat =>treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat =>treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat =>treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if(FlavorId != 0)
      {
        var joinExists = _db.FlavorTreats.FirstOrDefault(join => join.FlavorId == FlavorId && join.TreatId == treat.TreatId);

        if (joinExists != null)
        {
          ViewBag.ErrorMessage = "This treat already has this flavor";
          return View("Error");
        }
        else
        {
          _db.FlavorTreats.Add(new FlavorTreat() {FlavorId = FlavorId, TreatId = treat.TreatId});
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
      }
      else
      {
        ViewBag.ErrorMessage = "This Flavor no longer Exists";
        return View("Error");
      }
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
    {
      var thisJoin = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreats.Remove(thisJoin);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Error()
    {
      return View();
    }

  }

}