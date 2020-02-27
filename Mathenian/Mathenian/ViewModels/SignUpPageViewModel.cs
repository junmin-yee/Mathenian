﻿using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathenian.ViewModels
{
    public class SignUpPageViewModel : BindableBase
    {
        private string _username;
        public string Username 
        { 
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public DelegateCommand SubmitCommand { get; private set; }

        private readonly INavigationService _navigationService;

        public SignUpPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SubmitCommand = new DelegateCommand(ExecuteSubmitCommand);
        }

        async void ExecuteSubmitCommand()
        {
            UserCompletion userCompletion = new UserCompletion();
            Account account = new Account
            {
                Username = Username,
                Password = Hash(Password),
                Completion = userCompletion.GenerateForDatabase()
            };

            await App.Database.SaveItemAsync(account);
            await _navigationService.NavigateAsync("/MainPage");
        }

        private string Hash(string password)
        {
            return password; // Make safer xd
        }
    }
}