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
using System.Linq;
using System.Text;

namespace ExamHelper
{
	public partial class Summary : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["QuizData"] == null)
				Response.Redirect("Default.aspx");

			QuizSessionData qd = Session["QuizData"] as QuizSessionData;

			qd.CurrentQuestion = null;

			#region BUILD COMPLETED QUESTION LIST
			if (qd.CompletedQuestionList != null)
			{
				qd.Summary = new List<QuestionSummary>();

				#region ITERATE OVER SUMMARY QUESTION LIST
				for (int i = 0; i < qd.CompletedQuestionList.Count; i++)
				{
					StringBuilder ans = new StringBuilder();
					CompletedQuestion cq = qd.CompletedQuestionList[i];

					int id = qd.StudentData.Offset + i;
					string questionText = cq.Q.Text;

					foreach (Answer a in cq.Q.Answers)
					{
						string format = (a.Correct) ? String.Format("<b>{0}</b>", a.Text) : String.Format("{0}", a.Text);

						ans.Append(String.Format("{0}<br>", format));
					}

					#region QUESTION GRADING
					string providedAnswers = String.Empty;
					string correctAnswers = String.Empty;

					List<Answer> correctAnswersList = (from a in cq.Q.Answers
																						 where a.Correct == true
																						 select a).ToList();

					List<Answer> providedAnswersList = new List<Answer>();

					for (int z = 0; z < cq.A.Count(); z++)
					{
						int index = cq.A[z];

						Answer a = cq.Q.Answers[index];

						providedAnswersList.Add(new Answer(a.Text, a.Correct));
					}

					List<Answer> cResults = null;
					List<Answer> pResults = null;

					if (providedAnswersList.Count > 0)
					{
						cResults = correctAnswersList.Except(providedAnswersList, new AComparer()).ToList();
						pResults = providedAnswersList.Except(correctAnswersList, new AComparer()).ToList();

						providedAnswers = String.Join(" | ", providedAnswersList.Select(a => a.Text).ToArray());
						correctAnswers = String.Join(" | ", correctAnswersList.Select(a => a.Text).ToArray());
					}

					if ((cResults == null || cResults.Count > 0) ||
							 (pResults == null || pResults.Count > 0))
						qd.Summary.Add(new QuestionSummary(questionText, "Wrong", ans.ToString(), correctAnswers, providedAnswers, id));
					else
						qd.Summary.Add(new QuestionSummary(questionText, "Correct", ans.ToString(), correctAnswers, providedAnswers, id));
					#endregion
				}
				#endregion
			}
			#endregion

			if (qd.Summary != null)
			{
				var _summaryList = (from s in qd.Summary
														orderby s.Result descending
														select s).ToList();

				examSummary.SetData(_summaryList);
				examSummary.Visible = true;
			}
			else
			{
				StatusLabel.Visible = true;
				StatusLabel.Text = "You have not completed any questions.";
			}
		}
	}
}
