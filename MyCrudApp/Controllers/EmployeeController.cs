using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCrudApp.Controllers
{
    using MyCrudApp.Models;
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult viewAllEmployees()
        {
            return View(getEmpList());
        }

        IEnumerable<Employee> getEmpList() {
            using (DbModelOne model = new DbModelOne())
            { 
                return model.Employees.ToList<Employee>();
            }
        }

        public ActionResult AddOrUpdateEmployee(int id=0) {
            Employee emp = new Employee();
            if (id != 0) {
                using (DbModelOne model = new DbModelOne()) {
                    emp = model.Employees.Where(x => x.empid == id).FirstOrDefault<Employee>();
                }
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult AddOrUpdateEmployee(Employee emp)
        {
            using (DbModelOne model = new DbModelOne()) {
                if (emp.empid == 0 && emp != null)
                {
                    model.Employees.Add(emp);
                }
                else {
                    model.Entry(emp).State = System.Data.EntityState.Modified;
                    
                }
                model.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int empid)
        {
            try { 
                using (DbModelOne model = new DbModelOne())
                {
                    Employee emp = model.Employees.Where(x => x.empid == empid).FirstOrDefault<Employee>();
                    model.Employees.Remove(emp);
                    model.SaveChanges();
                }
            }
            catch(Exception e) { 
                
            }
            return RedirectToAction("viewAllEmployees");
        }
    }
}
