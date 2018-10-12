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

namespace ExamHelper
{
  public partial class _Default : System.Web.UI.Page
  {
    protected override void OnInit(EventArgs e)
    {
      totalQuestions.SubmitAction += new PickTotalQuestions.SubmitActionDelegate(totalQuestions_SubmitAction);
      totalQuestions.CheckBoxSelectionChangedAction += new PickTotalQuestions.CheckBoxSelectionChangedActionDelegate(totalQuestions_ExamMode);
    }

    void totalQuestions_ExamMode(object sender, EventArgs e)
    {
      QuizSessionData qd = CreateNewQuizSession();

      qd.ExamMode = totalQuestions.ExamMode;

      if (qd.ExamMode)
      {
        Response.Redirect("AskQuestion.aspx");
      }
      else
      {
        Response.Redirect("Default.aspx");
      }
    }

    void totalQuestions_SubmitAction(object sender, EventArgs e)
    {
      Response.Redirect("AskQuestion.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (totalQuestions.ExamMode)
      {
        return;
      }

      if (!IsPostBack)
      {
        QuizSessionData qd = CreateNewQuizSession();

        totalQuestions.SetStatusMessages(string.Format("out of {0}", qd.QuizData.Questions.Count), "starting point within the question list");
      }
      else
      {
        QuizSessionData qd = Session["QuizData"] as QuizSessionData;

        if (qd == null)
        {
          qd = CreateNewQuizSession();
        }

        qd.QuestionCount = totalQuestions.QuestionCount;
        qd.QuestionOffset = totalQuestions.QuestionOffset;
        qd.ShowAnswersButton = totalQuestions.ShowAnswerButton;
      }
    }

    protected QuizSessionData CreateNewQuizSession()
    {
      QuizSessionData qd = new QuizSessionData();
      qd.QuizData = new Quiz(Server.MapPath(@"App_Data\questions.xml"));
      qd.StudentData = new Student(qd.QuizData);

      Session["QuizData"] = qd;

      return qd;
    }
  }
}

