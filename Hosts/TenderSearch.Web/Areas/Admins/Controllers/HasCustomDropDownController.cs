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
    public class HasCustomDropDownController : Controller
    {
        private TenderSearchDb db = new TenderSearchDb();

        // GET: Admins/HasCustomDropDown
        public async Task<ActionResult> Index()
        {
            return View(await db.HasCustomDropDowns.ToListAsync());
        }

        // GET: Admins/HasCustomDropDown/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasCustomDropDown hasCustomDropDown = await db.HasCustomDropDowns.FindAsync(id);
            if (hasCustomDropDown == null)
            {
                return HttpNotFound();
            }
            return View(hasCustomDropDown);
        }

        // GET: Admins/HasCustomDropDown/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/HasCustomDropDown/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Name,DateDeleted,DeletionReason")] HasCustomDropDown hasCustomDropDown)
        {
            if (ModelState.IsValid)
            {
                db.HasCustomDropDowns.Add(hasCustomDropDown);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hasCustomDropDown);
        }

        // GET: Admins/HasCustomDropDown/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasCustomDropDown hasCustomDropDown = await db.HasCustomDropDowns.FindAsync(id);
            if (hasCustomDropDown == null)
            {
                return HttpNotFound();
            }
            return View(hasCustomDropDown);
        }

        // POST: Admins/HasCustomDropDown/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Name,DateDeleted,DeletionReason")] HasCustomDropDown hasCustomDropDown)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hasCustomDropDown).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hasCustomDropDown);
        }

        // GET: Admins/HasCustomDropDown/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HasCustomDropDown hasCustomDropDown = await db.HasCustomDropDowns.FindAsync(id);
            if (hasCustomDropDown == null)
            {
                return HttpNotFound();
            }
            return View(hasCustomDropDown);
        }

        // POST: Admins/HasCustomDropDown/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HasCustomDropDown hasCustomDropDown = await db.HasCustomDropDowns.FindAsync(id);
            db.HasCustomDropDowns.Remove(hasCustomDropDown);
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
