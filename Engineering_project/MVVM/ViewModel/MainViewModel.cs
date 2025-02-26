using System;
using System.Windows.Input;
using Engineering_project.Core;
using Engineering_project.MVVM.View;
using System.Windows;

namespace Engineering_project.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public MainViewModel()
        {
            NavigateToLoginCommand = new RelayCommand(NavigateToLogin);
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
        }

        private void NavigateToLogin()
        {
            var loginWindow = new LoginView();
            loginWindow.Show();  
        }

        private void NavigateToRegister()
        {
            var registerWindow = new CreateAccountView();
            registerWindow.Show();  
        }
    }
}
