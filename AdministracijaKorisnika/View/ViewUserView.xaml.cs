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
    /// Interaction logic for ViewUserView.xaml
    /// </summary>
    public partial class ViewUserView : Window
    {
        public ViewUserView()
        {
            InitializeComponent();
            Loaded += ViewUserView_Loaded;
        }

        private void ViewUserView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewUserViewModel viewUserViewModel = (ViewUserViewModel)DataContext;
            viewUserViewModel.Done += ViewUserViewModel_Done;

            if (DataContext is ICloseWindow vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
            }
        }

        private void ViewUserViewModel_Done(object sender, ViewUserViewModel.DoneEventArgs e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
