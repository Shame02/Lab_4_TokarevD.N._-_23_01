using Lab_4_TokarevD.N._БПИ_23_01.Helper;
using Lab_4_TokarevD.N._БПИ_23_01.Model;
using Lab_4_TokarevD.N._БПИ_23_01.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_4_TokarevD.N._БПИ_23_01.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> ListPerson { get; set; }
        public ObservableCollection<PersonDpo> ListPersonDpo { get; set; }

        private PersonDpo selectedPersonDpo;
        public PersonDpo SelectedPersonDpo
        {
            get { return selectedPersonDpo; }
            set
            {
                selectedPersonDpo = value;
                OnPropertyChanged();
            }
        }

        public PersonViewModel()
        {
            ListPerson = new ObservableCollection<Person>
            {
                new Person(1, 1, "Дмитрий", "Токарев", new DateTime(1990, 1, 1)),
                new Person(2, 2, "Никита", "Данченко", new DateTime(1992, 5, 10)),
                new Person(3, 3, "Андрей", "Вершини", new DateTime(1995, 9, 20))
            };

            ListPersonDpo = GetListPersonDpo();
        }

        // создаем DPO-список из основной коллекции Person
        public ObservableCollection<PersonDpo> GetListPersonDpo()
        {
            var list = new ObservableCollection<PersonDpo>();
            foreach (var p in ListPerson)
            {
                list.Add(new PersonDpo().CopyFromPerson(p));
            }
            return list;
        }

        public int MaxId()
        {
            if (ListPersonDpo.Count == 0)
                return 0;
            return ListPersonDpo.Max(x => x.Id);
        }

        // ===============================================================
        // Команды
        // ===============================================================

        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                if (addPerson == null)
                {
                    addPerson = new RelayCommand(obj =>
                    {
                        WindowNewEmployee wnPerson = new WindowNewEmployee
                        {
                            Title = "Добавление сотрудника"
                        };

                        PersonDpo per = new PersonDpo
                        {
                            Id = MaxId() + 1
                        };
                        wnPerson.DataContext = per;

                        if (wnPerson.ShowDialog() == true)
                        {
                            Role r = (Role)wnPerson.CbRole.SelectedItem;
                            per.RoleId = r.Id;
                            per.RoleName = r.NameRole;
                            ListPersonDpo.Add(per);

                            Person p = new Person();
                            p = p.CopyFromPersonDPO(per);
                            ListPerson.Add(p);
                        }
                    },
                    obj => true);
                }
                return addPerson;
            }
        }

        private RelayCommand editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                if (editPerson == null)
                {
                    editPerson = new RelayCommand(obj =>
                    {
                        WindowNewEmployee wnPerson = new WindowNewEmployee
                        {
                            Title = "Редактирование сотрудника"
                        };

                        PersonDpo tempPerson = SelectedPersonDpo.ShallowCopy();
                    wnPerson.DataContext = tempPerson;

                    if (wnPerson.ShowDialog() == true)
                    {
                        Role r = (Role)wnPerson.CbRole.SelectedItem;
                        SelectedPersonDpo.RoleId = r.Id;
                        SelectedPersonDpo.RoleName = r.NameRole;
                        SelectedPersonDpo.FirstName = tempPerson.FirstName;
                            SelectedPersonDpo.LastName = tempPerson.LastName;
                            SelectedPersonDpo.Birthday = tempPerson.Birthday;

                            Person p = ListPerson.FirstOrDefault(x => x.Id == SelectedPersonDpo.Id);
                            if (p != null)
                            {
                                p = p.CopyFromPersonDPO(SelectedPersonDpo);
                            }
                        }
                    },
                    obj => SelectedPersonDpo != null && ListPersonDpo.Count > 0);
                }
                return editPerson;
            }
        }

        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                if (deletePerson == null)
                {
                    deletePerson = new RelayCommand(obj =>
                    {
                        PersonDpo person = SelectedPersonDpo;
                        if (person == null) return;

                        MessageBoxResult result = MessageBox.Show(
                            "Удалить данные по сотруднику:\n" + person.LastName + " " + person.FirstName,
                            "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.OK)
                        {
                            ListPersonDpo.Remove(person);

                            Person existing = ListPerson.FirstOrDefault(p => p.Id == person.Id);
                            if (existing != null) ListPerson.Remove(existing);
                        }
                    },
                    obj => SelectedPersonDpo != null && ListPersonDpo.Count > 0);
                }
                return deletePerson;
            }
        }

        // ===============================================================

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


















