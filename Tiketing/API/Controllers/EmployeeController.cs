﻿using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    public class EmployeeController : ApiController
    {
        ApplicationDbContext myContext = new ApplicationDbContext();

        public IQueryable<EmployeeVM> GetEmployees()
        {
            return myContext.Employee;
        }

        [ResponseType(typeof(EmployeeVM))]
        public IHttpActionResult GetEmployess(int id)
        {
            EmployeeVM employee = myContext.Employee.Find(id);
            return Ok(employee);
        }

        [ResponseType(typeof(EmployeeVM))]
        public IHttpActionResult Post(EmployeeVM employee, int id)
        {
            myContext.Employee.Add(employee);
            myContext.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = employee.id }, employee);
        }

        //[ResponseType(typeof(void))]
        //public IHttpActionResult put(EmployeeVM employee, int id)
        //{
        //    var put = myContext.Employee.Find(id);
        //    myContext.first_name = employee.first_name;
        //    myContext.last_name = employee.last_name;
        //    myContext.birthday = employee.birth_day;
        //    myContext.gender = employee.gender;
        //    myContext.email = employee.email;
        //    myContext.phone = employee.phone;
        //    myContext.Entry(put).State = EntityState.Modified;
        //    myContext.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}
    }
}
