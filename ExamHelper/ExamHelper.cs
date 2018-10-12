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
using System.Xml.Linq;

namespace ExamHelper
{
  #region Question and Answer
  public class AComparer : IEqualityComparer<Answer>
  {
    bool IEqualityComparer<Answer>.Equals(Answer x, Answer y)
    {
      if (x.Text == y.Text)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    int IEqualityComparer<Answer>.GetHashCode(Answer obj)
    {
      // This is basically retarded but I want it to do a comparison using Equals and not the hash code.
      return -1;
    }
  }

  public class Answer
  {
    public string Text { get; private set; }
    public bool Correct { get; private set; }

    public Answer(string text, bool correct)
    {
      Text = text;
      Correct = correct;
    }
  }

  public class Question
  {
    public string Text { get; private set; }
    public List<Answer> Answers { get; private set; }
    public int[] StudentAnswer { get; set; } // array of indices into the list of answers

    public Question(string text, List<Answer> ans)
    {
      Text = HTMLize(text);
      Answers = ans;
      StudentAnswer = null;

      //RandomizeAnswers();
    }

    // This is just a quick function to illustrate how to 
    // allow for some special tags in the question so it can 
    // be replaced with HTML.
    private string HTMLize(string str)
    {
      str = str.Replace("[br]", "<br />");

      return str;
    }

    //private void RandomizeAnswers()
    //{
    //  List<Answer> randomized = new List<Answer>();
    //  List<Answer> control = new List<Answer>(Answers);

    //  Random r = new Random(DateTime.Now.Millisecond);

    //  while (control.Count != 0)
    //  {
    //    int index = r.Next(0, control.Count);
    //    randomized.Add(control.ElementAt(index));
    //    control.RemoveAt(index);
    //  }

    //  Answers = randomized;
    //}
  }
  #endregion

  public class QuizSessionData
  {
    public int QuestionCount { get; set; }
    public int QuestionOffset { get; set; }
    public bool ShowAnswersButton { get; set; }
    public bool ExamMode { get; set; }

    public bool Initialized { get; set; }

    public Student StudentData { get; set; }
    public Quiz QuizData { get; set; }
    public Question CurrentQuestion { get; set; }
    public List<CompletedQuestion> CompletedQuestionList { get; set; }
    public List<QuestionSummary> Summary { get; set; }

    public QuizSessionData()
    {
      QuestionCount = 0;
      QuestionCount = 0;
      ShowAnswersButton = false;
      ExamMode = false;
      Initialized = false;

      StudentData = null;
      QuizData = null;
      CurrentQuestion = null;
      CompletedQuestionList = null;
      Summary = null;
    }
  }

  public class Student
  {
    private Quiz quiz;
    public List<Question> Questions { get; private set; }
    public int currentQuestionIndex = 0;
    public int Offset { get; private set; }

    public Student(Quiz q)
    {
      quiz = q;
    }

    public void InitializeStudyMode(int count, int offset)
    {
      Offset = offset;

      if (IsOffsetAndCountValid(offset, count))
      {
        Questions = quiz.Take(offset, count);
      }
    }

    public void InitializeExamMode()
    {
      Questions = quiz.RandomQuestionsExamMode();
    }

    public bool IsOffsetAndCountValid(int offset, int count)
    {
      return quiz.IsOffsetAndCountValid(offset, count);
    }

    public Question NextQuestion()
    {
      if ((currentQuestionIndex + 1) > Questions.Count)
      {
        return null;
      }

      return Questions.ElementAt(currentQuestionIndex++);
    }

    public void AnswerQuestion(Question q, int[] answer)
    {
      q.StudentAnswer = answer;
    }
  }

  public class CompletedQuestion
  {
    public Question Q { get; private set; }
    public int[] A { get; private set; }

    public CompletedQuestion(Question q, int[] a)
    {
      Q = q;
      A = a;
    }
  }

  public class QuestionSummary
  {
    public string Text { get; private set; }
    public string Result { get; private set; }
    public string Answers { get; private set; }
    public string CorrectAnswer { get; private set; }
    public string ProvidedAnswer { get; private set; }
    public int ID { get; private set; }

    public QuestionSummary(string text, string result, string answer, string correct, string provided, int id)
    {
      Text = text;
      Result = result;
      Answers = answer;
      CorrectAnswer = correct;
      ProvidedAnswer = provided;
      ID = id;
    }
  }

  public class Quiz
  {
    private List<Question> questions;

    public List<Question> Questions { get { return questions; } }

    public Quiz(string dataFile)
    {
      questions = (from q in XDocument.Load(dataFile).Descendants("question")
                   select new Question(q.Element("text").Value, (from a in q.Descendants("answers").Descendants("answer")
                                                                 select new Answer(a.Value, Convert.ToBoolean(a.Attribute("correct").Value))).ToList())).ToList();
    }

    public bool IsOffsetAndCountValid(int offset, int count)
    {
      if (count > questions.Count() || count < 0)
      {
        return false;
      }

      if (offset + count > questions.Count() || offset < 0)
      {
        return false;
      }

      return true;
    }

    public List<Question> RandomQuestionsExamMode()
    {
      List<Question> randomized = Randomize(Randomize(questions));
      List<Question> exam = new List<Question>();

      Random r = new Random(DateTime.Now.Millisecond);

      int totalQuestions = (randomized.Count() > 100) ? 100 : randomized.Count();

      for (int i = 0; i < totalQuestions; i++)
      {
        int index = r.Next(0, randomized.Count);
        exam.Add(randomized.ElementAt(index));
        randomized.RemoveAt(index);
      }

      return exam;
    }

    public List<Question> TakeRandom(int offset, int count)
    {
      return Randomize(Take(offset, count));
    }

    private List<Question> Randomize(List<Question> control)
    {
      List<Question> randomized = new List<Question>();

      Random r = new Random(DateTime.Now.Millisecond);

      while (control.Count != 0)
      {
        int index = r.Next(0, control.Count);
        randomized.Add(control.ElementAt(index));
        control.RemoveAt(index);
      }

      return randomized;
    }

    public List<Question> Take(int offset, int count)
    {
      List<Question> set = new List<Question>();

      if (IsOffsetAndCountValid(count, offset))
      {
        if (offset != 0)
        {
          offset -= 1;
        }

        for (int i = offset; i < questions.Count(); i++)
        {
          set.Add(questions[i]);

          count--;

          if (count < 1)
          {
            break;
          }
        }
      }

      return set;
    }
  }

  public static class ExtensionMethods
  {
    public static string[] ToStringArray(this int[] arr)
    {
      string[] _strarray = new string[arr.Count()];

      for (int i = 0; i < arr.Count(); i++)
      {
        _strarray[i] = arr[i].ToString();
      }

      return _strarray;
    }
  }
}