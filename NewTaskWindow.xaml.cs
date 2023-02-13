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

namespace ToDoApplication
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        public Task Result { get; set; }
        public NewTaskWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Task t = new Task();
            if (NameTextBox.Text != "")
            {
                t.Name = NameTextBox.Text;
                t.Description = DescriptionTextBox.Text;
                t.IsCompleted = IsCompletedCheckBox.IsChecked.Value;
                Result = t;
                DialogResult = true;
            }
            else
                NameTextBox.Background = Brushes.Red;

        }
    }
}
