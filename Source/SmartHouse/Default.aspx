<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" Height="136px" ImageUrl="~/App_Data/SHLogo_width.jpg" Width="797px"/>        
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="House ID :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbHouse" runat="server" TextMode="Number"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbHouse" Display="Dynamic" ErrorMessage="RequiredField" ForeColor="Red">Required field</asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="User Name :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbUser" Display="Dynamic" ErrorMessage="RequiredField" ForeColor="Red">Required field</asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Password : "></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbPass" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPass" Display="Dynamic" ErrorMessage="RequiredField" ForeColor="Red">Required field</asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="lblErr" runat="server" Text="Login Error: house id or/and user name or/and password are invalid" Visible="False"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button ID="BtnLog" runat="server" Text="Login" OnClick="BtnLog_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnReg" runat="server" Text="Register" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnPass" runat="server" Text="Forgot password" />
        <br />
    
    </div>
    </form>
</body>
</html>
