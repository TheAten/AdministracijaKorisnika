using AdministracijaKorisnika.Commands;
using AdministracijaKorisnika.DataAccess;
using AdministracijaKorisnika.Helpers;
using AdministracijaKorisnika.Model;
using AdministracijaKorisnika.View;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AdministracijaKorisnika.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged, ICloseWindow
    {

        private User user;

        public User User
        {
            get { return user; }
            set
            {
                if (user == value) return;

                user = value;

                OnPropertyChanged(new PropertyChangedEventArgs("User"));

            }
        }

        private ICommand loginCommand;

        public ICommand LoginCommand
        {
            get { return loginCommand; }
            set
            {
                if (loginCommand == value) return;

                loginCommand = value;

                OnPropertyChanged(new PropertyChangedEventArgs("LoginCommand"));

            }
        }



        void LoginExecute(object obj)
        {
            if (GlobalConfig.dataAcces.LoginCheck(User) == 1)
            {
                AdminWindowView adminWindowView = new AdminWindowView();
                adminWindowView.DataContext = new AdminWindowViewModel();
                adminWindowView.Show();
                Close?.Invoke();
            }
            else
            {
                MessageBox.Show("Login failed. Please try again");
            }

        }

        bool CanLogin(object obj)
        {
            return true;
        }

        public LoginViewModel()
        {
            User = new User();
            GlobalConfig.InitializeConnection();
            LoginCommand = new RelayCommand(LoginExecute, CanLogin);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);
        }

        public Action Close { get; set; }
    }
}
