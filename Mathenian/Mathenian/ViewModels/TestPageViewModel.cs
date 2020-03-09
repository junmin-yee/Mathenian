﻿using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Mathenian.ViewModels
{
    public class TestPageViewModel : BindableBase, INavigationAware
    {
        private const int NumQuestions = 20;

        private string _title;
        public string Title
        {
            get => _title;
            set { SetProperty(ref _title, value); }
        }

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
                SetProperty(ref _answerInput, value);
            }
        }

        private string _currentQuestion;
        public string CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                SetProperty(ref _currentQuestion, value);
            }
        }

        private Topic _topic;
        private Mastery _mastery;
        private Account _userAccount;

        private Color[] _colors = new Color[] { Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray,
                                                Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray,
                                                Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray,
                                                Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray, Color.LightGray };
        public Color[] Colors { get => _colors; set => _colors = value; }

        public DelegateCommand NavigateCommand { get; private set; }
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public TestPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            Title = "Lesson";
            _navigationService = navigationService;
            _dialogService = dialogService;
            NavigateCommand = new DelegateCommand(ExecuteNavigateCommand);
        }

        async void ExecuteNavigateCommand()
        {
            if (AnswerSet[QuestionIndex].Equals(AnswerInput))
            {
                Colors[QuestionIndex] = Color.LawnGreen;
                ++NumAnswersCorrect;
            }
            else
            {
                Colors[QuestionIndex] = Color.Red;
                if (QuestionIndex - 2 > NumAnswersCorrect)
                {
                    await _dialogService.DisplayAlertAsync("Test Failed", "You got too many wrong answers!", "Ok");
                    var parameters = new NavigationParameters
                    {
                        { "Topic", _topic },
                        { "Mastery", _mastery },
                        { "Account", _userAccount }
                    };
                    await _navigationService.GoBackAsync(parameters);
                }
            }

            RaisePropertyChanged("Colors");

            if (QuestionIndex < NumQuestions - 1)
            {
                AnswerInput = "";
                CurrentQuestion = QuestionSet[++QuestionIndex];
            }
            else
            {
                var parameters = new NavigationParameters
                {
                    { "NumCorrect", NumAnswersCorrect },
                    { "NumQuestions", NumQuestions },
                    { "Topic", _topic },
                    { "Account", _userAccount }
                };

                await _navigationService.NavigateAsync("ResultsPage", parameters);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _topic = parameters.GetValue<Topic>("Topic");
            _mastery = parameters.GetValue<Mastery>("Mastery");
            _userAccount = parameters.GetValue<Account>("Account");

            Title = _topic.ToString() + " Lesson Page";

            var factory = new QuestionSet().ExecuteCreate(_topic, NumQuestions, _mastery);

            Tuple<string[], string[]> results = factory.GenerateQuestionSet();
            QuestionSet = results.Item1;
            AnswerSet = results.Item2;

            CurrentQuestion = QuestionSet[0];
        }
    }
}