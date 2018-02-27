<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="TAMegaChallengeCasino._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="oneImg" runat="server" />
            <asp:Image ID="twoImg" runat="server" />
            <asp:Image ID="threeImg" runat="server" />
            <br />
            <br />
            You&nbsp; bet:&nbsp;
            <asp:TextBox ID="betBox" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="leverButton" runat="server" OnClick="leverButton_Click" Text="Pull the lever!" />
            <br />
            <asp:Label ID="resultLabel" runat="server"></asp:Label>
            <br />
            <asp:Label ID="statusLabel" runat="server"></asp:Label>
            <br />
            <br />
            1 cherry - x2 your bet<br />
            2 cherries - x3 your bet<br />
            3 cherries - x 4 your bet<br />
            3 7&#39;s - Jackpot: x100 your bet<br />
            <br />
            HOWEVER ... If there is even 1 BAR, you lose your bet!</div>
    </form>
</body>
</html>
