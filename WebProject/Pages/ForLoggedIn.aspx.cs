using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject.Pages
{
    public partial class ForLoggedIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check if our session if invalid (if we logged in or not)
            if (Session["userId"] == null)
                // session is invalid
                Response.Redirect("/Pages/Login");
        }
    }
}