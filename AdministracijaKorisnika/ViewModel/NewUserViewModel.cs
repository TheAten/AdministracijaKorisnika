using AdministracijaKorisnika.Commands;
using AdministracijaKorisnika.DataAccess;
using AdministracijaKorisnika.Helpers;
using AdministracijaKorisnika.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AdministracijaKorisnika.ViewModel
{
    public class NewUserViewModel : ICloseWindow, INotifyPropertyChanged
    {


        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (currentUser == value) return;

                currentUser = value;

                OnPropertyChanged(new PropertyChangedEventArgs("CurrentUser"));
            }
        }


        private ICommand createCommand;



        public ICommand CreateCommand
        {
            get { return createCommand; }
            set
            {
                if (createCommand == value) return;

                createCommand = value;

                OnPropertyChanged(new PropertyChangedEventArgs("CreateCommand"));
            }
        }

        public Action Close { get; set; }

        void CreateExecute(object obj)
        {
            int i = GlobalConfig.dataAcces.RegisterCheck(CurrentUser);
            MessageBox.Show(i.ToString());
            if (GlobalConfig.dataAcces.RegisterCheck(CurrentUser) == 0 &&
                CurrentUser.Username != null && CurrentUser.Password != null && !CurrentUser.HasErrors)
            {
                GlobalConfig.dataAcces.CreateUser(CurrentUser);
                OnDone(new DoneEventArgs("User has been created123123"));

                Close?.Invoke();

            }
            else OnDone(new DoneEventArgs("User has not been created"));


        }

        bool CanCreate(object obj)
        {

            return true;
        }


        public NewUserViewModel()
        {

            GlobalConfig.InitializeConnection();
            CreateCommand = new RelayCommand(CreateExecute, CanCreate);
            CurrentUser = new User();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);
        }

        public delegate void DoneEventHandler(object sender, DoneEventArgs e);

        public class DoneEventArgs : EventArgs
        {
            private string message;

            public string Message
            {
                get { return message; }
                set
                {
                    if (message == value) return;

                    message = value;

                }
            }

            public DoneEventArgs(string message)
            {
                Message = message;
            }
        }

        public event DoneEventHandler? Done;

        public void OnDone(DoneEventArgs e)
        {
            if (Done != null) Done(this, e);
        }
    }
}
