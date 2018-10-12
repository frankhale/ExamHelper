<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExamSummary.ascx.cs"
    Inherits="ExamHelper.ExamSummary" %>
<link id="Link1" href="css/style.css" rel="stylesheet" type="text/css" runat="server"
    visible="false" />
<div id="examSummary">
    <a href="Default.aspx">Start Over</a> |
  <asp:LinkButton ID="downloadResults" runat="server" OnClick="downloadResults_Click">Download Results</asp:LinkButton>
    <asp:CheckBox ID="UseExcel" runat="server" Text="Open Excel" />
    <hr />
    <asp:Repeater ID="summaryRepeater" runat="server" OnItemDataBound="summaryRepeater_ItemDataBound">
        <ItemTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <%# Eval("ID") %>:
            <%# Eval("Text") %>
                    </td>
                    <td rowspan="3" width="5%">
                        <asp:Label ID="ResultLabel" runat="server" Text='<%#Eval("Result")%>'>
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <i>
                            <%# Eval("Answers") %></i>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="providedAnswers">You answered:<i>
                            <%# Eval("ProvidedAnswer")%></i> </span>
                    </td>
                </tr>
            </table>
            <br />
        </ItemTemplate>
    </asp:Repeater>
</div>
