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
    public class РецептИнгредиентыController : Controller
    {
        private psk_dbEntities1 db = new psk_dbEntities1();

        // GET: РецептИнгредиенты
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.RecipeSortParm = sortOrder == "Recipe" ? "recipe_desc" : "Recipe";
            var рецептИнгредиенты = from ri in db.РецептИнгредиенты
                                    select ri;

            if (!String.IsNullOrEmpty(searchString))
            {
                рецептИнгредиенты = рецептИнгредиенты.Where(ri => ri.Ингредиенты.Название.Contains(searchString)
                                        || ri.Рецепты.Название.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    рецептИнгредиенты = рецептИнгредиенты.OrderByDescending(ri => ri.Ингредиенты.Название);
                    break;
                case "Quantity":
                    рецептИнгредиенты = рецептИнгредиенты.OrderBy(ri => ri.Количество);
                    break;
                case "quantity_desc":
                    рецептИнгредиенты = рецептИнгредиенты.OrderByDescending(ri => ri.Количество);
                    break;
                case "Recipe":
                    рецептИнгредиенты = рецептИнгредиенты.OrderBy(ri => ri.Рецепты.Название);
                    break;
                case "recipe_desc":
                    рецептИнгредиенты = рецептИнгредиенты.OrderByDescending(ri => ri.Рецепты.Название);
                    break;
                default:
                    рецептИнгредиенты = рецептИнгредиенты.OrderBy(ri => ri.Ингредиенты.Название);
                    break;
            }

            return View(рецептИнгредиенты.ToList());
        }


        // GET: РецептИнгредиенты/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            РецептИнгредиенты рецептИнгредиенты = db.РецептИнгредиенты.Find(id);
            if (рецептИнгредиенты == null)
            {
                return HttpNotFound();
            }
            return View(рецептИнгредиенты);
        }

        // GET: РецептИнгредиенты/Create
        public ActionResult Create()
        {
            ViewBag.ИнгредиентId = new SelectList(db.Ингредиенты, "ИнгредиентId", "Название");
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название");
            return View();
        }

        // POST: РецептИнгредиенты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "РецептИнгредиентId,РецептId,ИнгредиентId,Количество")] РецептИнгредиенты рецептИнгредиенты)
        {
            if (ModelState.IsValid)
            {
                db.РецептИнгредиенты.Add(рецептИнгредиенты);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ИнгредиентId = new SelectList(db.Ингредиенты, "ИнгредиентId", "Название", рецептИнгредиенты.ИнгредиентId);
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", рецептИнгредиенты.РецептId);
            return View(рецептИнгредиенты);
        }

        // GET: РецептИнгредиенты/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            РецептИнгредиенты рецептИнгредиенты = db.РецептИнгредиенты.Find(id);
            if (рецептИнгредиенты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИнгредиентId = new SelectList(db.Ингредиенты, "ИнгредиентId", "Название", рецептИнгредиенты.ИнгредиентId);
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", рецептИнгредиенты.РецептId);
            return View(рецептИнгредиенты);
        }

        // POST: РецептИнгредиенты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "РецептИнгредиентId,РецептId,ИнгредиентId,Количество")] РецептИнгредиенты рецептИнгредиенты)
        {
            if (ModelState.IsValid)
            {
                db.Entry(рецептИнгредиенты).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ИнгредиентId = new SelectList(db.Ингредиенты, "ИнгредиентId", "Название", рецептИнгредиенты.ИнгредиентId);
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", рецептИнгредиенты.РецептId);
            return View(рецептИнгредиенты);
        }

        // GET: РецептИнгредиенты/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            РецептИнгредиенты рецептИнгредиенты = db.РецептИнгредиенты.Find(id);
            if (рецептИнгредиенты == null)
            {
                return HttpNotFound();
            }
            return View(рецептИнгредиенты);
        }

        // POST: РецептИнгредиенты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            РецептИнгредиенты рецептИнгредиенты = db.РецептИнгредиенты.Find(id);
            db.РецептИнгредиенты.Remove(рецептИнгредиенты);
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
