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
  public class FlavorsController : Controller
  {
    private readonly TreatStoreContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(TreatStoreContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Flavors.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flavor)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flavor.User = currentUser;
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      ViewBag.NoTreats = _db.Treats.ToList().Count == 0;
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      var thisFlavor = _db.Flavors
        .Include(flavor => flavor.FlavorTreatEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor =>flavor.FlavorId ==id);
      return View(thisFlavor);
    }

    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeletConfirmed(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        var joinExists = _db.FlavorTreats.FirstOrDefault(join => join.FlavorId == flavor.FlavorId && join.TreatId == TreatId);
      
        if (joinExists != null)
        {
          ViewBag.ErrorMessage = "This Flavor is already in production for this treat";
          return View("Error");
        }
        else
        {
          _db.FlavorTreats.Add( new FlavorTreat(){FlavorId = flavor.FlavorId, TreatId = TreatId});
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
      
      }
      else
      {
        ViewBag.ErrorMessage = "This treat no longer exists";
        return View("Error");
      }

    }

    [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
      var thisJoin = _db.FlavorTreats.FirstOrDefault(entry =>entry.FlavorTreatId == joinId);
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