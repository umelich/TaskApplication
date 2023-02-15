﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;

namespace TaskApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Task> tasksList = new ObservableCollection<Task>();
        public MainWindow()
        {
            InitializeComponent();

            ToDoListBox.ItemsSource = tasksList;
            ToDoListBox.DisplayMemberPath = "Name";

            Description.ItemsSource = tasksList;
            Description.DisplayMemberPath = "Description";   
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
                    selected.IsCompleted = false;
                else
                    selected.IsCompleted = true;
            }
            Refresh();
        }

        private void AllRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (AllRadioButton.IsChecked == true || AllRadioButton.IsChecked == null)
                CompleteButton.Content = "Завершити";
            ToDoListBox.ItemsSource = tasksList;
            Description.ItemsSource = tasksList;
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
            ToDoListBox.ItemsSource = filtered;
            Description.ItemsSource = filtered;
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
            ToDoListBox.ItemsSource = filtered;
            Description.ItemsSource = filtered;
        }

        string fileName = "data.bin";
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

                ToDoListBox.ItemsSource = tasksList;
                Description.ItemsSource = tasksList;
                AllRadioButton.IsChecked = true;
            }
            catch (Exception )
            {
                MessageBox.Show("Не вдалось відкрити збережені задачі");
            }
        }
        public void Refresh()
        {
            if (NotCompletedRadioButton.IsChecked == true)
                NotCompletedRadioButtonMethod();
            else if (CompletedRadioButton.IsChecked == true)
                CompletedRadioButtonMethod();
            else
                ToDoListBox.ItemsSource = tasksList;
        }
    }
}
