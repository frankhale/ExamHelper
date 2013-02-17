<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PickTotalQuestions.ascx.cs"
  Inherits="ExamHelper.PickTotalQuestions" %>
<link id="Link1" href="css/style.css" rel="stylesheet" type="text/css" runat="server"
  visible="false" />
<div id="totalQuestionPanel">
  <table border="0" width="100%">
    <tr>
      <td width="15%" align="right">
        <asp:Label ID="Label1" runat="server" Text="Number of Questions:"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="questionCount" runat="server"></asp:TextBox>
        <asp:Label ID="infoLabel1" runat="server" Text=""></asp:Label>
      </td>
    </tr>
    <tr>
      <td width="15%" align="right">
        <asp:Label ID="Label2" runat="server" Text="Question offset:"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="questionOffset" runat="server">
        </asp:TextBox>
        <asp:Label ID="infoLabel2" runat="server" Text=""></asp:Label>
      </td>
    </tr>
    <tr>
      <td align="right">
        <asp:Button ID="submit" runat="server" Text="Submit" onclick="submit_Click" />
      </td>
      <td>
        <asp:CheckBox ID="displayShowAnswerButton" runat="server" Text="Display show answer button for questions" />
        <br />
        <asp:CheckBox ID="examModeCheckBox" runat="server" Text="Exam Mode" 
          oncheckedchanged="examModeCheckBox_CheckedChanged" AutoPostBack="true" />
      </td>
    </tr>
  </table>
  <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
  
  <asp:RequiredFieldValidator ID="numberOfQuestionsRFV" runat="server" ErrorMessage="Number of questions is required."
    ControlToValidate="questionCount" Display="None"></asp:RequiredFieldValidator>
  <asp:RequiredFieldValidator ID="questionOffsetRFV" runat="server" ErrorMessage="Question offset is required."
    ControlToValidate="questionOffset" Display="None"></asp:RequiredFieldValidator>
  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="(\d+)"
    ErrorMessage="Number of questions must be a number." ControlToValidate="questionCount"
    Display="None"></asp:RegularExpressionValidator>
  <asp:RegularExpressionValidator ID="questionOffsetREV" runat="server" ValidationExpression="(\d+)"
    ErrorMessage="Question offset must be a number." ControlToValidate="questionOffset"
    Display="None"></asp:RegularExpressionValidator>
</div>
