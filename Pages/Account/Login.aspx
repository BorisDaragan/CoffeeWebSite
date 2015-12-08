<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Account_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td><b>Login: </b></td>
            <td><asp:TextBox ID="txtLogin" runat="server"></asp:TextBox><b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogin" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </b></td>
        </tr>
        <tr>
            <td><b>Password: </b></td>
            <td><asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox><b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </b></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                <br />
                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" PostBackUrl="~/Pages/Account/Registration.aspx">Register</asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>

