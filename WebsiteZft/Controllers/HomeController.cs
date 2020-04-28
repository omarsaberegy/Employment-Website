using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteZft.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace WebsiteZft.Controllers
{
     
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Get all jobs by all category
        public ActionResult Index()
        {
            var list = db.Categories.ToList();

            return View(list);
        }

        [Authorize]
        public ActionResult JobDetails(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if(job==null)
            {
                return HttpNotFound();
            }
            Session["Job_Id"] = JobId;
            return View(job);
        }

        [Authorize]
        public ActionResult Apply()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string Message, HttpPostedFileBase up)
        {


            var userid = User.Identity.GetUserId();
            var jobid = (int)Session["Job_Id"];
            var load = db.Jobs.Find(jobid);
            if(load.UserID == userid)
            {
                ViewBag.Message = "You Can't Apply for this Job";
                return View();

            }

            

            var check = db.ApplyforJobs.Where(a => a.UserId == userid &&  a.JobId==jobid).ToList();

            if(check.Count < 1)
            {
                if(up != null)
                {

                    string path = Path.Combine(Server.MapPath("~/Uploads"), up.FileName);
                    up.SaveAs(path);
                }
               
                var job = new ApplyforJob();
                job.JobId = jobid;
                job.Message = Message;
                job.UserId = userid;
                job.CV = up.FileName;
                job.ApplyDate = DateTime.Now;
                db.ApplyforJobs.Add(job);
                db.SaveChanges();
                ViewBag.Message = "Thanks for Applying , Your Apply is Confirmed";
            }

            else
            {

                ViewBag.Message = "You already Apllied for this job";
            }
           
            return View();
        }

        [Authorize]
        public ActionResult GetJobsApplicationsByUser()
        {
            var userid = User.Identity.GetUserId();
            var jobs = db.ApplyforJobs.Where(a => a.UserId == userid ).ToList();

            return View(jobs);
        }
        [Authorize]
        public ActionResult DetailsofJobApplication(int Id)
        {
            var job = db.ApplyforJobs.Find(Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            
            return View(job);
        }

        public ActionResult EditJobApplication(int Id)
        {
            
            var job = db.ApplyforJobs.Find(Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            
            return View(job);
        }

        [HttpPost]
        public ActionResult EditJobApplication(ApplyforJob job, HttpPostedFileBase up)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), job.CV);

                if (up != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), up.FileName);
                    up.SaveAs(path);
                    job.CV = up.FileName;
                }
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsApplicationsByUser");
            }
            return View(job);
        }

        public ActionResult DeleteJobApplication(int id)
        {
            var role = db.ApplyforJobs.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost]
        public ActionResult DeleteJobApplication(ApplyforJob role)
        {
            var myrole = db.ApplyforJobs.Find(role.Id);
            db.ApplyforJobs.Remove(myrole);
            db.SaveChanges();
            return RedirectToAction("GetJobsApplicationsByUser");
        }



     

        public ActionResult Search()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var d = db.Jobs.Where(a => a.JobName.Contains(searchName)
          || a.JobContent.Contains(searchName)
          || a.Category.CategoryName.Contains(searchName)
          || a.Category.CategoryDescreption.Contains(searchName));

            return View(d.ToList());
        }

        public ActionResult Contact()
        {
            

            return View();
        }

        public ActionResult Management()
        {


            return View();
        }
        //[HttpPost]
        //public ActionResult Contact(Contact con)
        //{

        //    var mail=new MailMessage();
        //    var infolog = new NetworkCredential("rmero6yui6@gmail.com‏", "germanyforever10");
        //    mail.From = new MailAddress(con.Email);
        //    mail.To.Add(new MailAddress("rmero6yui6@gmail.com‏"));
        //    mail.Subject = con.MessageSubject;
        //    mail.IsBodyHtml = true;
            
          

        //    mail.Body = con.Message;
            

        //    var smtpclient = new SmtpClient("smtp.gmail.com", 587);
        //    smtpclient.EnableSsl = true;
        //    smtpclient.Credentials = infolog;
        //    smtpclient.Send(mail);
        //    return RedirectToAction("Index");
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}