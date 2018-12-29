using PizzaProjekt.Helpers;
using PizzaProjekt.Models;
using PizzaProjekt.Models.Context;
using PizzaProjekt.Models.Enums;
using PizzaProjekt.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PizzaProjekt.Controllers
{
    public class PizzasController : Controller
    {
        private readonly PizzaProjektContext db = new PizzaProjektContext();

        public ActionResult Index()
        {
            return View(db.Pizzas.ToList().Select(p => new PizzaViewModel(p)));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var pizza = db.Pizzas.Find(id);
            if (pizza == null)
                return HttpNotFound();

            ViewBag.Ingredients = pizza.Ingredients.Select(i => i.Name);
            var model = new PizzaViewModel(pizza);
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Ingredients = db.Ingredients.ToList();
            ViewBag.Batters = Enum.GetValues(typeof(Batter))
                .Cast<Batter>()
                .ToDictionary(t => (int)t, t => t.GetDescription());
            ViewBag.Sizes = Enum.GetValues(typeof(Size))
                .Cast<Size>()
                .ToDictionary(t => (int)t, t => t.GetDescription());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaViewModel model)
        {
            var errors = ValidatePizzaViewModelObject(model);
            if (errors != null && errors.Any())
                return Json(new { dataValid = false, errors = errors }, JsonRequestBehavior.AllowGet);

            try
            {
                var pizza = new Pizza
                {
                    Name = model.Name,
                    Ingredients = db.Ingredients.Where(i => model.IngredientsIds.Contains(i.Id)).ToList(),
                    Versions = model.Versions.Select(v => new Models.Version
                    {
                        Batter = v.Batter,
                        Size = v.Size,
                        Price = v.Price
                    }).ToList()
                };

                db.Pizzas.Add(pizza);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { dataValid = false, errors = new List<string> { e.Message } }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { dataValid = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var pizza = db.Pizzas.Find(id);
            if (pizza == null)
                return HttpNotFound();

            ViewBag.Ingredients = db.Ingredients.ToList();
            ViewBag.Batters = Enum.GetValues(typeof(Batter))
                .Cast<Batter>()
                .ToDictionary(t => (int)t, t => t.GetDescription());
            ViewBag.Sizes = Enum.GetValues(typeof(Size))
                .Cast<Size>()
                .ToDictionary(t => (int)t, t => t.GetDescription());
            var model = new PizzaViewModel(pizza);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PizzaViewModel model)
        {
            var errors = ValidatePizzaViewModelObject(model);
            if (errors != null && errors.Any())
                return Json(new { dataValid = false, errors = errors }, JsonRequestBehavior.AllowGet);

            try
            {
                var pizza = db.Pizzas.Find(model.Id);

                if (pizza.Name != model.Name)
                    pizza.Name = model.Name;

                foreach (var version in pizza.Versions)
                {
                    if (Math.Abs(version.Price - model.Versions.First(v => v.Batter == version.Batter && v.Size == version.Size).Price) > 0.01)
                        version.Price = model.Versions.First(v => v.Batter == version.Batter && v.Size == version.Size).Price;
                }

                if (model.IngredientsIds.Count != pizza.Ingredients.Count || !model.IngredientsIds.All(pizza.Ingredients.Select(i => i.Id).Contains))
                    pizza.Ingredients = db.Ingredients.Where(i => model.IngredientsIds.Contains(i.Id)).ToList();

                db.Entry(pizza).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { dataValid = false, errors = new List<string> { e.Message } }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { dataValid = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var pizza = db.Pizzas.Find(id);
            if (pizza == null)
                return HttpNotFound();

            var model = new PizzaViewModel(pizza);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pizza = db.Pizzas.Find(id);
            //foreach (var version in pizza.Versions)
            //{
            //    db.Versions.Remove(version);
            //}
            db.Pizzas.Remove(pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        private IEnumerable<string> ValidatePizzaViewModelObject(PizzaViewModel model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();

            if (db.Pizzas.Any(p => p.Name == model.Name && (model.Id == 0 || p.Id != model.Id)))
                errors.Add("Pizza o podanej nazwie już istnieje.");

            if (model.Versions.Count != Enum.GetValues(typeof(Size)).Length * Enum.GetValues(typeof(Batter)).Length)
                errors.Add("Podaj wszystkie ceny.");

            if (model.Versions.Any(v => v.Price <= 0))
                errors.Add("Każde Pole Cena musi być większe od 0.");

            return errors;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}