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
using System.Web.Services;

namespace ExamHelper
{
	public partial class PickTotalQuestions : System.Web.UI.UserControl
	{
		public delegate void SubmitActionDelegate(object sender, EventArgs e);
		public delegate void CheckBoxSelectionChangedActionDelegate(object sender, EventArgs e);

		public event SubmitActionDelegate SubmitAction;
		public event CheckBoxSelectionChangedActionDelegate CheckBoxSelectionChangedAction;

		public bool ShowAnswerButton { get { return displayShowAnswerButton.Checked; } }

		public void SetStatusMessages(string msg1, string msg2)
		{
			infoLabel1.Text = msg1;
			infoLabel2.Text = msg2;
		}

		public bool ExamMode { get { return examModeCheckBox.Checked; } }

		public int QuestionCount
		{
			get
			{
				int count = 0;

				Int32.TryParse(questionCount.Text, out count);

				return count;
			}
		}

		public int QuestionOffset
		{
			get
			{
				int offset = 0;

				Int32.TryParse(questionOffset.Text, out offset);

				return offset;
			}
		}

		protected void submit_Click(object sender, EventArgs e)
		{
			SubmitAction(sender, e);
		}

		protected void examModeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxSelectionChangedAction(sender, e);
		}
	}
}