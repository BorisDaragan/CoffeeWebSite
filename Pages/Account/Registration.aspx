<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Pages_Account_Registration" %>






<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td style="height: 29px">Name:</td>
            <td style="height: 29px">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Confirm Password:</td>
            <td>
                <asp:TextBox ID="txtCofirm" TextMode="Password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCofirm" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>E-mail</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" Width="114px" />
                <br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtCofirm" ControlToValidate="txtPassword" ErrorMessage="Passwords must match" ForeColor="Red"></asp:CompareValidator>
                <br />
                <asp:Label ID="lblResult" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

