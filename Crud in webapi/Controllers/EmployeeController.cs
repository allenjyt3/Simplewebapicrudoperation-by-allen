using Crud_in_webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crud_in_webapi.Controllers
{
    public class EmployeeController : ApiController
    {
       
        public IHttpActionResult GetAllStudents()
        {
            List <tbl_Employees> employees = new List<tbl_Employees>();
            using (var context = new SchoolEntities())
            {
                employees = context.tbl_Employees.ToList();
            }

            if (employees.Count > 0)
                return Ok(employees);
            else
                return Ok("No employees found");
        }

        public IHttpActionResult PostNewEmployee(tbl_Employees employees)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid employee.");

            using (var context = new SchoolEntities())
            {
                context.tbl_Employees.Add(new tbl_Employees()
                {
                    EmpId = employees.EmpId,
                    EmpName = employees.EmpName,
                    Address = employees.Address,
                    Salary = employees.Salary
                });
                context.SaveChanges();
            }
            return Ok("New  Employee Added");
        }
        [HttpPost]
        [Route("edit")]
        public IHttpActionResult EditEmployee(tbl_Employees emp)
        {
            using (var context = new SchoolEntities())
            {
                var x = (from e in context.tbl_Employees where e.EmpId == emp.EmpId select e).FirstOrDefault();
                x.EmpId = emp.EmpId;
                x.EmpName = emp.EmpName;
                x.Address = emp.Address;
                x.Salary = emp.Salary;
                context.SaveChanges();
                return Ok("Inserted into db");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            SchoolEntities Context = new SchoolEntities();
            var y = (from e in Context.tbl_Employees where e.EmpId == id select e).FirstOrDefault();
            Context.tbl_Employees.Remove(y);
            Context.SaveChanges();
            return Ok("deleted from db");
        }



    }


}

