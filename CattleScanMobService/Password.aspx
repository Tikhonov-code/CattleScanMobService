<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Password.aspx.cs" Inherits="Password" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Width="265px"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" BackColor="#99FF33" OnClick="Button1_Click" Text="Get MD5 Code" />
            <br />
            MD5 Code</div>
        <asp:TextBox ID="TextBox2" runat="server" Width="610px"></asp:TextBox>
    </form>
</body>
</html>
