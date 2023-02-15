using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace TaskApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Task> tasksList = new ObservableCollection<Task>();
        string fileName = "data.bin";

        public MainWindow()
        {
            InitializeComponent();

            ToDoListBox.ItemsSource = Description.ItemsSource = CreateDateToString.ItemsSource = CompletionDateToString.ItemsSource = tasksList;
            ToDoListBox.DisplayMemberPath = "Name";
            Description.DisplayMemberPath = "Description";
            CreateDateToString.DisplayMemberPath = "CreateDateToString";
            CompletionDateToString.DisplayMemberPath = "CompletionDateToString";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow window = new NewTaskWindow();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (window.ShowDialog() == true)
            {
                Task newTask = window.Result;
                tasksList.Add(newTask);
            }
            Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Task selected = ToDoListBox.SelectedItem as Task;
            if (selected != null)
            {
                for (int i = 0; i < tasksList.Count; i++)
                {
                    if (selected.Name == tasksList[i].Name)
                        tasksList.Remove(selected);
                }
            }
            Refresh();
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            Task selected = ToDoListBox.SelectedItem as Task;
            if (selected != null)
            {
                if (CompletedRadioButton.IsChecked == true)
                {
                    selected.IsCompleted = false;
                    selected.CompletionDateToString = "";
                }
                else
                {
                    selected.IsCompleted = true;
                    selected.CompletionDateToString = DateTime.Now.ToString("d-MMMM-yyyy HH:mm:ss");
                    selected.CompletionDate = DateTime.Now;
                }
            }
            Refresh();
        }
        public void Refresh()
        {
            if (NotCompletedRadioButton.IsChecked == true)
                NotCompletedRadioButtonMethod();
            if (CompletedRadioButton.IsChecked == true)
                CompletedRadioButtonMethod();
        }

        private void AllRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (AllRadioButton.IsChecked == true || AllRadioButton.IsChecked == null)
                CompleteButton.Content = "Завершити";

            ToDoListBox.ItemsSource = Description.ItemsSource = CreateDateToString.ItemsSource = CompletionDateToString.ItemsSource = tasksList;
        }

        private void NotCompletedRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            NotCompletedRadioButtonMethod();
        }

        private void NotCompletedRadioButtonMethod()
        {
            if (NotCompletedRadioButton.IsChecked == true)
                CompleteButton.Content = "Завершити";
            ObservableCollection<Task> filtered = new ObservableCollection<Task>();
            for (int i = 0; i < tasksList.Count; i++)
            {
                Task current = tasksList[i];
                if (current.IsCompleted == false)
                {
                    filtered.Add(current);
                }
            }
            ToDoListBox.ItemsSource = Description.ItemsSource = CreateDateToString.ItemsSource = CompletionDateToString.ItemsSource = filtered;
        }

        private void CompletedRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (CompletedRadioButton.IsChecked == true)
                CompleteButton.Content = "Відмінити";
            CompletedRadioButtonMethod();
        }

        private void CompletedRadioButtonMethod()
        {
            ObservableCollection<Task> filtered = new ObservableCollection<Task>();
            for (int i = 0; i < tasksList.Count; i++)
            {
                Task current = tasksList[i];
                if (current.IsCompleted == true)
                {
                    filtered.Add(current);
                }
            }
            ToDoListBox.ItemsSource = Description.ItemsSource = CreateDateToString.ItemsSource = CompletionDateToString.ItemsSource = filtered;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream file = File.OpenWrite(fileName);
            formatter.Serialize(file, tasksList);
            file.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream file = File.OpenRead(fileName);
                tasksList = formatter.Deserialize(file) as ObservableCollection<Task>;
                file.Close();
                AllRadioButton.IsChecked = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Файл не знайдено");
            }
        }

    }
}
