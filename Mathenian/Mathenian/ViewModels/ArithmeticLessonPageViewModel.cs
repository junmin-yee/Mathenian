using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Mathenian.ViewModels
{
    public class ArithmeticLessonPageViewModel : BindableBase
    {
        private const int NumQuestions = 10;

        private string _title;
        public string Title { get => _title; set => _title = value; }

        private string[] _questionSet = new string[NumQuestions];
        public string[] QuestionSet { get => _questionSet; set => _questionSet = value; }

        private string[] _answerSet = new string[NumQuestions];
        public string[] AnswerSet { get => _answerSet; set => _answerSet = value; }

        private int _questionIndex = 0;
        public int QuestionIndex { get => _questionIndex; set => _questionIndex = value; }

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

        private string _currentQuestion;
        public string CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                RaisePropertyChanged("CurrentQuestion");
            }
        }

        private Color[] colors = new Color[] { Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray,
                                               Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray, };
        public Color[] Colors
        {
            get => colors;
            set
            {
                colors = value;
                RaisePropertyChanged("Colors");
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

            var factory = new QuestionSet().ExecuteCreate(Topic.Arithmetic, NumQuestions, Mastery.Bronze);

            Tuple<string[], string[]> results = factory.GenerateQuestionSet();
            QuestionSet = results.Item1;
            AnswerSet = results.Item2;

            CurrentQuestion = QuestionSet[0];
        }

        async void ExecuteNavigateCommand()
        {
            if (AnswerSet[QuestionIndex].Equals(AnswerInput))
            {
                Colors[QuestionIndex] = Color.LawnGreen;
                Colors = Colors; // PLEASE MAKE MORE EFFICIENT, RAISE PROPERTY ON INDIVIDUAL UPDATE
                ++NumAnswersCorrect;
            }
            else
            {
                Colors[QuestionIndex] = Color.Red;
                Colors = Colors;
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
    }
}
