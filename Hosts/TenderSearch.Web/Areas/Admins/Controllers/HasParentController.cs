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
    public class HasParentController : Controller
    {
        private TenderSearchDb db = new TenderSearchDb();

        // GET: Admins/HasParent
        public async Task<ActionResult> Index()
        {
            return View(await db.HasParents.ToListAsync());
        }

        // GET: Admins/HasParent/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasParent hasParent = await db.HasParents.FindAsync(id);
            if (hasParent == null)
            {
                return HttpNotFound();
            }
            return View(hasParent);
        }

        // GET: Admins/HasParent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/HasParent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ParentId,Name,DateDeleted,DeletionReason")] HasParent hasParent)
        {
            if (ModelState.IsValid)
            {
                db.HasParents.Add(hasParent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hasParent);
        }

        // GET: Admins/HasParent/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasParent hasParent = await db.HasParents.FindAsync(id);
            if (hasParent == null)
            {
                return HttpNotFound();
            }
            return View(hasParent);
        }

        // POST: Admins/HasParent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ParentId,Name,DateDeleted,DeletionReason")] HasParent hasParent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hasParent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hasParent);
        }

        // GET: Admins/HasParent/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasParent hasParent = await db.HasParents.FindAsync(id);
            if (hasParent == null)
            {
                return HttpNotFound();
            }
            return View(hasParent);
        }

        // POST: Admins/HasParent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HasParent hasParent = await db.HasParents.FindAsync(id);
            db.HasParents.Remove(hasParent);
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
