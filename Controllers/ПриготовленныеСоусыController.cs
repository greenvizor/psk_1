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
    public class ПриготовленныеСоусыController : Controller
    {
        private psk_dbEntities1 db = new psk_dbEntities1();

        // GET: ПриготовленныеСоусы
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.BatchNumberSortParm = sortOrder == "BatchNumber" ? "batchNumber_desc" : "BatchNumber";
            ViewBag.ProductionDateSortParm = sortOrder == "ProductionDate" ? "productionDate_desc" : "ProductionDate";
            ViewBag.ResponsibleSortParm = sortOrder == "Responsible" ? "responsible_desc" : "Responsible";
            ViewBag.SauceSortParm = sortOrder == "Sauce" ? "sauce_desc" : "Sauce";

            var sauces = from s in db.ПриготовленныеСоусы
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sauces = sauces.Where(s => s.Название.Contains(searchString)
                                       || s.Количество.ToString().Contains(searchString)
                                       || s.НомерПартии.ToString().Contains(searchString)
                                       || s.ДатаПроизводства.ToString().Contains(searchString)
                                       || s.Ответственный.Contains(searchString)
                                       || s.Соусы.Название.Contains(searchString)
                                       || s.Рецепты.Название.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sauces = sauces.OrderByDescending(s => s.Название);
                    break;
                case "Quantity":
                    sauces = sauces.OrderBy(s => s.Количество);
                    break;
                case "quantity_desc":
                    sauces = sauces.OrderByDescending(s => s.Количество);
                    break;
                case "BatchNumber":
                    sauces = sauces.OrderBy(s => s.НомерПартии);
                    break;
                case "batchNumber_desc":
                    sauces = sauces.OrderByDescending(s => s.НомерПартии);
                    break;
                case "ProductionDate":
                    sauces = sauces.OrderBy(s => s.ДатаПроизводства);
                    break;
                case "productionDate_desc":
                    sauces = sauces.OrderByDescending(s => s.ДатаПроизводства);
                    break;
                case "Responsible":
                    sauces = sauces.OrderBy(s => s.Ответственный);
                    break;
                case "responsible_desc":
                    sauces = sauces.OrderByDescending(s => s.Ответственный);
                    break;
                case "Sauce":
                    sauces = sauces.OrderBy(s => s.Соусы.Название);
                    break;
                case "sauce_desc":
                    sauces = sauces.OrderByDescending(s => s.Соусы.Название);
                    break;
                default:
                    sauces = sauces.OrderBy(s => s.Название);
                    break;
            }

            return View(sauces.ToList());
        }



        // GET: ПриготовленныеСоусы/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ПриготовленныеСоусы приготовленныеСоусы = db.ПриготовленныеСоусы.Find(id);
            if (приготовленныеСоусы == null)
            {
                return HttpNotFound();
            }
            return View(приготовленныеСоусы);
        }

        // GET: ПриготовленныеСоусы/Create
        public ActionResult Create()
        {
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название");
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название");
            return View();
        }

        // POST: ПриготовленныеСоусы/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "СоусId,Название,Количество,НомерПартии,ДатаПроизводства,Ответственный,РецептId")] ПриготовленныеСоусы приготовленныеСоусы)
        {
            if (ModelState.IsValid)
            {
                db.ПриготовленныеСоусы.Add(приготовленныеСоусы);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", приготовленныеСоусы.РецептId);
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", приготовленныеСоусы.СоусId);
            return View(приготовленныеСоусы);
        }

        // GET: ПриготовленныеСоусы/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ПриготовленныеСоусы приготовленныеСоусы = db.ПриготовленныеСоусы.Find(id);
            if (приготовленныеСоусы == null)
            {
                return HttpNotFound();
            }
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", приготовленныеСоусы.РецептId);
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", приготовленныеСоусы.СоусId);
            return View(приготовленныеСоусы);
        }

        // POST: ПриготовленныеСоусы/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "СоусId,Название,Количество,НомерПартии,ДатаПроизводства,Ответственный,РецептId")] ПриготовленныеСоусы приготовленныеСоусы)
        {
            if (ModelState.IsValid)
            {
                db.Entry(приготовленныеСоусы).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.РецептId = new SelectList(db.Рецепты, "РецептId", "Название", приготовленныеСоусы.РецептId);
            ViewBag.СоусId = new SelectList(db.Соусы, "СоусId", "Название", приготовленныеСоусы.СоусId);
            return View(приготовленныеСоусы);
        }

        // GET: ПриготовленныеСоусы/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ПриготовленныеСоусы приготовленныеСоусы = db.ПриготовленныеСоусы.Find(id);
            if (приготовленныеСоусы == null)
            {
                return HttpNotFound();
            }
            return View(приготовленныеСоусы);
        }

        // POST: ПриготовленныеСоусы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ПриготовленныеСоусы приготовленныеСоусы = db.ПриготовленныеСоусы.Find(id);
            db.ПриготовленныеСоусы.Remove(приготовленныеСоусы);
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
