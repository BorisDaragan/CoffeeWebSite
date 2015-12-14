using System;
using System.Globalization;
using System.Text;
using System.Collections;
using System.Data;

public partial class Pages_Orders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AuthenticateAdministrator();
        if (txtOpenOrders1.Text != "" && txtOpenOrders2.Text != "")
        {
            GenerateOrders(txtOpenOrders1.Text, txtOpenOrders2.Text, false);
            GenerateOrders(txtOpenOrders1.Text, txtOpenOrders2.Text, true);
        }
           
            
        GenerateLineChart("amount", "client", "Orders per customer", LineChart1);
    }

    private void GenerateOrders(string beginDate, string endDate, bool shipped)
    {
        DateTimeFormatInfo info = new CultureInfo("en-us", false).DateTimeFormat;
        StringBuilder sb = new StringBuilder();

        //Get Dates and convert to United States Format(en-US)
        DateTime date1 = Convert.ToDateTime(beginDate, info);
        DateTime date2 = Convert.ToDateTime(endDate, info);
        DateTime incrementalDate = date1;

        //Get grouped orders
        while (incrementalDate <= date2)
        {
            sb.Append(string.Format("Orders for {0}: ", info.GetMonthName(incrementalDate.Month)));
            ArrayList orderList = ConnectionClass.GetGroupedOrders(incrementalDate, date2, shipped);           
            if (orderList.Count == 0) {
                //No orders for current month
                sb.Append("No orders for this month <br /><br />");
            }
            else 
            {
                sb.Append(@"<table class='orderTable'>
                            <tr><th>Date</th> <th>Client</th> <th>Total</th></tr>");

                foreach (GroupedOrder groupedOrder in orderList)
                {
                    sb.Append(string.Format("<tr> <td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td> </tr>", 
                        groupedOrder.Date, groupedOrder.Client, groupedOrder.Total,
                        string.Format("<a href='ordersDetailed.aspx?client={0}&date={1}'>View Details</a>", groupedOrder.Client, groupedOrder.Date)));
                }

                sb.Append("</table>");
            }

            //Increment Date to next month and set first day of new month to 1
            incrementalDate = incrementalDate.AddMonths(1);
            incrementalDate = new DateTime(incrementalDate.Year, incrementalDate.Month, 1);
        }

        if (shipped == false)
            lblOpenOrders.Text = sb.ToString();
        else
        {
            lblClosedOrders.Text = sb.ToString();
        }

    }

    private void GenerateLineChart(string sumObject, string groupByObject, string title, AjaxControlToolkit.LineChart chart)
    {
        string query = string.Format("SELECT SUM({0}), {1} FROM orders GROUP BY {1}", sumObject, groupByObject);

        DataTable dt = ConnectionClass.GetChartData(query);

        decimal [] x = new decimal[dt.Rows.Count];
        string [] y = new string[dt.Rows.Count];

        for(int i = 0; i < dt.Rows.Count; i++)
        {
            x[i] = Convert.ToInt32(dt.Rows[i][0].ToString());
            y[i] = dt.Rows[i][1].ToString();
        }

        chart.Series.Add(new AjaxControlToolkit.LineChartSeries { Data = x });
        chart.CategoriesAxis = string.Join(",", y);
        chart.ChartTitle = title;

        if (x.Length > 3)
            chart.ChartWidth = (x.Length * 75).ToString();

    }

    private void AuthenticateAdministrator()
    {
        if ((string)Session["type"] != "administrator")
            Response.Redirect("~/Pages/Account/Login.aspx");
    }
}