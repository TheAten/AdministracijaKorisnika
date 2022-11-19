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
    /// Interaction logic for NewUserView.xaml
    /// </summary>
    public partial class NewUserView : Window
    {
        public NewUserView()
        {
            InitializeComponent();
            Loaded += NewUserView_Loaded;
        }

        private void NewUserView_Loaded(object sender, RoutedEventArgs e)
        {
            NewUserViewModel newUserViewModel = (NewUserViewModel)DataContext;
            newUserViewModel.Done += NewUserViewModel_Done;

            if (DataContext is ICloseWindow vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }

        private void NewUserViewModel_Done(object sender, NewUserViewModel.DoneEventArgs e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
