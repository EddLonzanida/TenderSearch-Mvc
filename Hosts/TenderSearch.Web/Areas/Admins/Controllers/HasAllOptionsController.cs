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
    public class HasAllOptionsController : Controller
    {
        private TenderSearchDb db = new TenderSearchDb();

        // GET: Admins/HasAllOptions
        public async Task<ActionResult> Index()
        {
            return View(await db.HasAllOptions.ToListAsync());
        }

        // GET: Admins/HasAllOptions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasAllOptions hasAllOptions = await db.HasAllOptions.FindAsync(id);
            if (hasAllOptions == null)
            {
                return HttpNotFound();
            }
            return View(hasAllOptions);
        }

        // GET: Admins/HasAllOptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/HasAllOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ParentId,Description,Name,DateDeleted,DeletionReason")] HasAllOptions hasAllOptions)
        {
            if (ModelState.IsValid)
            {
                db.HasAllOptions.Add(hasAllOptions);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hasAllOptions);
        }

        // GET: Admins/HasAllOptions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasAllOptions hasAllOptions = await db.HasAllOptions.FindAsync(id);
            if (hasAllOptions == null)
            {
                return HttpNotFound();
            }
            return View(hasAllOptions);
        }

        // POST: Admins/HasAllOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ParentId,Description,Name,DateDeleted,DeletionReason")] HasAllOptions hasAllOptions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hasAllOptions).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hasAllOptions);
        }

        // GET: Admins/HasAllOptions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasAllOptions hasAllOptions = await db.HasAllOptions.FindAsync(id);
            if (hasAllOptions == null)
            {
                return HttpNotFound();
            }
            return View(hasAllOptions);
        }

        // POST: Admins/HasAllOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HasAllOptions hasAllOptions = await db.HasAllOptions.FindAsync(id);
            db.HasAllOptions.Remove(hasAllOptions);
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
