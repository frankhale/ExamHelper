/*
 * A simple testing engine used for practicing multiple choice questions
 * 
 * Frank Hale <frankhale@gmail.com>
 * 
 * Copyright (C) 2010-2013 Frank Hale
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace ExamHelper
{
	public partial class ExamSummary : System.Web.UI.UserControl
	{
		public void SetData(List<QuestionSummary> questionSummary)
		{
			Session["Summary"] = questionSummary;

			summaryRepeater.DataSource = questionSummary;
			summaryRepeater.DataBind();
		}

		protected void summaryRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			Label res = e.Item.FindControl("ResultLabel") as Label;

			if (res.Text == "Correct")
				res.CssClass = "positiveResult";
			else if (res.Text == "Wrong")
				res.CssClass = "negativeResult";
		}

		protected void downloadResults_Click(object sender, EventArgs e)
		{
			List<QuestionSummary> Summary = Session["Summary"] as List<QuestionSummary>;

			StringBuilder data = new StringBuilder();

			data.AppendLine("Num,Result,Correct Answer, Question");

			Summary.ForEach(q => data.AppendLine(String.Format("{0},{1},{2},{3}", q.ID, q.Result, q.CorrectAnswer.Trim().Replace(",", ";"), q.Text.Trim().Replace("\n", "").Replace(",", ";"))));

			Response.Clear();

			if (UseExcel.Checked)
			{
				Response.AddHeader("Content-Disposition", "attachment; filename=results.csv");
				Response.ContentType = "text/csv";
			}
			else
				Response.ContentType = "text/plain";

			Response.Write(data.ToString());
			Response.Flush();
			Response.End();
		}
	}
}