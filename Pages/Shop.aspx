<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="Pages_Shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblResult" runat="server" Visible="False"></asp:Label>
    <br />
    <asp:Button ID="btnOk" runat="server" Text="Ok" Width="100px" OnClick="btnOk_Click" Visible="False" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="False" Width="100px" OnClick="btnCancel_Click" />
    <br />
    <asp:Button ID="btnOrder" runat="server" Text="Order!" OnClick="btnOrder_Click" />
    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <br />
    <asp:Panel ID="pnlProducts" runat="server">
    </asp:Panel>
    <br />
</asp:Content>

