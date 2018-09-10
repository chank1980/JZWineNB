using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using JZWineNB.Models;
using JZWineNB.viewmodels;

namespace JZWineNB.Controllers
{
    public class CommentController : Controller
    {
        private JZWineNBEntities3 db = new JZWineNBEntities3();
         
        // GET: Comment
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Drinker).Include(c => c.Wine);
            return View(comments.ToList());
        }

        // GET: Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comment/Create
        public ActionResult Create(int? id, string searchString)
        {
            CommentWineVM model = new CommentWineVM()
            {
                WineID = (int)id,
                Name = db.Wines.Find(id).Name,
                Date = DateTime.Now,                
            };
            
                    
            ViewBag.DrinkerID = new SelectList(db.Drinkers, "DrinkerID", "Alias");
            ViewBag.WineID = new SelectList(db.Wines, "WineID", "Name");

            return View(model);
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,Rate,Date,Comment1,DrinkerID,WineID")] Comment comment)
        {           

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                //Recalculate average rate for Wine.cs               
                ReCalRate(comment);

                return RedirectToAction("Index", "Wine");
            }

            ViewBag.DrinkerID = new SelectList(db.Drinkers, "DrinkerID", "Alias", comment.DrinkerID);
            ViewBag.WineID = new SelectList(db.Wines, "WineID", "Name", comment.WineID);
            return View(comment);
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            
            return View(comment);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,Rate,Date,Comment1,DrinkerID,WineID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
               
                //Recalculate average rate for Wine.cs               
                ReCalRate(comment);

                return RedirectToAction("Index");
            }
            ViewBag.DrinkerID = new SelectList(db.Drinkers, "DrinkerID", "Alias", comment.DrinkerID);
            ViewBag.WineID = new SelectList(db.Wines, "WineID", "Name", comment.WineID);
            return View(comment);
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
           
            //Recalculate average rate for Wine.cs            
            ReCalRate(comment);

            return RedirectToAction("Index");
        }

        private void ReCalRate(Comment comment)
        {
            decimal totalRate = 0;
            var count = 0;
            decimal averageRate = 0;
            var commentRecords = from c in db.Comments
                                 where c.WineID == comment.WineID
                                 select c;
            //Retrieve and add wine Rate
            foreach (var item in commentRecords)
            {
                if (item.Rate != null )
                {
                    totalRate += (decimal)item.Rate;
                }
               
                count++;

            }
                   
            if (totalRate >0 && count >0)
            {
                averageRate = totalRate / count;
            }
            else
            {
                averageRate = 0;
            }
            
            var wineRecord = db.Wines.SingleOrDefault(c => c.WineID == comment.WineID);
            wineRecord.RateOverall = averageRate;
            db.SaveChanges();
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
