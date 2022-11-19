using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AdministracijaKorisnika.Model
{
    public class User : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public int Id { get; set; }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                if (username == value) return;

                username = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "" || string.IsNullOrWhiteSpace(value))
                {
                    errors.Add("Username can't be empty");
                    SetErrors(nameof(Username), errors);
                    valid = false;
                }

                else if (!Regex.Match(value, @"^[a-zA-Z]+$").Success)
                {
                    errors.Add("Username can only contain letters");
                    SetErrors("Username", errors);
                    valid = false;
                }

                if (valid)
                {
                    ClearErrors("Username");
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Username"));
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (password == value) return;

                password = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value=="")
                {
                    errors.Add("Password can't be empty");
                    SetErrors("Password", errors);
                    valid = false;
                }

                if (!Regex.Match(value, @"^[a-zA-Z]+$").Success)
                {
                    errors.Add("Password can only contain letters");
                    SetErrors("Password", errors);
                    valid = false;
                }

                if (valid)
                {
                    ClearErrors("Password");
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Password"));
            }
        }

        private int isAdmin;

        public int IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }

        public User Clone()
        {
            User clonedUser = new();

            clonedUser.Username = this.Username;
            clonedUser.Password = this.Password;
            clonedUser.IsAdmin = this.isAdmin;
            clonedUser.Id = this.Id;

            return clonedUser;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                return (errors.Count > 0);
            }
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);
        }

        public IEnumerable? GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (errors.Values);
            }
            else
            {
                if (errors.ContainsKey(propertyName))
                {
                    return(errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }
        }

        private void SetErrors(string propertyName, List<string> propertyErrors)
        {
            errors.Remove(propertyName);
            errors.Add(propertyName, propertyErrors);
            if (ErrorsChanged != null) ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            errors.Remove(propertyName);
            if (ErrorsChanged != null) ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            User objUser = (User)obj;

            if (objUser.Id == this.Id) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}