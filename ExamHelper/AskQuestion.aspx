<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskQuestion.aspx.cs" Inherits="ExamHelper.AskQuestion" %>

<%@ Register TagPrefix="cc1" TagName="QuestionPanel" Src="~/QuestionPanel.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exam Questions</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="msg" runat="server" Text=""></asp:Label>

            <asp:Label ID="StatusLabel" runat="server" Text=""></asp:Label>
            <cc1:QuestionPanel ID="questionPanel" runat="server" Visible="false" />
            <br />
            <br />
            <asp:ScriptManager ID="scriptManager" runat="server" />
            <asp:UpdatePanel ID="updatePanel" runat="server" Visible="false">
                <ContentTemplate>
                    <asp:Button ID="answerButton" runat="server" Text="See Answer" OnClick="answerButton_Click" />
                    <br />
                    <br />
                    <asp:Label ID="answerLabel" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
