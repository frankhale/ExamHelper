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

using System.Collections.Generic;

namespace ExamHelper
{
  public partial class QuestionPanel : System.Web.UI.UserControl
  {
    public void SetQuestionText(string text)
    {
      questionText.Text = text;
    }

    public void SetAnswers(List<Answer> answers)
    {
      answerList.Items.Clear();
      answers.ForEach(a => answerList.Items.Add(a.Text));
    }

    public int[] GetSelectedAnswers()
    {
      List<int> s = new List<int>();

      for (int i = 0; i < answerList.Items.Count; i++)
      {
        if (answerList.Items[i].Selected)
        {
          s.Add(i);
        }
      }

      return s.ToArray();
    }
  }
}