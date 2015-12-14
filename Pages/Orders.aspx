<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageCMS.master" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Pages_Orders" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                Show orders between:&nbsp;<asp:TextBox runat="server" AutoPostBack="True" ID="txtOpenOrders1"></asp:TextBox>

                <ajaxToolkit:CalendarExtender runat="server" BehaviorID="_content_txtOpenOrders1_CalendarExtender" TargetControlID="txtOpenOrders1" ID="txtOpenOrders1_CalendarExtender"></ajaxToolkit:CalendarExtender>

                &nbsp;and
                <asp:TextBox runat="server" AutoPostBack="True" ID="txtOpenOrders2"></asp:TextBox>

                <ajaxToolkit:CalendarExtender runat="server" BehaviorID="_content_txtOpenOrders2_CalendarExtender" TargetControlID="txtOpenOrders2" ID="txtOpenOrders2_CalendarExtender"></ajaxToolkit:CalendarExtender>

                <br />
    <br />
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="603px" >
        <ajaxToolkit:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
            <HeaderTemplate>
                Open orders
            </HeaderTemplate>
            <ContentTemplate>
                &nbsp;<br />
                <asp:Label ID="lblOpenOrders" runat="server"></asp:Label>
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
            <HeaderTemplate>
                Closed orders
            </HeaderTemplate>
            <ContentTemplate>
                <br />
                <asp:Label ID="lblClosedOrders" runat="server"></asp:Label>
                <br />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
            <HeaderTemplate>
                Analitics
            </HeaderTemplate>
            <ContentTemplate>
                <ajaxToolkit:LineChart ID="LineChart1" runat="server" Height="317px" AreaDataLabel="" BaseLineColor="" CategoriesAxis="" CategoryAxisLineColor="" ChartTitle="" ChartTitleColor="" Theme="" TooltipBackgroundColor="" TooltipBorderColor="" TooltipFontColor="" ValueAxisLineColor="" ValueAxisLines="0">
                </ajaxToolkit:LineChart>
                <br />
                <br />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>

