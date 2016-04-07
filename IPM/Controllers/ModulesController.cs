using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IPM.DAL;
using IPM.Models;

namespace IPM.Controllers
{
    public class ModulesController : Controller
    {
        private IPMContext db = new IPMContext();

        // GET: Modules
        public ActionResult Index()
        {
            return View(db.Modules.ToList());
        }
        // GET: Modules
        public ActionResult Table()
        {
            return View(db.Modules.ToList());
        }
        public JsonResult GetData(int limit, int offset)
        {
            //int offset = 1;
            //int limit = 1;
            //var list = db.Modules.ToList().Select(t => new { t.ModuleName, t.CreatedBy });
           
            //var rows = list.Skip(offset).Take(limit).ToList();
            //var total = rows.Count;
            //return Json(new {total = total,rows=rows}, JsonRequestBehavior.AllowGet);
            var list = db.Modules.ToList();
            var total = list.Count;
            list = list.Skip(offset).Take(limit).ToList();
            return Json(new { total = total,rows = list},JsonRequestBehavior.AllowGet);
        }

        // GET: Modules/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleId,ModuleName,ParentId,ModuleType,Url,Controller,SortOrder,Remark,CreatedBy,CreatedDate")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Modules.Add(module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(module);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleId,ModuleName,ParentId,ModuleType,Url,Controller,SortOrder,Remark,CreatedBy,CreatedDate")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(module);
        }

        // GET: Modules/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
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
