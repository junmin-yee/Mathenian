using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Mathenian.Views
{
    public partial class ArithmeticLessonPage : ContentPage
    {
        private static readonly string[] QuestionTemplates = { "{0} + {1}", "{0} - {1}", "{0} * {1}", "{0} / {1}" };
        private const int NumQuestions = 10;

        private string[] _questionSet = new string[NumQuestions];
        public string[] QuestionSet { get => _questionSet; set => _questionSet = value; }

        private string[] _answerSet = new string[NumQuestions];
        public string[] AnswerSet { get => _answerSet; set => _answerSet = value; }

        public ArithmeticLessonPage()
        {
            InitializeComponent();
            GenerateQuestionSet();
        }

        private void GenerateQuestionSet()
        {
            for (int i = 0; i < NumQuestions; ++i)
            {
                Tuple<string, string> result = GenerateQuestion();
                _questionSet[i] = result.Item1;
                _answerSet[i] = result.Item2;
            }
        }

        private Tuple<string, string> GenerateQuestion()
        {
            var rand = new Random();
            int firstValue = rand.Next(1, 11);
            int secondValue = rand.Next(1, 11);
            switch (rand.Next(4))
            {
                case 0:
                    return Tuple.Create(string.Format(QuestionTemplates[0], firstValue, secondValue), 
                        (firstValue + secondValue).ToString());
                case 1:
                    return Tuple.Create(string.Format(QuestionTemplates[1], firstValue, secondValue),
                        (firstValue - secondValue).ToString());
                case 2:
                    return Tuple.Create(string.Format(QuestionTemplates[2], firstValue, secondValue),
                        (firstValue * secondValue).ToString());
                case 3:
                    firstValue = secondValue * rand.Next(1, 11);
                    return Tuple.Create(string.Format(QuestionTemplates[3], firstValue, secondValue),
                        (firstValue / secondValue).ToString());
            }
            throw new ArithmeticException();
        }
    }
}
