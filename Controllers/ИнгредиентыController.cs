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
    public class ИнгредиентыController : Controller
    {
        private psk_dbEntities1 db = new psk_dbEntities1();

        // GET: Ингредиенты
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var ingredients = from i in db.Ингредиенты
                              select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                ingredients = ingredients.Where(i => i.Название.Contains(searchString)
                                              || i.ЕдиницаИзмерения.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    ingredients = ingredients.OrderByDescending(i => i.Название);
                    break;
                case "Date":
                    ingredients = ingredients.OrderBy(i => i.СрокГодности);
                    break;
                case "date_desc":
                    ingredients = ingredients.OrderByDescending(i => i.СрокГодности);
                    break;
                default:
                    ingredients = ingredients.OrderBy(i => i.Название);
                    break;
            }

            return View(ingredients.ToList());
        }


        // GET: Ингредиенты/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ингредиенты ингредиенты = db.Ингредиенты.Find(id);
            if (ингредиенты == null)
            {
                return HttpNotFound();
            }
            return View(ингредиенты);
        }

        // GET: Ингредиенты/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ингредиенты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ИнгредиентId,Название,ЕдиницаИзмерения,СрокГодности")] Ингредиенты ингредиенты)
        {
            if (ModelState.IsValid)
            {
                db.Ингредиенты.Add(ингредиенты);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ингредиенты);
        }

        // GET: Ингредиенты/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ингредиенты ингредиенты = db.Ингредиенты.Find(id);
            if (ингредиенты == null)
            {
                return HttpNotFound();
            }
            return View(ингредиенты);
        }

        // POST: Ингредиенты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ИнгредиентId,Название,ЕдиницаИзмерения,СрокГодности")] Ингредиенты ингредиенты)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ингредиенты).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ингредиенты);
        }

        // GET: Ингредиенты/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ингредиенты ингредиенты = db.Ингредиенты.Find(id);
            if (ингредиенты == null)
            {
                return HttpNotFound();
            }
            return View(ингредиенты);
        }

        // POST: Ингредиенты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ингредиенты ингредиенты = db.Ингредиенты.Find(id);
            db.Ингредиенты.Remove(ингредиенты);
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
