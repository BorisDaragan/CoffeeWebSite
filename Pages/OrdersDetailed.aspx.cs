using System;
using System.Collections;
using System.Net.Mail;
using System.Text;

public partial class Pages_OrdersDetailed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTitle.Text = string.Format("<h2>Client: {0} <br/>Date: {1} </h2>",
                            Request.QueryString["client"], Request.QueryString["date"]);
    }
    protected void btnShip_Click(object sender, EventArgs e)
    {
        //Get variables from Url
        string client = Request.QueryString["client"];
        DateTime date = Convert.ToDateTime(Request.QueryString["date"]);

        //Get user info + user's placed orders
        User user = ConnectionClass.GetUserDetails(client);
        ArrayList orderList = ConnectionClass.GetDatailedOrders(client, date);

        //Update database and send confirmation e-mail
        //Afterwards send user to 'Orders' Page
        ConnectionClass.UpdateOrders(client, date);
        SendEmail(user.Login,  user.Email, orderList);
        Response.Redirect("~/Pages/Orders.aspx");
    }

   private void SendEmail(string client, string eMail, ArrayList orderList)
    {
        //Set up sender and receiver e-mail adresses
        MailAddress to = new MailAddress(eMail);
        MailAddress from = new MailAddress("boris.daragan@gmail.com");

        //Set up e-mail body
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat(@"
Dear {0}.
<br/>
We are happy to announce your order placed on {1} has been completed.
<br/>
Your ordered products:<br/>", client, Request.QueryString["date"]);
        double total = 0;

        foreach (Order order in orderList)
        {
            sb.AppendFormat(@"
- {0} ({1} $)  X  {2}  =   {3} $ <br/>",
                                    order.Product, order.Price, order.Amount, (order.Amount * order.Price));

            total += (order.Amount * order.Price);
        }

        sb.Append(@"
Total amount = " + total + " $");

        sb.Append(@"<br/>
You can come collect your order at your earliest convenience.
<br/>
Kind regards");
       

        MailMessage mail = new MailMessage();
        SmtpClient sc = new SmtpClient();
        try
        {
            mail.From = new MailAddress("your@gmail.com");
            mail.To.Add(eMail);
            mail.Subject = "Your order has been completed";
            mail.IsBodyHtml = true;
            mail.Body = sb.ToString();
            sc.Host = "smtp.gmail.com";
            sc.Port = 587;
            sc.Credentials = new System.Net.NetworkCredential("your@gmail.com", "Your password");

            sc.EnableSsl = true;
            sc.Send(mail);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}