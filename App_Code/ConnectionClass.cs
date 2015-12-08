using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Diagnostics;

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

    public static User LoginUser(string login, string password)
    {
        //Проверяем, существует ли пользователь с этим именем
        string query = string.Format("SELECT COUNT(*) FROM CoffeeDB.dbo.users WHERE name = '{0}'", login);
        command.CommandText = query;

        try
        {
            conn.Open();
            int amountOfUsers = (int)command.ExecuteScalar();
            if (amountOfUsers == 1)
            {
                //Проверяем, совпадают ли пароли
                query = string.Format("SELECT password FROM CoffeeDB.dbo.users WHERE name = '{0}'", login);
                command.CommandText = query;
                string dbPassword = command.ExecuteScalar().ToString();

                if (dbPassword == password)
                {
                    //Получаем остальные данные пользователя из базы данных
                    query = string.Format("SELECT email, user_type FROM CoffeeDB.dbo.users WHERE name = '{0}'", login);
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
}