using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bài_tập_Quản_lý_Project.Models;
using PagedList;

namespace Bài_tập_Quản_lý_Project.Controllers
{
    public class AssignTasksController : Controller
    {
        private QLProject_Context db = new QLProject_Context();

        // GET: AssignTasks
        [OutputCache(CacheProfile = "1MinutCatche")]
        public ActionResult Index(string SearchString, string Department, string Company, string Date, int? pageNo)
        {
            var asss = from a in db.AssignTasks
                              join e in db.Employees
                              on a.EmployeeId equals e.EmployeeId
                              join c in db.Clients
                              on a.ClientId equals c.ClientId
                              join p in db.Projects
                              on a.ProjectId equals p.ProjectId
                              select a;

            int pageSize = 2;
            int NoOffPage = (pageNo ?? 1);
            asss = asss.OrderBy(a => a.AssignTaskId);

            //dropdown Department
            var DepartmentLst = new List<String>();
            var DepartmentQry = from d in db.Employees
                                orderby d.EmployeeId
                                select d.EmployeeDepartment;
            DepartmentLst.AddRange(DepartmentQry.Distinct());
            ViewBag.Department = new SelectList(DepartmentLst);

            //dropdown Company
            var CompanyLst = new List<String>();
            var CompanyQry = from c in db.Clients
                             orderby c.ClientId
                             select c.ClientCompany;
            CompanyLst.AddRange(CompanyQry.Distinct());
            ViewBag.Company = new SelectList(CompanyLst);

            var DateLst = new List<String>() { "Đang thực hiện", "Đã hoàn thành" };
            ViewBag.Date = new SelectList(DateLst);

             
            if (!String.IsNullOrEmpty(SearchString))
            {
                asss = asss.Where(a => 
                   a.Employee.EmployeeName.Contains(SearchString)
                || a.Client.ClientName.Contains(SearchString)
                || a.Project.ProjectName.Contains(SearchString));
            }
            if (!String.IsNullOrEmpty(Department))
            {
                asss = asss.Where(a => a.Employee.EmployeeDepartment.Contains(Department));
            }
            if (!String.IsNullOrEmpty(Company))
            {
                asss = asss.Where(a => a.Client.ClientCompany.Contains(Company));
            }

            //var assignTasks = db.AssignTasks.Include(a => a.Client).Include(a => a.Employee).Include(a => a.Project);
            return View(asss.ToPagedList(NoOffPage,pageSize));
        }
        //var assignTasks = db.AssignTasks.Include(a => a.Client).Include(a => a.Employee).Include(a => a.Project);
        //    return View(assignTasks.ToList());


        // GET: AssignTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            return View(assignTask);
        }

        // GET: AssignTasks/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: AssignTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignTaskId,EmployeeId,ClientId,ProjectId,Task,Note")] AssignTask assignTask)
        {
            if (ModelState.IsValid)
            {
                db.AssignTasks.Add(assignTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", assignTask.ProjectId);
            return View(assignTask);
        }

        // GET: AssignTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", assignTask.ProjectId);
            return View(assignTask);
        }

        // POST: AssignTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignTaskId,EmployeeId,ClientId,ProjectId,Task,Note")] AssignTask assignTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", assignTask.ProjectId);
            return View(assignTask);
        }

        // GET: AssignTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            return View(assignTask);
        }

        // POST: AssignTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignTask assignTask = db.AssignTasks.Find(id);
            db.AssignTasks.Remove(assignTask);
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
