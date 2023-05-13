using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject.Pages
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // We dont touch the PageLoad if we will use LogInMessage
        }

        public string LogInMessage()
        {
            if (IsPostBack)
            {
                // Get Username and Password
                string Username = Request.Form["userName"];
                string Password = Request.Form["password"];

                // Get the User Row Data if exist into Row obj by Username and Password
                User Row = Helper.GetRow(Username, Password);

                if (Row.userId == -1) // do check if user is invalid
                    // if the user is invalid then print error
                    return "inccorect username or password";
                else
                {
                    // if no, create session to save user data while logged in
                    Session["userId"] = Row.userId;
                    Session["userName"] = Row.userName;
                    Session["firstName"] = Row.firstName;
                    Session["Admin"] = Row.Admin;
                    
                    // and redirect to for Login Users Page
                    Response.Redirect("~/");

                }
            }

            // in any case we return just "" to avoid issues
            return String.Empty;
        }
    }
}