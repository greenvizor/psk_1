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
    public class СоусыController : Controller
    {
        private psk_dbEntities1 db = new psk_dbEntities1();

        // GET: Соусы
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var sauces = from s in db.Соусы
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sauces = sauces.Where(s => s.Название.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sauces = sauces.OrderByDescending(s => s.Название);
                    break;
                case "Date":
                    sauces = sauces.OrderBy(s => s.ДатаПроизводства);
                    break;
                case "date_desc":
                    sauces = sauces.OrderByDescending(s => s.ДатаПроизводства);
                    break;
                default:
                    sauces = sauces.OrderBy(s => s.Название);
                    break;
            }

            return View(sauces.ToList());
        }



        // GET: Соусы/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Соусы соусы = db.Соусы.Find(id);
            if (соусы == null)
            {
                return HttpNotFound();
            }
            return View(соусы);
        }

        // GET: Соусы/Create
        public ActionResult Create()
        {
            ViewBag.СоусId = new SelectList(db.ПриготовленныеСоусы, "СоусId", "Название");
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название");
            return View();
        }

        // POST: Соусы/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "СоусId,Название,НомерПартии,ДатаПроизводства,Ответственный,РецептId")] Соусы соусы)
        {
            if (ModelState.IsValid)
            {
                db.Соусы.Add(соусы);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.СоусId = new SelectList(db.ПриготовленныеСоусы, "СоусId", "Название", соусы.СоусId);
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", соусы.РецептId);
            return View(соусы);
        }

        // GET: Соусы/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Соусы соусы = db.Соусы.Find(id);
            if (соусы == null)
            {
                return HttpNotFound();
            }
            ViewBag.СоусId = new SelectList(db.ПриготовленныеСоусы, "СоусId", "Название", соусы.СоусId);
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", соусы.РецептId);
            return View(соусы);
        }

        // POST: Соусы/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "СоусId,Название,НомерПартии,ДатаПроизводства,Ответственный,РецептId")] Соусы соусы)
        {
            if (ModelState.IsValid)
            {
                db.Entry(соусы).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.СоусId = new SelectList(db.ПриготовленныеСоусы, "СоусId", "Название", соусы.СоусId);
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", соусы.РецептId);
            return View(соусы);
        }

        // GET: Соусы/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Соусы соусы = db.Соусы.Find(id);
            if (соусы == null)
            {
                return HttpNotFound();
            }
            return View(соусы);
        }

        // POST: Соусы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Соусы соусы = db.Соусы.Find(id);
            db.Соусы.Remove(соусы);
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
