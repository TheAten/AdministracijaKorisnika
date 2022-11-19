using AdministracijaKorisnika.Commands;
using AdministracijaKorisnika.DataAccess;
using AdministracijaKorisnika.Helpers;
using AdministracijaKorisnika.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace AdministracijaKorisnika.ViewModel
{
	public class AdminWindowViewModel : INotifyPropertyChanged, IOpenWindow
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		


		private User currentUser;

		public User CurrentUser
		{
			get { return currentUser; }
			set { currentUser = value; }
		}

		private List<User> userList;

		public List<User> UserList
		{
			get { return userList; }
			set { userList = value; }
		}

		private ListCollectionView userListView;

		public ListCollectionView UserListView
		{
			get { return userListView; }
			set { userListView = value; }
		}

		private ICommand viewCommand;

		public ICommand ViewCommand
		{
			get { return viewCommand; }
			set { viewCommand = value; }
		}

		private ICommand newCommand;

		public ICommand NewCommand
		{
			get { return newCommand; }
			set { newCommand = value; }
		}

		public Action OpenView { get; set; }
		public Action OpenNew { get; set; }

		void ViewExecute(object obj)
		{
			if(CurrentUser is null)
			{
				MessageBox.Show("No user selected");
			}else OpenView?.Invoke();
		}

		bool CanView(object obj)
		{
			if (CurrentUser == null) return false;

			return true;
		}

		void NewExecute(object obj)
		{
			OpenNew?.Invoke();
		}

		bool CanNew(object obj)
		{
			return true;
		}


		public AdminWindowViewModel()
		{
			
            GlobalConfig.InitializeConnection();
            ViewCommand = new RelayCommand(ViewExecute, CanView);
            NewCommand = new RelayCommand(NewExecute, CanNew);
            UserList = GlobalConfig.dataAcces.GetAllUsers();
            UserListView = new ListCollectionView(UserList);
			//CurrentUser = new User();
		}
	}
}
