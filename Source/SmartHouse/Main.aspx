<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    Select a device form the list below to view its status.<br />
    In order to change its status simply change the device status radio button.<br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Devices in my house:"></asp:Label>
&nbsp;
    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="smarthousedb" DataTextField="devname" DataValueField="deviceid" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>
    <asp:SqlDataSource ID="smarthousedb" runat="server" ConnectionString="<%$ ConnectionStrings:smarthousedbConnectionString %>" SelectCommand="getDevicenameByHouseidProc" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="houseid" SessionField="houseid" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="Label2" runat="server" Text="Device status:"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:RadioButtonList ID="RblStatus" runat="server" RepeatDirection="Horizontal" Width="133px" OnSelectedIndexChanged="RblStatus_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>On</asp:ListItem>
            <asp:ListItem>Off</asp:ListItem>
        </asp:RadioButtonList>
    </asp:Panel>
    <br />
    <br />
</asp:Content>

