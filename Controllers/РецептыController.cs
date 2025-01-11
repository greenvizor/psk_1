using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using psk_1.Models;

namespace psk_1.Controllers
{
    public class РецептыController : Controller
    {
        private psk_dbEntities1 db = new psk_dbEntities1();

        // GET: Рецепты
        // Контроллер
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var recipes = from r in db.Рецепты
                          select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.Название.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    recipes = recipes.OrderByDescending(r => r.Название);
                    break;
                default:
                    recipes = recipes.OrderBy(r => r.Название);
                    break;
            }

            return View(recipes.ToList());
        }


        // GET: Рецепты/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рецепты рецепты = db.Рецепты.Find(id);
            if (рецепты == null)
            {
                return HttpNotFound();
            }
            return View(рецепты);
        }

        // GET: Рецепты/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Рецепты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "РецептId,Название")] Рецепты рецепты)
        {
            if (ModelState.IsValid)
            {
                db.Рецепты.Add(рецепты);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(рецепты);
        }

        // GET: Рецепты/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рецепты рецепты = db.Рецепты.Find(id);
            if (рецепты == null)
            {
                return HttpNotFound();
            }
            return View(рецепты);
        }

        // POST: Рецепты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "РецептId,Название")] Рецепты рецепты)
        {
            if (ModelState.IsValid)
            {
                db.Entry(рецепты).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(рецепты);
        }

        // GET: Рецепты/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рецепты рецепты = db.Рецепты.Find(id);
            if (рецепты == null)
            {
                return HttpNotFound();
            }
            return View(рецепты);
        }

        // POST: Рецепты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Рецепты рецепты = db.Рецепты.Find(id);
            db.Рецепты.Remove(рецепты);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
