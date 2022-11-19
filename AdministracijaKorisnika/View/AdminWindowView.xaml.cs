using AdministracijaKorisnika.Helpers;
using AdministracijaKorisnika.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdministracijaKorisnika.View
{
    /// <summary>
    /// Interaction logic for AdminWindowView.xaml
    /// </summary>
    public partial class AdminWindowView : Window
    {
        public AdminWindowView()
        {
            InitializeComponent();
            Loaded += AdminWindowView_Loaded;
        }

        private void AdminWindowView_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is IOpenWindow vm)
            {
                vm.OpenView += () =>
                {
                    AdminWindowViewModel adminWindowViewModel = (AdminWindowViewModel)DataContext;
                    ViewUserView viewUserView = new ViewUserView();
                    viewUserView.DataContext = new ViewUserViewModel(adminWindowViewModel.CurrentUser.Clone());
                    viewUserView.ShowDialog();
                };

                vm.OpenNew += () =>
                {
                    NewUserView newUserView = new NewUserView
                    {
                        DataContext = new NewUserViewModel()
                    };
                    newUserView.ShowDialog();
                };
            }
        }
    }
}
