<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionPanel.ascx.cs"
  Inherits="ExamHelper.QuestionPanel" %>

<link id="Link1" href="css/style.css" rel="stylesheet" type="text/css" runat="server" visible="false" />

<div id="questionPanel">
  <hr />
  <asp:Label ID="questionText" runat="server" CssClass="question" Text=""></asp:Label>
  <br />
  <asp:CheckBoxList ID="answerList" runat="server">
  </asp:CheckBoxList>
  <asp:Button ID="submit" runat="server" Text="Submit" />
  
</div>
