using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class SiteMaster : MasterPage
    {
        // Create Data to save and use Later
        protected bool IsLoggedIn = false;
        protected bool IsAdmin = false;
        protected string FirstName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if our session is valid and we can use the data
            if (Session["userId"] != null)
            {
                // if we just logged in
                IsLoggedIn = true;

                // check if we have admin prems
                if (bool.Parse(Session["Admin"].ToString()))
                    IsAdmin = true;

                // and just save Firstname for later use
                FirstName = Session["firstName"].ToString();
            }
        }
    }
}