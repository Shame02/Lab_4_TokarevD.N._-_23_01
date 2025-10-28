using Lab_4_TokarevD.N._БПИ_23_01.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab_4_TokarevD.N._БПИ_23_01.Helper;
using Lab_4_TokarevD.N._БПИ_23_01.View;
using System.Windows;

namespace Lab_4_TokarevD.N._БПИ_23_01.ViewModel
{
    public class RoleViewModel : INotifyPropertyChanged
    {
        private Role selectedRole;
        public Role SelectedRole
        {
            get => selectedRole;
            set { selectedRole = value; OnPropertyChanged();  }
        }

        public ObservableCollection<Role> ListRole { get; set; } = new ObservableCollection<Role>();

        public RoleViewModel()
        {
            ListRole.Add(new Role { Id = 1, NameRole = "Директор" });
            ListRole.Add(new Role { Id = 2, NameRole = "Бухгалтер" });
            ListRole.Add(new Role { Id = 3, NameRole = "Менеджер" });
        }

        public int MaxId() 
        {
            int max = 0;
            foreach (var r in this.ListRole)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;

        }

        private RelayCommand addRole;
        public RelayCommand AddRole
        {
            get
            {
                return addRole ??
                (addRole = new RelayCommand(obj =>
                {
                    WindowNewRole wnRole = new WindowNewRole
                    {
                        Title = "Новая должность",
                    };
                    int maxIdRole = MaxId() + 1;
                    Role role = new Role { Id = maxIdRole };
                    wnRole.DataContext = role;
                    if (wnRole.ShowDialog() == true)
                    {
                        ListRole.Add(role);
                    }
                    SelectedRole = role;
                }));
            }
        }

        private RelayCommand editRole;
        public RelayCommand EditRole
        {
            get
            {
                return editRole ??
                (editRole = new RelayCommand(obj =>
                {
                    WindowNewRole wnRole = new WindowNewRole
                    { Title = "Редактирование должности", };
                    Role role = SelectedRole;
                    Role tempRole = new Role();
                    tempRole = role.ShallowCopy();
                    wnRole.DataContext = tempRole;
                    if (wnRole.ShowDialog() == true)
                    {

                        role.NameRole = tempRole.NameRole;
                    }
                }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }

        private RelayCommand deleteRole;
        public RelayCommand DeleteRole
        {
            get
            {
                return deleteRole ??
                (deleteRole = new RelayCommand(obj =>
                {
                    Role role = SelectedRole;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по должности: " + role.NameRole, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListRole.Remove(role);
                    }
                }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
