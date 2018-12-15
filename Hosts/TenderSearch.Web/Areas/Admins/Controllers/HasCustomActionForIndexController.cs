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
    public class HasCustomActionForIndexController : Controller
    {
        private TenderSearchDb db = new TenderSearchDb();

        // GET: Admins/HasCustomActionForIndex
        public async Task<ActionResult> Index()
        {
            return View(await db.HasCustomActionForIndexes.ToListAsync());
        }

        // GET: Admins/HasCustomActionForIndex/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasCustomActionForIndex hasCustomActionForIndex = await db.HasCustomActionForIndexes.FindAsync(id);
            if (hasCustomActionForIndex == null)
            {
                return HttpNotFound();
            }
            return View(hasCustomActionForIndex);
        }

        // GET: Admins/HasCustomActionForIndex/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/HasCustomActionForIndex/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Name,DateDeleted,DeletionReason")] HasCustomActionForIndex hasCustomActionForIndex)
        {
            if (ModelState.IsValid)
            {
                db.HasCustomActionForIndexes.Add(hasCustomActionForIndex);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hasCustomActionForIndex);
        }

        // GET: Admins/HasCustomActionForIndex/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasCustomActionForIndex hasCustomActionForIndex = await db.HasCustomActionForIndexes.FindAsync(id);
            if (hasCustomActionForIndex == null)
            {
                return HttpNotFound();
            }
            return View(hasCustomActionForIndex);
        }

        // POST: Admins/HasCustomActionForIndex/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Name,DateDeleted,DeletionReason")] HasCustomActionForIndex hasCustomActionForIndex)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hasCustomActionForIndex).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hasCustomActionForIndex);
        }

        // GET: Admins/HasCustomActionForIndex/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasCustomActionForIndex hasCustomActionForIndex = await db.HasCustomActionForIndexes.FindAsync(id);
            if (hasCustomActionForIndex == null)
            {
                return HttpNotFound();
            }
            return View(hasCustomActionForIndex);
        }

        // POST: Admins/HasCustomActionForIndex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HasCustomActionForIndex hasCustomActionForIndex = await db.HasCustomActionForIndexes.FindAsync(id);
            db.HasCustomActionForIndexes.Remove(hasCustomActionForIndex);
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
