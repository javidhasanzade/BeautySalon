using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace BeautySalon.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected int branchNum;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnChanged<T>(out T prop, T value, [CallerMemberName] string propName = "")
        {
            prop = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public BaseViewModel()
        {
            InitializeBranchId();
        }
        public void InitializeBranchId()
        {
            this.branchNum = int.Parse(File.ReadAllText("settings.txt"));
        }
    }

    //class BranchSettings
    //{
    //    public int BranchId { get; set; }
    //}
}
