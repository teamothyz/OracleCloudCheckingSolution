using System.ComponentModel;

namespace OracleAccountChecking.Models
{
    public class CountModel : INotifyPropertyChanged
    {
        private int total;
        private int success;
        private int failed;
        private int remaining;

        public CountModel()
        {
            total = 0;
            success = 0;
            failed = 0;
            remaining = 0;
        }

        public int Total
        {
            get => total;
            set
            {
                total = value;
                NotifyPropertyChanged(nameof(Total));
            }
        }

        public int Success
        {
            get => success;
            set
            {
                success = value;
                NotifyPropertyChanged(nameof(Success));
            }
        }

        public int Failed
        {
            get => failed;
            set
            {
                failed = value;
                NotifyPropertyChanged(nameof(Failed));
            }
        }

        public int Remaining
        {
            get => remaining;
            set
            {
                remaining = value;
                NotifyPropertyChanged(nameof(remaining));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
