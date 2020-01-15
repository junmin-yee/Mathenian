using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathenian.ViewModels
{
    public class ArithmeticLessonPageViewModel : BindableBase
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private static readonly string[] QuestionTemplates = { "{0} + {1}", "{0} - {1}", "{0} * {1}", "{0} / {1}" };
        private const int NumQuestions = 10;

        private string _title;
        public string Title { get => _title; set => _title = value; }

        private string[] _questionSet = new string[NumQuestions];
        public string[] QuestionSet { get => _questionSet; set => _questionSet = value; }

        private string[] _answerSet = new string[NumQuestions];
        public string[] AnswerSet { get => _answerSet; set => _answerSet = value; }

        private int _questionIndex = 0;
        public int QuestionIndex { get => _questionIndex; set => _questionIndex = value; }

        private string _currentQuestion;

        private int _numAnswersCorrect;
        public int NumAnswersCorrect { get => _numAnswersCorrect; set => _numAnswersCorrect = value; }

        private string _answerInput;
        public string AnswerInput
        {
            get => _answerInput;
            set
            {
                _answerInput = value;
                RaisePropertyChanged("AnswerInput");
            }
        }

        public string CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                RaisePropertyChanged("CurrentQuestion");
            }
        }

        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        public ArithmeticLessonPageViewModel(INavigationService navigationService)
        {
            Title = "Arithmetic Lesson";
            _navigationService = navigationService;
            GenerateQuestionSet();
            CurrentQuestion = QuestionSet[0];
        }

        async void ExecuteNavigateCommand()
        {
            if (AnswerSet[QuestionIndex].Equals(AnswerInput))
            {
                ++NumAnswersCorrect;
            }

            if (QuestionIndex < NumQuestions - 1)
            {
                AnswerInput = "";
                CurrentQuestion = QuestionSet[++QuestionIndex];
            }
            else
            {
                var parameters = new NavigationParameters();
                parameters.Add("NumCorrect", NumAnswersCorrect);

                await _navigationService.NavigateAsync("ArithmeticResultsPage", parameters);
            }
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
            lock (syncLock)
            {
                int firstValue = random.Next(1, 11);
                int secondValue = random.Next(1, 11);
                switch (random.Next(4))
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
                        firstValue = secondValue * random.Next(1, 11);
                        return Tuple.Create(string.Format(QuestionTemplates[3], firstValue, secondValue),
                            (firstValue / secondValue).ToString());
                }
            }
            throw new ArithmeticException();
        }
    }
}
