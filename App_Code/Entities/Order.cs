using System;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    private string p1;
    private string p2;
    private int amountOfOrders;
    private double p3;
    private DateTime dateTime;
    private bool p4;

    public int Id { get; set; }
    public string Client { get; set; }
    public string Product { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public bool Ordershipped { get; set; }

	public Order(int id, string client, string product, int amount, double price, DateTime date, bool orderShipped)
	{
        Id = id;
        Client = client;
        Product = product;
        Amount = amount;
        Price = price;
        Date = date;
        Ordershipped = orderShipped;
	}

    public Order(string client, string product, int amount, double price, DateTime date, bool orderShipped)
    {
        Client = client;
        Product = product;
        Amount = amount;
        Price = price;
        Date = date;
        Ordershipped = orderShipped;
    }

}