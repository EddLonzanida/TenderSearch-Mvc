using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TenderSearch.Business.Common.Entities;
using TenderSearch.Data;

namespace TenderSearch.Web.Areas.Admins.Controllers
{
    public class IsSoftDeleteController : Controller
    {
        private TenderSearchDb db = new TenderSearchDb();

        // GET: Admins/IsSoftDelete
        public async Task<ActionResult> Index()
        {
            return View(await db.IsSoftDeletes.ToListAsync());
        }

        // GET: Admins/IsSoftDelete/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsSoftDelete isSoftDelete = await db.IsSoftDeletes.FindAsync(id);
            if (isSoftDelete == null)
            {
                return HttpNotFound();
            }
            return View(isSoftDelete);
        }

        // GET: Admins/IsSoftDelete/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/IsSoftDelete/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Name,DateDeleted,DeletionReason")] IsSoftDelete isSoftDelete)
        {
            if (ModelState.IsValid)
            {
                db.IsSoftDeletes.Add(isSoftDelete);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(isSoftDelete);
        }

        // GET: Admins/IsSoftDelete/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsSoftDelete isSoftDelete = await db.IsSoftDeletes.FindAsync(id);
            if (isSoftDelete == null)
            {
                return HttpNotFound();
            }
            return View(isSoftDelete);
        }

        // POST: Admins/IsSoftDelete/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Name,DateDeleted,DeletionReason")] IsSoftDelete isSoftDelete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(isSoftDelete).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(isSoftDelete);
        }

        // GET: Admins/IsSoftDelete/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsSoftDelete isSoftDelete = await db.IsSoftDeletes.FindAsync(id);
            if (isSoftDelete == null)
            {
                return HttpNotFound();
            }
            return View(isSoftDelete);
        }

        // POST: Admins/IsSoftDelete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IsSoftDelete isSoftDelete = await db.IsSoftDeletes.FindAsync(id);
            db.IsSoftDeletes.Remove(isSoftDelete);
            await db.SaveChangesAsync();
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
