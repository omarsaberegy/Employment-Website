using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteZft.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace WebsiteZft.Controllers
{
     
    public class JobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Job/
         [Authorize(Roles="Publisher")]
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Category);
            var userid = User.Identity.GetUserId();

            

            var alljobs = from b in jobs
                          where b.UserID == userid
                          select b;
            return View(alljobs.ToList());


            //var jobs = db.ApplyforJobs.Where(a => a.UserId == userid).ToList();

        }

        public ActionResult GetPublic()
        {
            var jobs = db.Jobs.Include(j => j.Category);
            var userid = User.Identity.GetUserId();

            var alljobs = from b in jobs
                          where b.Category.CategoryName == "Public Jobs"
                          select b;

            
            return View(alljobs.ToList());

        }

        public ActionResult GetPrivate()
        {
            var jobs = db.Jobs.Include(j => j.Category);
            var userid = User.Identity.GetUserId();

            var alljobs = from b in jobs
                          where b.Category.CategoryName == "Private Jobs"
                          select b;


            return View(alljobs.ToList());

        }

        public ActionResult Getfree()
        {
            var jobs = db.Jobs.Include(j => j.Category);
            var userid = User.Identity.GetUserId();

            var alljobs = from b in jobs
                          where b.Category.CategoryName == "Free Jobs"
                          select b;


            return View(alljobs.ToList());

        }








       [Authorize(Roles = "Publisher")]
        public ActionResult DisplayPublisherJobs()
        {
            var userid = User.Identity.GetUserId();
            var jobs = from a in db.ApplyforJobs
                       join t in db.Jobs
                       on a.JobId equals t.Id
                       where t.User.Id == userid
                       select a;

            var alljobs = from b in db.Jobs
                          where b.UserID == userid
                          select b.JobName;

            ViewBag.CategoryId = new SelectList(alljobs.ToList());

            var grouped = from j in jobs
                          group j by j.job.JobName
                              into gr
                              select new JobsGrouping
                              {
                                  JobName = gr.Key,
                                  Items = gr

                              };

            return View(grouped.ToList());
        }


        // GET: /Job/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
         [Authorize(Roles = "Publisher")]
        // GET: /Job/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: /Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Job job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                job.JobImage = upload.FileName;
                job.UserID = User.Identity.GetUserId();
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
            return View(job);
        }
         [Authorize(Roles = "Publisher")]
        // GET: /Job/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
            return View(job);
        }

        // POST: /Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {

                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), job.JobImage);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    job.JobImage = upload.FileName;
                }

            
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryId);
            return View(job);
        }

        // GET: /Job/Delete/5
         [Authorize(Roles="Publisher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: /Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
