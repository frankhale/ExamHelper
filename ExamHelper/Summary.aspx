<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="ExamHelper.Summary" %>

<%@ Register TagPrefix="cc1" TagName="ExamSummary" Src="~/ExamSummary.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Exam Summary</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Label ID="StatusLabel" runat="server" Text="" Visible="false"></asp:Label>
    <cc1:ExamSummary ID="examSummary" runat="server" Visible="false" />
    </div>
    </form>
</body>
</html>
