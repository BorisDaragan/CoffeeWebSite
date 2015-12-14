using System;

/// <summary>
/// Summary description for GroupedOrder
/// </summary>
public class GroupedOrder
{
	
	public string Client { get; set; }
    public DateTime Date { get; set; }
    public double Total { get; set; }

    public GroupedOrder(string client, DateTime date, double total) 
    {
        Client = client;
        Date = date;
        Total = total;
    }
	
}