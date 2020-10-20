<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPost.aspx.cs" Inherits="TestPost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Button ID="Button1" runat="server" Height="67px" OnClick="Button1_Click" Text="Post Login" Width="350px" />
        <asp:Button ID="Button2" runat="server" BorderColor="#CCFF99" Height="66px" OnClick="Button2_Click" Text="Get Farm stat" Width="207px" />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="321px" TextMode="MultiLine" Width="433px"></asp:TextBox>
    </form>
</body>
</html>
