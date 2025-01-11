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
    public class ОклейкаЭтикеткамиController : Controller
    {
        private psk_dbEntities1 db = new psk_dbEntities1();

        // GET: ОклейкаЭтикетками
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var labels = from label in db.ОклейкаЭтикетками
                         select label;

            if (!String.IsNullOrEmpty(searchString))
            {
                labels = labels.Where(label => label.НазваниеБренда.Contains(searchString)
                                           || label.Ответственный.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    labels = labels.OrderByDescending(label => label.НазваниеБренда);
                    break;
                case "Date":
                    labels = labels.OrderBy(label => label.Ответственный);
                    break;
                case "date_desc":
                    labels = labels.OrderByDescending(label => label.Ответственный);
                    break;
                default:
                    labels = labels.OrderBy(label => label.НазваниеБренда);
                    break;
            }

            return View(labels.ToList());
        }


        // GET: ОклейкаЭтикетками/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ОклейкаЭтикетками оклейкаЭтикетками = db.ОклейкаЭтикетками.Find(id);
            if (оклейкаЭтикетками == null)
            {
                return HttpNotFound();
            }
            return View(оклейкаЭтикетками);
        }

        // GET: ОклейкаЭтикетками/Create
        public ActionResult Create()
        {
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название");
            return View();
        }

        // POST: ОклейкаЭтикетками/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ОклейкаЭтикеткамиId,СоусId,НазваниеБренда,Количество,Ответственный")] ОклейкаЭтикетками оклейкаЭтикетками)
        {
            if (ModelState.IsValid)
            {
                db.ОклейкаЭтикетками.Add(оклейкаЭтикетками);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", оклейкаЭтикетками.СоусId);
            return View(оклейкаЭтикетками);
        }

        // GET: ОклейкаЭтикетками/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ОклейкаЭтикетками оклейкаЭтикетками = db.ОклейкаЭтикетками.Find(id);
            if (оклейкаЭтикетками == null)
            {
                return HttpNotFound();
            }
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", оклейкаЭтикетками.СоусId);
            return View(оклейкаЭтикетками);
        }

        // POST: ОклейкаЭтикетками/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ОклейкаЭтикеткамиId,СоусId,НазваниеБренда,Количество,Ответственный")] ОклейкаЭтикетками оклейкаЭтикетками)
        {
            if (ModelState.IsValid)
            {
                db.Entry(оклейкаЭтикетками).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", оклейкаЭтикетками.СоусId);
            return View(оклейкаЭтикетками);
        }

        // GET: ОклейкаЭтикетками/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ОклейкаЭтикетками оклейкаЭтикетками = db.ОклейкаЭтикетками.Find(id);
            if (оклейкаЭтикетками == null)
            {
                return HttpNotFound();
            }
            return View(оклейкаЭтикетками);
        }

        // POST: ОклейкаЭтикетками/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ОклейкаЭтикетками оклейкаЭтикетками = db.ОклейкаЭтикетками.Find(id);
            db.ОклейкаЭтикетками.Remove(оклейкаЭтикетками);
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
