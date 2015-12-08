using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Css_MasterPageCMS : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Проверяем, зашел ли пользователь
        if (Session["login"] != null)
        {
            lblLogin
            lblLogin.Text = "Welcome " + Session["login"].ToString();
            lblLogin.Visible = true;
            LinkButton1.Text = "Logout";
        }
        else
        {
            lblLogin.Visible = false;
            LinkButton1.Text = "Login";
        }
    }
}
