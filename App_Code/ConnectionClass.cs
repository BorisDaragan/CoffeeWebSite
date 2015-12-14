using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ConnectionClass
/// </summary>
public static class ConnectionClass
{
    private static SqlConnection conn;
    private static SqlCommand command;
    static ConnectionClass()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["coffeeConnection"].ToString();
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("", conn);
    }

    //Coffee methods

    public static ArrayList GetCoffeeByType(string coffeeType)
    {
        ArrayList list = new ArrayList();
        string query = string.Format("SELECT * FROM coffee WHERE type LIKE '{0}'", coffeeType);
        try
        {
            conn.Open();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string type = reader.GetString(2);
                double price = reader.GetDouble(3);
                string roast = reader.GetString(4);
                string country = reader.GetString(5);
                string image = reader.GetString(6);
                string review = reader.GetString(7);

                Coffee coffee = new Coffee(id, name, type, price, roast, country, image, review);
                list.Add(coffee);
            }
        }
        finally
        {
            conn.Close();
        }
        return list;
    }

    public static Coffee GetCoffeeById(int id)
    {
        string query = String.Format("SELECT * FROM coffee WHERE id = '{0}'", id);
        Coffee coffee = null;

        try
        {
            conn.Open();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString(1);
                string type = reader.GetString(2);
                double price = reader.GetDouble(3);
                string roast = reader.GetString(4);
                string country = reader.GetString(5);
                string image = reader.GetString(6);
                string review = reader.GetString(7);

                coffee = new Coffee(name, type, price, roast, country, image, review);

            }
        }
        finally
        {
            conn.Close();
        }

        return coffee;
    }

    public static void AddCoffee(Coffee coffee)
    {
        string query = string.Format(
             @"INSERT INTO coffee VALUES ('{0}', '{1}', @prices, '{2}', '{3}','{4}', '{5}')",
             coffee.Name, coffee.Type, coffee.Roast, coffee.Country, coffee.Image, coffee.Review);
       command.CommandText = query;
        command.Parameters.Add(new SqlParameter("@prices", coffee.Price));
        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            command.Parameters.Clear();
        }
    }

    //User methods

    public static User LoginUser(string login, string password)
    {
        //Проверяем, существует ли пользователь с этим именем
        string query = string.Format("SELECT COUNT(*) FROM users WHERE name = '{0}'", login);
        command.CommandText = query;

        try
        {
            conn.Open();
            int amountOfUsers = (int)command.ExecuteScalar();
            if (amountOfUsers == 1)
            {
                //Проверяем, совпадают ли пароли
                query = string.Format("SELECT password FROM users WHERE name = '{0}'", login);
                command.CommandText = query;
                string dbPassword = command.ExecuteScalar().ToString();

                if (dbPassword == password)
                {
                    //Получаем остальные данные пользователя из базы данных
                    query = string.Format("SELECT email, user_type FROM users WHERE name = '{0}'", login);
                    command.CommandText = query;

                    SqlDataReader reader = command.ExecuteReader();
                    User user = null;

                    while (reader.Read())
                    {
                        string email = reader.GetString(0);
                        string type = reader.GetString(1);

                        user = new User(login, password, email, type);
                    }
                    return user;
                }
                else
                {
                    //Пароли не совпадают
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        finally
        {
            conn.Close();
        }
    }

    public static string RegisterUser(User user) 
    {
        string query = string.Format("SELECT COUNT(*) FROM users WHERE name = '{0}'", user.Login);
        command.CommandText = query;

        try
        {
            conn.Open();
            int amountOfUsers = (int)command.ExecuteScalar();

            if (amountOfUsers < 1)
            {
                //Пользователь не существует, создаем нового пользователя
                query = string.Format("INSERT INTO users VALUES ('{0}', '{1}', '{2}', '{3}')",
                    user.Login, user.Password, user.Email, user.Type);
                command.CommandText = query;
                command.ExecuteNonQuery();
                return "User registered!";
            }
            else
            {
                //Пользователь существует
                return "A user with this name already exists";
            }
        }
        finally
        {
            conn.Close();
        }
    }

    public static User GetUserDetails(string userName)
    {
        string query = string.Format("SELECT * FROM users WHERE name = '{0}'", userName);
        command.CommandText = query;
        User user = null;

        try
        {
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string password = reader.GetString(2);
                string email = reader.GetString(3);
                string userType = reader.GetString(4);

                user = new User(id, name, password, email, userType);
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            conn.Close();
        }

        return user;
    }

    //Order methods

    public static void AddOrders(ArrayList orders)
    {
        try
        {
            command.CommandText = "INSERT INTO orders VALUES (@client, @product, @amount, @price, @date, @orderSent)";
            conn.Open();

            //Обновляем значения для каждой переменной заказа
            foreach (Order order in orders)
            {
                command.Parameters.Add(new SqlParameter("@client", order.Client));
                command.Parameters.Add(new SqlParameter("@product", order.Product));
                command.Parameters.Add(new SqlParameter("@amount", order.Amount));
                command.Parameters.Add(new SqlParameter("@price", order.Price));
                command.Parameters.Add(new SqlParameter("@date", order.Date));
                command.Parameters.Add(new SqlParameter("@orderSent", order.Ordershipped));

                //Выполняем запрос и очищаем параметры
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }
        finally
        {
            conn.Close();
        }
    }

    public static ArrayList GetGroupedOrders(DateTime currentDate, DateTime endDate, Boolean shipped)
    {
        string query = @"SELECT client, date, SUM(total) as total
                                FROM (
	                                    SELECT client, date, (amount * price) AS total
	                                    FROM orders
	                                    WHERE date >= @date1
	                                    AND date <= @date2
                                        AND orderShipped = @shipped
                                )as result
                                GROUP BY client, date";

        ArrayList orderList = new ArrayList();
        int lastDay;

        //Check if current dat.month == enddate.month
        if (currentDate.Month == endDate.Month && currentDate.Year == endDate.Year)
        {
            //Yes, last to be displayed after this ine. Last day = Last day of the month
            lastDay = endDate.Day;
        }
        else
        {
            //No, Other months will be displayed after this one. Last day = Last day of the month
            lastDay = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
        }
        DateTime date2 = new DateTime(currentDate.Year, currentDate.Month, lastDay);

        try
        {
            conn.Open();
            command.CommandText = query;

            command.Parameters.Add(new SqlParameter("@date1", currentDate));
            command.Parameters.Add(new SqlParameter("@date2", date2));
            command.Parameters.Add(new SqlParameter("@shipped", shipped));
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string client = reader.GetString(0);
                DateTime date = reader.GetDateTime(1);
                double total = reader.GetDouble(2);

                GroupedOrder groupedOrder = new GroupedOrder(client, date, total);
                orderList.Add(groupedOrder);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            conn.Close();
            command.Parameters.Clear();
        }

        return orderList;
    }
    
    public static ArrayList GetDatailedOrders (string client, DateTime date)
    {
        string query = @"SELECT id, product, amount, price, orderShipped
                        FROM orders
                        WHERE client = @client AND date = @date";
        command.CommandText = query;
        ArrayList orderList = new ArrayList();

        try
        {
            conn.Open();
            command.Parameters.Add(new SqlParameter("@client", client));
            command.Parameters.Add(new SqlParameter("@date", date));
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string product = reader.GetString(1);
                int amount = reader.GetInt32(2);
                double price = reader.GetDouble(3);
                bool orderShipped = reader.GetBoolean(4);

                Order order = new Order(id, client, product, amount, price, date, orderShipped);               
                orderList.Add(order);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            conn.Close();
            command.Parameters.Clear();
        }
        return orderList;
    }

    public static void UpdateOrders(string client, DateTime date)
    {
        string query = @"UPDATE orders
                        SET ordersShipped = 1
                        WHERE client = @client AND date = @date";
        command.CommandText = query;

        try
        {
            conn.Open();
            command.Parameters.Add(new SqlParameter("@client", client));
            command.Parameters.Add(new SqlParameter("@date", date));
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        { }
        finally
        {
            conn.Close();
            command.Parameters.Clear();
        }
    }

    public static DataTable GetChartData(string query)
    {
        command.CommandText = query;
        DataTable dt = new DataTable();

        try
        {
            conn.Open();

            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                sda.SelectCommand = command;
                sda.Fill(dt);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            conn.Close();
        }

        return dt;
    }
}