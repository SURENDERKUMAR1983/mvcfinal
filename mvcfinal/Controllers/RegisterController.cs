using mvcfinal.data;
using mvcfinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace mvcfinal.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext context;
        public RegisterController()
        {
            context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: Register
        public ActionResult Index()
        {
            var RegisterList = context.Registers.Include(r => r.City).Include(r => r.City.State).Include(r => r.City.State.Country).ToList();
            return View(RegisterList);
        }

        public ActionResult Create()
        {
            ViewBag.CountryList = context.Countries.ToList();
            ViewBag.StateList = context.States.ToList();
            ViewBag.CityList = context.Cities.ToList();
            Register register = new Register();
            return View(register);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Register register)

        {
            if (register == null)
                return HttpNotFound();
            if (!ModelState.IsValid)
            {
                ViewBag.CountryList = context.Countries.ToList();
                ViewBag.StateList = context.States.ToList();
                ViewBag.CityList = context.Cities.ToList();
                return View();
            }
            context.Registers.Add(register);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Edit(int id)
        {
            var userinDb = context.Registers.Include(r => r.City).Include(r => r.City.State).Include(r => r.City.State.Country).FirstOrDefault(r => r.Id == id);
            if (userinDb == null)
                return HttpNotFound();
            ViewBag.CountryList = context.Countries.ToList();
            ViewBag.StateList = context.States.ToList();
            ViewBag.CityList = context.Cities.ToList();
            userinDb.CountryId = userinDb.City.State.Country.Id;
            userinDb.StateId = userinDb.City.State.Id;
            return View(userinDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Register register)
        {
            if (register == null)
                return HttpNotFound();
            var userfromDb = context.Registers.Find(register.Id);
            if (userfromDb == null)
                return HttpNotFound();
            if (!ModelState.IsValid)
            {
                ViewBag.CountryList = context.Countries.ToList();
                ViewBag.StateList = context.States.ToList();
                ViewBag.CityList = context.Cities.ToList();
                return View();
            }
            userfromDb.Name = register.Name;
            userfromDb.Address = register.Address;
            userfromDb.Email = register.Email;
            userfromDb.Gender = register.Gender;
            userfromDb.Subscribe = register.Subscribe;
            userfromDb.CityId = register.CityId;
            userfromDb.StateId = register.StateId;
            userfromDb.CountryId = register.CountryId;
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Details(int id)
        {
            var userinDb = context.Registers.Include(r => r.City).Include(r => r.City.State).Include(r => r.City.State.Country).FirstOrDefault(r => r.Id == id);
            if (userinDb == null)
                return HttpNotFound();
            return View(userinDb);
        }
        public ActionResult Delete(int id)
        {
            var userinDb = context.Registers.Include(r => r.City).Include(r => r.City.State).Include(r => r.City.State.Country).FirstOrDefault(r => r.Id == id);
            if (userinDb == null)
                return HttpNotFound();
            context.Registers.Remove(userinDb);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // This code used for- when select country-state list of that country will come automatically - and when select state then city list of that particular state should come automaticallyw1sssssssssssssss 
        #region APIs
        private List<State> GetStates(int CountryId)
        {
            return context.States.Where(s => s.CountryId == CountryId).ToList();
        }
        private List<City> GetCities(int StateId)
        {
            return context.Cities.Where(s => s.StateId == StateId).ToList();
        }
        public ActionResult LoadStateByCountryId(int CountryId)
        {
            var StateList = GetStates(CountryId);
            var StateListData = StateList.Select(sl => new SelectListItem()
            {
                Text = sl.Name,
                Value = sl.Id.ToString()

            });
            return Json(StateListData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadCityByStateId(int StateId)
        {
            var CityList = GetCities(StateId);
            var CityListData = CityList.Select(cl => new SelectListItem()
            {
                Text=cl.Name,
                Value=cl.Id.ToString()
            });
            return Json(CityListData, JsonRequestBehavior.AllowGet);
        }      
        #endregion



    }
}
