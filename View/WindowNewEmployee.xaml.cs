using Lab_4_TokarevD.N._БПИ_23_01.Model;
using Lab_4_TokarevD.N._БПИ_23_01.ViewModel;
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

namespace Lab_4_TokarevD.N._БПИ_23_01.View
{

    public partial class WindowNewEmployee : Window
    {
        public WindowNewEmployee()
        {
            InitializeComponent();


            var roleVm = new RoleViewModel();
            CbRole.ItemsSource = roleVm.ListRole;


            if (DataContext is PersonDpo person)
            {
                CbRole.SelectedValue = person.RoleId; 
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PersonDpo person)
            {
                if (CbRole.SelectedItem is Role selectedRole)
                {
                    person.RoleId = selectedRole.Id; 
                    person.RoleName = selectedRole.NameRole;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите должность.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                DialogResult = true;
            }
        }
    }
}
