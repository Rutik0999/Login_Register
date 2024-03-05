using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Employee_login.Models;

namespace Employee_login.Controllers
{
    public class FormController : Controller
    {
        mvcDBEntities db = new mvcDBEntities();
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(Emp_data log)
        {
            var user = db.Emp_data.Where(x => x.Employee_code == log.Employee_code && x.Pass == log.Pass).Count();
            if (user > 0)
            {
                return RedirectToAction("profile");
            }
            else
            {
                ViewBag.msg = "Invalid login";
                return View();
            }
        }

        public ActionResult profile()
        {
            // Retrieve the ID of the currently logged-in user
            var loggedInUserId = User.Identity.Name; // Assuming the username is used as the user identifier

            // Retrieve data from the database for the logged-in user
            var loggedInUser = db.Emp_data.FirstOrDefault(e => e.Employee_code == loggedInUserId);

            if (loggedInUser != null)
            {
                return View(new List<Emp_data> { loggedInUser });
            }
            else
            {
                // Handle the case where the user is not found
                return RedirectToAction("login"); // Redirect to the login page or handle it based on your application flow
            }
        }







    }
}