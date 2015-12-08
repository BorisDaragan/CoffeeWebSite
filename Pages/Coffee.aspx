<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Coffee.aspx.cs" Inherits="Pages_Coffee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
    Select a type<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="sds_type" DataTextField="type" DataValueField="type" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:SqlDataSource ID="sds_type" runat="server" ConnectionString="<%$ ConnectionStrings:CoffeeDBConnectionString %>" SelectCommand="SELECT DISTINCT [type] FROM [coffee] ORDER BY [type]"></asp:SqlDataSource>
</p>
<p>
    <asp:Label ID="lblOutput" runat="server" BorderStyle="None"></asp:Label>
</p>
</asp:Content>

