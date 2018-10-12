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
using System.Web;

namespace ExamHelper
{
  public partial class AskQuestion : System.Web.UI.Page
  {
    protected void Page_Init(object sender, EventArgs e)
    {
      Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
      Response.Cache.SetCacheability(HttpCacheability.NoCache);
      Response.Cache.SetNoStore();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (scriptManager.IsInAsyncPostBack)
      {
        return;
      }

      if (Session["QuizData"] == null)
      {
        Response.Redirect("Default.aspx");
      }

      QuizSessionData qd = Session["QuizData"] as QuizSessionData;

      if (qd.ExamMode)
      {
        if (!qd.Initialized)
        {
          qd.StudentData.InitializeExamMode();
          qd.Initialized = true;
        }

        StatusLabel.Text = string.Format("Exam Mode | Total Questions = {0}", qd.StudentData.Questions.Count());
      }
      else
      {
        if (qd.StudentData.IsOffsetAndCountValid(qd.QuestionOffset, qd.QuestionCount))
        {
          qd.StudentData.InitializeStudyMode(qd.QuestionCount, qd.QuestionOffset);
          qd.Initialized = true;
        }
        else
        {
          StatusLabel.Text = "Please check your question offset and count to make sure they are valid.";
          Session["QuizData"] = null;
          return;
        }

        StatusLabel.Text = string.Format("{0} out of {1} starting at {2}", qd.QuestionCount.ToString(), qd.QuizData.Questions.Count, qd.QuestionOffset);
      }

      if (qd.CurrentQuestion != null)
      {
        if (qd.CompletedQuestionList == null)
        {
          qd.CompletedQuestionList = new List<CompletedQuestion>();
        }

        qd.CompletedQuestionList.Add(new CompletedQuestion(qd.CurrentQuestion, questionPanel.GetSelectedAnswers()));
      }

      Question q = qd.StudentData.NextQuestion();

      if (q != null)
      {
        qd.CurrentQuestion = q;

        questionPanel.SetQuestionText(q.Text);
        questionPanel.SetAnswers(q.Answers);
        questionPanel.Visible = true;

        if (qd.ShowAnswersButton)
        {
          updatePanel.Visible = true;
          answerButton.Text = "Show Answer";
          answerLabel.Text = string.Empty;
        }

        StatusLabel.Text += string.Format(" | Current question: {0} | <a href=\"Default.aspx\">Start Over</a> | <a href=\"Summary.aspx\">End and View Summary</a> ", qd.StudentData.currentQuestionIndex.ToString());
      }
      else
      {
        Response.Redirect("Summary.aspx");
      }
    }

    protected void answerButton_Click(object sender, EventArgs e)
    {
      if (answerLabel.Text.Length > 0)
      {
        answerLabel.Text = string.Empty;

        answerButton.Text = "Show Answer";
      }
      else if (Session["QuizData"] != null)
      {
        QuizSessionData qd = Session["QuizData"] as QuizSessionData;

        answerLabel.Text = string.Join(" | ", qd.CurrentQuestion.Answers.Where(a => a.Correct == true).Select(a => a.Text).ToArray());

        answerButton.Text = "Hide Answer";
      }
    }
  }
}
