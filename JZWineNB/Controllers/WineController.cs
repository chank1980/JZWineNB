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

namespace JZWineNB.Controllers
{
    public class WineController : Controller
    {
        private JZWineNBEntities3 db = new JZWineNBEntities3();

        // GET: Wine
        public ActionResult Index(string searchString, string wineType)
        {
            //create a dropdown list for wine types
            List<string> typeList = new List<string>();
            var typeQuery = from t in db.Wines
                            orderby t.WineType
                            select t.WineType;
            typeList.AddRange(typeQuery.Distinct()); //unique in list
            ViewBag.wineType = new SelectList(typeList);
            var wines = from w in db.Wines
                        select w;
            if (!string.IsNullOrEmpty(wineType))
            {
                wines = wines.Where(x => x.WineType == wineType);
            }
            //Linq to write wine records
            //search by grape
            if (!string.IsNullOrEmpty(searchString))
            {
                wines = wines.Where(x => x.Grape.Contains(searchString));
            }

            foreach (var wine in wines)
            {
                wine.Description = wine.Description != null && wine.Description.Length > 70 
                    ? 
                    wine.Description.Substring(0, 70) + "..." 
                    :
                    wine.Description;
            }
            return View(wines);
        }

        // GET: Wine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // GET: Wine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WineID,Name,WineType,Grape,Region,Country,Description,Body,AlcoholVol,RateOverall,WineImage")] Wine wine)
        {
            //default wine images             

            if (wine.WineImage == null)
            {
                switch (wine.WineType)
                {
                    case "Red":
                        //use @ to escape '\' charater error
                        wine.WineImage = @"~\Content\Images\redWine.jpg";
                        break;
                    case "White":
                        wine.WineImage = @"~\Content\Images\whiteWine.jpg";
                        break;

                    default:
                        wine.WineImage = @"~\Content\Images\roseWine.jpg";
                        break;
                }
            }

            if (ModelState.IsValid)
            {
                db.Wines.Add(wine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wine);
        }

        // GET: Wine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // POST: Wine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WineID,Name,WineType,Grape,Region,Country,Description,Body,AlcoholVol,RateOverall,WineImage")] Wine wine)
        {
            if (wine.WineImage == null)
            {                
                switch (wine.WineType)
                {
                    case "Red":
                        //use @ to escape '\' charater error
                        wine.WineImage = @"~\Content\Images\redWine.jpg";
                        break;
                    case "White":
                        wine.WineImage = @"~\Content\Images\whiteWine.jpg";
                        break;

                    default:
                        wine.WineImage = @"~\Content\Images\roseWine.jpg";
                        break;
                }
            }

                if (ModelState.IsValid)
            {
                db.Entry(wine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wine);
        }

        // GET: Wine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // POST: Wine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //delete records in Comment before Wine
            db.Comments.RemoveRange(db.Comments.Where(c => c.WineID == id));
            // delete wine record
            Wine wine = db.Wines.Find(id);
            db.Wines.Remove(wine);
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
