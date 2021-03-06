﻿using System;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Проверяем, зашел ли пользователь
        if (Session["login"] != null)
        {
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Пользователь заходит
        if (LinkButton1.Text == "Login")
        {
            Response.Redirect("~/Pages/Account/Login.aspx");
        }
        else
        {
            //Пользователь выходит
            Session.Clear();
            Response.Redirect("~/Pages/Home.aspx");
        }
    }
}
