using PizzaProjekt.Models;
using PizzaProjekt.Models.Context;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PizzaProjekt.ViewModels;

namespace PizzaProjekt.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly PizzaProjektContext db = new PizzaProjektContext();

        public ActionResult Index()
        {
            var ingredients = db.Ingredients.ToList().Select(i => new IngredientViewModel(i));
            foreach (var ingredient in ingredients)
            {
                ingredient.UsedInPizzasCount = db.Pizzas.Count(p => p.Ingredients.Any(i => i.Id == ingredient.Id));
            }
            return View(ingredients);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
                return HttpNotFound();

            var model = new IngredientViewModel(ingredient)
            {
                UsedInPizzasCount = db.Pizzas.Count(p => p.Ingredients.Any(i => i.Id == ingredient.Id))
            };

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Ingredient ingredient)
        {
            if (!ModelState.IsValid) return View(ingredient);

            if (db.Ingredients.Any(i => i.Name == ingredient.Name))
            {
                ModelState.AddModelError(string.Empty, "Składnik o tej nazwie już instnieje.");
                return View(ingredient);
            }

            db.Ingredients.Add(ingredient);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
                return HttpNotFound();

            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Ingredient ingredient)
        {
            if (!ModelState.IsValid) return View(ingredient);

            if (db.Ingredients.Any(i => i.Name == ingredient.Name && i.Id != ingredient.Id))
            {
                ModelState.AddModelError(string.Empty, "Składnik o tej nazwie już instnieje.");
                return View(ingredient);
            }

            db.Entry(ingredient).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
                return HttpNotFound();

            var model = new IngredientViewModel(ingredient)
            {
                UsedInPizzasCount = db.Pizzas.Count(p => p.Ingredients.Any(i => i.Id == ingredient.Id))
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ingredient = db.Ingredients.Find(id);

            if (db.Pizzas.Any(p => p.Ingredients.Any(i => i.Id == id)))
            {
                ModelState.AddModelError(string.Empty, "Ten składnik jest dodany do conajmniej jednej pizzy.");
                return View(ingredient);
            }

            db.Ingredients.Remove(ingredient);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}