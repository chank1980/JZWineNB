using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JZWineNB.Models;

namespace JZWineNB.Controllers
{
    public class DrinkerController : Controller
    {
        private JZWineNBEntities3 db = new JZWineNBEntities3();

        // GET: Drinker
        public ActionResult Index()
        {
            return View(db.Drinkers.ToList());
        }

        // GET: Drinker
        public ActionResult Alias(string gender)
        {
            List<string> genderList = new List<string>();
            var genderQuery = from g in db.Drinkers
                              orderby g.Gender
                              select g.Gender;
            genderList.AddRange(genderQuery.Distinct());
            ViewBag.gender = new SelectList(genderList);
            var drinkers = from d in db.Drinkers
                           select d;
            if (!String.IsNullOrEmpty(gender))
            {
                drinkers = drinkers.Where(x => x.Gender==gender);
            }
                           
           

            return View(drinkers);
        }



        // GET: Drinker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drinker drinker = db.Drinkers.Find(id);
            if (drinker == null)
            {
                return HttpNotFound();
            }
            return View(drinker);
        }

        // GET: Drinker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drinker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DrinkerID,Alias,Gender,DrinkerImage")] Drinker drinker)
        {
            if (ModelState.IsValid)
            {
                db.Drinkers.Add(drinker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drinker);
        }

        // GET: Drinker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drinker drinker = db.Drinkers.Find(id);
            if (drinker == null)
            {
                return HttpNotFound();
            }
            return View(drinker);
        }

        // POST: Drinker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DrinkerID,Alias,Gender,DrinkerImage")] Drinker drinker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drinker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drinker);
        }

        // GET: Drinker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drinker drinker = db.Drinkers.Find(id);
            if (drinker == null)
            {
                return HttpNotFound();
            }
            return View(drinker);
        }

        // POST: Drinker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drinker drinker = db.Drinkers.Find(id);
            db.Drinkers.Remove(drinker);
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
