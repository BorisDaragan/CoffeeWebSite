<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageCMS.master" AutoEventWireup="true" CodeFile="Coffee_Add.aspx.cs" Inherits="Pages_Coffee_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Add new Coffee</h3>

    <table cellspacing="15" class="coffeeTable">
        <tr>
            <td style="width: 80px">

                Name:</td>
            <td>

                <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        
        <tr>
            <td style="width: 80px">

                Type:</td>
            <td>

                <asp:TextBox ID="txtType" runat="server" Width="300px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtType" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        
        <tr>
            <td style="width: 80px">

                Price:</td>
            <td>

                <asp:TextBox ID="txtPrice" runat="server" Width="300px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        
        <tr>
            <td style="width: 80px">

                Roast:</td>
            <td>

                <asp:TextBox ID="txtRoast" runat="server" Width="300px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRoast" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        
        <tr>
            <td style="width: 80px">

                Country:</td>
            <td>

                <asp:TextBox ID="txtCountry" runat="server" Width="300px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCountry" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        
        <tr>
            <td style="width: 80px">

                Image:</td>
            <td>

                <asp:DropDownList ID="ddlImage" runat="server" Width="300px">
                </asp:DropDownList>
                <br />
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnUploadImage" runat="server" Text="Upload Image" OnClick="btnUploadImage_Click" CausesValidation="False" />
            </td>
        </tr>
        
        <tr>
            <td style="width: 80px">

                Review:</td>
            <td>

                <asp:TextBox ID="txtReview" runat="server" Height="83px" TextMode="MultiLine" Width="348px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
    </table>
    <asp:Label ID="lblResult" runat="server"></asp:Label>
    <br />
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
</asp:Content>

