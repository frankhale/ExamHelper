<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExamHelper._Default"  %>

<%@ Register TagPrefix="cc1" TagName="PickTotalQuestions" Src="~/PickTotalQuestions.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Exam Helper</title>
  <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <cc1:PickTotalQuestions ID="totalQuestions" runat="server" Visible="true" />
  </div>
  </form>
</body>
</html>
