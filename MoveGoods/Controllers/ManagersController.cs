using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoveGoods;

namespace MoveGoods.Controllers
{
    public class ManagersController : Controller
    {
        private MGDBContainer db = new MGDBContainer();

        // GET: Managers
        public ActionResult Index()
        {
            return View(db.ManagerSet.ToList());
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.ManagerSet.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            ViewBag.Managers = db.ManagerSet.ToList().Select((x, y) => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            return View();
        }

        // POST: Managers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SAM,Name,ParentId,Description")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                string m = Request.Form["Parent"];
                if (string.IsNullOrEmpty(m) == false)
                {
                    Manager mgr = new Manager()
                    {
                        Id = int.Parse(m)
                    };
                    db.ManagerSet.Attach(mgr);
                    manager.ParentManager = mgr;
                }
                db.ManagerSet.Add(manager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manager);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.ManagerSet.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stores = manager.Stores.Select((x, y) => new StoreModel() { Id = x.Id, Name = x.Name });
            ViewBag.Managers = db.ManagerSet.ToList().Select((x, y) => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            return View(manager);
        }

        // POST: Managers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SAM,Name,ParentId,Description")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                string m = Request.Form["Parent"];
                if (string.IsNullOrEmpty(m) == false)
                {
                    Manager mgr = new Manager()
                    {
                        Id = int.Parse(m)
                    };
                    db.ManagerSet.Attach(mgr);
                    manager.ParentManager = mgr;
                }
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manager);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.ManagerSet.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.ManagerSet.Find(id);
            db.ManagerSet.Remove(manager);
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
