using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hw1_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string NamePattern = @"\b([A-ZÀ-ÿ][-,a-z. ']+[ ]*)";
        readonly string EmailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        readonly string FilePath = "./Resume.txt";

        public ObservableCollection<Worker> _persons;
        public ObservableCollection<Worker> Persons
        {
            get { return _persons; }
            set { _persons = value; }
        }
        private Worker _sperson;

        public Worker SPerson
        {
            get { return _sperson; }
            set { _sperson = value; }
        }
            
        public MainWindow()
        {
            InitializeComponent();
            if (!Load())
            {
                _persons = new ObservableCollection<Worker>();
            }
            DataContext = this;

            listView.ItemsSource = _persons;

            Worker example = new Worker("Floppa", 20, "popa@gmail.com", "садовая 4");
            example.AddAbilities(true, hw1_wpf.Knowelege.English);
            example.Convert();
            _persons.Add(example);

            
        }
        private void OKbutton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckUser() && CheckForRepeat(EmailInput.Text))
            {
                Worker newbie = new Worker(NameInput.Text, Int32.Parse(AgeInput.Text), EmailInput.Text, AdressInput.Text);
                AddUserAbilities(ref newbie);

                _persons.Add(newbie);
                System.Windows.Forms.MessageBox.Show("Added new user!");
            }
        }
        private void Cancelbutton_Click(object sender, RoutedEventArgs e)
        {
            NameInput.Text = "";
            AdressInput.Text = "";
            AgeInput.Text = "";
            EmailInput.Text = "";

            EnglishKnowelege.IsChecked = false;
            CPlusKnowelege.IsChecked = false;
            SQLKnowelege.IsChecked = false;
            CSharpKnowelege.IsChecked = false;
        }
        private void NameInput_TextChanged()
        {

        }
        private void AddUserAbilities(ref Worker w)
        {
            w.AddAbilities(EnglishKnowelege.IsChecked, hw1_wpf.Knowelege.English);
            w.AddAbilities(CPlusKnowelege.IsChecked, hw1_wpf.Knowelege.CPlusPlus);
            w.AddAbilities(SQLKnowelege.IsChecked, hw1_wpf.Knowelege.Sql);
            w.AddAbilities(CSharpKnowelege.IsChecked, hw1_wpf.Knowelege.CSharp);

            w.Convert();
        }
        private bool CheckUser()
        {
            if(Regex.IsMatch(NameInput.Text, NamePattern) && AdressInput.Text.Length > 5)
            {
                int age;
                if(Int32.TryParse(AgeInput.Text, out age))
                {
                    if(age < 100 || age > 0)
                    {
                        if (Regex.IsMatch(EmailInput.Text, EmailPattern))
                        {
                            return true;
                        }
                        else System.Windows.MessageBox.Show("Wrong email format", "Error");
                    }
                    else System.Windows.MessageBox.Show("Are you serious? input real age.", "Error");
                }
                else System.Windows.MessageBox.Show("Wrong age format, only numbers", "Error");
            }
            else System.Windows.MessageBox.Show("Wrong name or adress format", "Error");
            return false;
        }
        private bool CheckForRepeat(string Email)
        {
            foreach(Worker w in _persons)
            {
                if(string.Compare(w.Email, Email) == 0)
                {
                    System.Windows.MessageBox.Show("Already used this email!", "Error");
                    return false;
                }
            }
            return true;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private bool Load()
        {
            try
            {
                FileStream stream = new FileStream(FilePath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                _persons = (ObservableCollection<Worker>)formatter.Deserialize(stream);

                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void Save()
        {
            FileStream stream = new FileStream(FilePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, _persons);
            stream.Close();

        }
    }
}
