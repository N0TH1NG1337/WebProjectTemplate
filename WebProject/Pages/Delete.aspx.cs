using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject.Pages
{
    public partial class Delete : System.Web.UI.Page
    {
        protected int UserId = -1;
        protected string Username = "";
        protected string Firstname = "";
        protected string Lastname = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                // we check if we are not admin to return the user to other page
                if (!bool.Parse(Session["Admin"].ToString()))
                    Response.Redirect("~/");
                else
                {
                    // do everything
                    if (Request.QueryString["id"] == "" || Request.QueryString["id"] == null)
                        Response.Redirect("/Pages/Admin");

                    UserId = int.Parse(Request.QueryString["id"]);
                    User UserObj = Helper.GetUserData(UserId);

                    // Save data to show before delete
                    Username = UserObj.userName;
                    Firstname = UserObj.firstName;
                    Lastname = UserObj.lastName;

                    if (IsPostBack)
                    {
                        Helper.Delete(UserId);
                        Response.Redirect("/Pages/Admin");
                    }

                }
            }
            else
            {
                // if the session is invalid
                Response.Redirect("/Pages/LogIn");
            }
        }
    }
}