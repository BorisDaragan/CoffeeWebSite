using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text;

public partial class Pages_Shop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateControls();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Authenticate();
        SendOrder();

        lblResult.Text = "Your order has been placed, thank you for shopping at our store";
        btnOk.Visible = false;
        btnCancel.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["orders"] = null;
        btnOk.Visible = false;
        btnCancel.Visible = false;
        lblResult.Visible = false;
    }
    protected void btnOrder_Click(object sender, EventArgs e)
    {
        Authenticate();
        GenerateReview();
    }

    //Заполняем страницу динамическими элементами(controls), показывая товары из базы данных
    private void GenerateControls()
    {
        //Get all coffeeObjects from database
        ArrayList coffeeList = ConnectionClass.GetCoffeeByType("%");

        foreach (Coffee coffee in coffeeList) {
            //Создаем controls
            Panel coffeePanel = new Panel();
            Image image = new Image {ImageUrl = coffee.Image, CssClass = "ProductsImage"};
            Literal literal = new Literal() { Text = "<br />" };
            Literal literal2 = new Literal() { Text = "<br />" };
            Label lblName = new Label { Text = coffee.Name, CssClass = "ProductsName" };
            Label lblPrice = new Label {
                                    Text = String.Format("{0:0.00}", coffee.Price + "<br />"),
                                    CssClass = "ProductsPrice"
                                };
            TextBox textBox = new TextBox
                            {
                                ID = coffee.Id.ToString(),
                                CssClass = "ProductsTextBox",
                                Width = 60,
                                Text = "0"
                            };
            //Добавляем валидацию только для чисел
            RegularExpressionValidator regex = new RegularExpressionValidator
                                                    {
                                                        ValidationExpression = "^[0-9]*",
                                                        ControlToValidate = textBox.ID,
                                                        ErrorMessage = "Please enter a number."
                                                    };

            //Помещаем controls на Panel
            coffeePanel.Controls.Add(image);
            coffeePanel.Controls.Add(literal);
            coffeePanel.Controls.Add(lblName);
            coffeePanel.Controls.Add(literal2);
            coffeePanel.Controls.Add(lblPrice);
            coffeePanel.Controls.Add(textBox);
            coffeePanel.Controls.Add(regex);

            pnlProducts.Controls.Add(coffeePanel);

        }

    }

    //Возвращает список всех заказов в textboxes
    private ArrayList GetOrders()
    {
        //Получаем список Texbox объектов в ContentPlaceHolder
        ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        ControlFinder<TextBox> cf = new ControlFinder<TextBox>();
        cf.FindChildControlsRecursive(cph);
        var textBoxList = cf.FoundControls;

        //Создаем заказы, используя данные из textbox
        ArrayList orderList = new ArrayList();

        foreach (TextBox textBox in textBoxList)
        {
            //Проверяем, есть ли текст в textbox
            if (textBox.Text != "")
            {
                int amountOfOrders = Convert.ToInt32(textBox.Text);
                //Создаем заказ для каждой textbox, которая > 0
                if (amountOfOrders > 0)
                {
                    Coffee coffee = ConnectionClass.GetCoffeeById(Convert.ToInt32(textBox.ID));
                    Order order = new Order(Session["login"].ToString(), coffee.Name, amountOfOrders, coffee.Price, DateTime.Now, false);
                    //Добавляем заказ в orderList
                    orderList.Add(order);
                }
            }
        }
        return orderList;
    }

    //Создаем HTML table для просмотра текущего заказа
    private void GenerateReview()
    {
        double totalAmount = 0;
        ArrayList orderList = GetOrders();
        Session["orders"] = orderList;

        StringBuilder sb = new StringBuilder();
        sb.Append("<table>");
        sb.Append("<h3>Please review your order</h3>");

        //Создаем row для каждого заказа
        foreach (Order order in orderList)
        {
            
            double totalRow = order.Price * order.Amount;
            sb.Append(String.Format(@"<tr>
                                            <td width = '50px'>{0} X </td>
                                            <td width = '200px'>{1} ({2}) </td>
                                            <td>{3}</td><td>$</td>
                                    </tr>", order.Amount, order.Product, order.Price, String.Format("{0:0.00}", totalRow)));
            totalAmount = totalAmount + totalRow;
        }

        //Создаем row для Total Amount
        sb.Append(String.Format(@"<tr>
                                    <td><b>Total: </b></td>
                                    <td><b>{0} $</b></td>
                                </tr>", totalAmount));
        sb.Append("</table>");

        //Делаем Controls видимыми
        lblResult.Text = sb.ToString();
        lblResult.Visible = true;
        btnOk.Visible = true;
        btnCancel.Visible = true;

    }

    private void SendOrder()
    {
        ArrayList orderList = (ArrayList)Session["Orders"];
        ConnectionClass.AddOrders(orderList);
        Session["orders"] = null;
    }

    private void Authenticate()
    {
        if (Session["login"] == null)
        {
            Response.Redirect("~/Pages/Account/Login.aspx");
        }
    }
}