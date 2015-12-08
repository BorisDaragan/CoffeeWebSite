using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Account_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //Создаем нового пользователя
        User user = new User(txtName.Text, txtPassword.Text, txtEmail.Text, "user");

        //Регистрируем пользователя
        lblResult.Text = ConnectionClass.RegisterUser(user);
    }
}