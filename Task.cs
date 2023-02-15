using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;

namespace TaskApplication
{
    [Serializable]
    public class Task : INotifyPropertyChanged
    {
        
        DateTime createDate;
        //TimeSpan timeExecute;
        string completionDateToString;

        public string CreateDateToString => createDate.ToString("d-MMMM-yyyy HH:mm:ss");
        //public string TimeExecuteToString => timeExecute.ToString("d-MMMM-yyyy HH:mm:ss");

        public Task()
        {
            createDate = DateTime.Now;
        }

        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CompletionDateToString
        {
            get { return completionDateToString; }
            set
            {
                this.completionDateToString = value;
                NotifyPropertyChanged();
            }
        }

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
