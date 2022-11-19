using AdministracijaKorisnika.Commands;
using AdministracijaKorisnika.DataAccess;
using AdministracijaKorisnika.Helpers;
using AdministracijaKorisnika.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministracijaKorisnika.ViewModel
{
    public class ViewUserViewModel : ICloseWindow, INotifyPropertyChanged
    {
		
		private User currentUser;

		public User CurrentUser
		{
			get { return currentUser; }
			set { 
				if(currentUser == value) return;

				currentUser = value;

				OnPropertyChanged(new PropertyChangedEventArgs("CurrentUser"));
			
			
			}
		}

		private ICommand updateCommand;

		public ICommand UpdateCommand
		{
			get { return updateCommand; }
			set { 
				if(updateCommand == value)
				{
					return;
				}

				updateCommand = value;

                OnPropertyChanged(new PropertyChangedEventArgs("UpdateCommand"));
            }
		}

		public Action Close { get; set; }

		void UpdateExecute(object obj)
		{
			if (CurrentUser != null && !CurrentUser.HasErrors)
			{
				GlobalConfig.dataAcces.UpdateUser(CurrentUser, CurrentUser.Id);
				OnDone(new DoneEventArgs("User has been saved"));
                
                Close?.Invoke();
			}
			else OnDone(new DoneEventArgs("User has not been saved"));

			
		}

		bool CanUpdate(object obj)
		{
			if (CurrentUser != null) return true;
			else return false;
		}

		public ViewUserViewModel()
		{

		}

		public ViewUserViewModel(User model)
		{
			
			UpdateCommand = new RelayCommand(UpdateExecute, CanUpdate);
			GlobalConfig.InitializeConnection();
			CurrentUser = model;
		}

        public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if(PropertyChanged != null) PropertyChanged(this, e);
		}

		public delegate void DoneEventHandler(object sender, DoneEventArgs e);

		public class DoneEventArgs : EventArgs
		{
			private string message;

			public string Message
			{
				get { return message; }
				set {
					if(message == value) return;

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
			if(Done!=null) Done(this, e);
		}
	
	}
}
