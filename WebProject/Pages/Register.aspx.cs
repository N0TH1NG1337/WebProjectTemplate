using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user pressed on button Submit / Pressed on enter
            if (IsPostBack)
            {
                // Save every Element from the input boxes by their id/name
                string Username = Request.Form["userName"];
                string Password = Request.Form["password"];
                string Firstname = Request.Form["firstName"];
                string Lastname = Request.Form["lastName"];
                DateTime Birthday = DateTime.Parse(Request.Form["birthday"]); // Convert String to Date type
                string City = Request.Form["city"];

                User user = new User(); // Create new User object

                // Enter every Data we have
                user.userName = Username;
                user.password = Password;
                user.firstName = Firstname;
                user.lastName = Lastname;
                user.birthday = Birthday;
                user.city = City;

                // Insert User Object to Table
                Helper.Insert(user);
            }
        }
    }
}