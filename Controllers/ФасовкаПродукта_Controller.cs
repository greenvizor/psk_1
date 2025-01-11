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
    public class ФасовкаПродукта_Controller : Controller
    {
        private psk_dbEntities1 db = new psk_dbEntities1();

        // GET: ФасовкаПродукта_
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.BrandSortParm = String.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.ResponsibleSortParm = sortOrder == "Responsible" ? "responsible_desc" : "Responsible";

            var packaging = from p in db.ФасовкаПродукта_
                            select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                packaging = packaging.Where(p => p.Соусы.Название.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "brand_desc":
                    packaging = packaging.OrderByDescending(p => p.Соусы.Название);
                    break;
                case "Quantity":
                    packaging = packaging.OrderBy(p => p.Количество);
                    break;
                case "quantity_desc":
                    packaging = packaging.OrderByDescending(p => p.Количество);
                    break;
                case "Responsible":
                    packaging = packaging.OrderBy(p => p.Ответственный);
                    break;
                case "responsible_desc":
                    packaging = packaging.OrderByDescending(p => p.Ответственный);
                    break;
                default:
                    packaging = packaging.OrderBy(p => p.Соусы.Название);
                    break;
            }

            return View(packaging.ToList());
        }

        // GET: ФасовкаПродукта_/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ФасовкаПродукта_ фасовкаПродукта_ = db.ФасовкаПродукта_.Find(id);
            if (фасовкаПродукта_ == null)
            {
                return HttpNotFound();
            }
            return View(фасовкаПродукта_);
        }

        // GET: ФасовкаПродукта_/Create
        public ActionResult Create()
        {
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название");
            return View();
        }

        // POST: ФасовкаПродукта_/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ФасовкаПродуктаId,СоусId,Тара,Количество,Ответственный")] ФасовкаПродукта_ фасовкаПродукта_)
        {
            if (ModelState.IsValid)
            {
                db.ФасовкаПродукта_.Add(фасовкаПродукта_);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", фасовкаПродукта_.СоусId);
            return View(фасовкаПродукта_);
        }

        // GET: ФасовкаПродукта_/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ФасовкаПродукта_ фасовкаПродукта_ = db.ФасовкаПродукта_.Find(id);
            if (фасовкаПродукта_ == null)
            {
                return HttpNotFound();
            }
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", фасовкаПродукта_.СоусId);
            return View(фасовкаПродукта_);
        }

        // POST: ФасовкаПродукта_/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ФасовкаПродуктаId,СоусId,Тара,Количество,Ответственный")] ФасовкаПродукта_ фасовкаПродукта_)
        {
            if (ModelState.IsValid)
            {
                db.Entry(фасовкаПродукта_).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", фасовкаПродукта_.СоусId);
            return View(фасовкаПродукта_);
        }

        // GET: ФасовкаПродукта_/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ФасовкаПродукта_ фасовкаПродукта_ = db.ФасовкаПродукта_.Find(id);
            if (фасовкаПродукта_ == null)
            {
                return HttpNotFound();
            }
            return View(фасовкаПродукта_);
        }

        // POST: ФасовкаПродукта_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ФасовкаПродукта_ фасовкаПродукта_ = db.ФасовкаПродукта_.Find(id);
            db.ФасовкаПродукта_.Remove(фасовкаПродукта_);
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
