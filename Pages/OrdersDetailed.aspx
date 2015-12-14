<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageCMS.master" AutoEventWireup="true" CodeFile="OrdersDetailed.aspx.cs" Inherits="Pages_OrdersDetailed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblTitle" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="sds_ordersDetailed" ForeColor="Black" GridLines="Vertical" style="margin-top: 0px" Width="628px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="product" HeaderText="product" SortExpression="product" />
            <asp:BoundField DataField="amount" HeaderText="amount" ReadOnly="True" SortExpression="amount" />
            <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
            <asp:CheckBoxField DataField="orderShipped" HeaderText="orderShipped" SortExpression="orderShipped" />
            <asp:BoundField DataField="total" HeaderText="total" ReadOnly="True" SortExpression="total" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
    <asp:SqlDataSource ID="sds_ordersDetailed" runat="server" ConnectionString="<%$ ConnectionStrings:CoffeeDBConnectionString %>" SelectCommand="SELECT
 product,
 SUM(amount) AS amount,
 price,
 orderShipped,
 SUM(amount * price) AS total
FROM
 orders
WHERE
 client = @client
AND
 date = @date
GROUP BY
 product, price, orderShipped">
        <SelectParameters>
            <asp:QueryStringParameter Name="client" QueryStringField="client" />
            <asp:QueryStringParameter DefaultValue="" Name="date" QueryStringField="date" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="btnShip" runat="server" Text="Ship Order" OnClick="btnShip_Click" />
</asp:Content>

