using BeautySalon.Commands;
using System;

namespace BeautySalon.ViewModels
{
    class MainViewViewModel : BaseViewModel
    {
        public MainViewViewModel()
        {
        }
        private BaseViewModel selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }

            set
            {
                OnChanged(out selectedViewModel,value);
            }
        }
        private BaseCommand switchPageCommand = null;
        public BaseCommand SwitchPageCommand => switchPageCommand ??= new BaseCommand(x =>
        {
            if (x.ToString() == "Home")
            {
                SelectedViewModel = new HomeViewModel();
            }

            if (x.ToString() == "Calendar")
            {
                SelectedViewModel = new CalendarViewModel();
            }
            else if(x.ToString() == "Staff")
            {
                SelectedViewModel = new StaffViewModel();
            }
            else if (x.ToString() == "Client")
            {
                SelectedViewModel = new ClientViewModel();
            }
            else if (x.ToString() == "Services")
            {
               SelectedViewModel = new ServiceViewModel();
            }
            else if (x.ToString() == "Expenses")
            {
                SelectedViewModel = new ExpenseViewModel();
            }
        }, () => true);
    }
}
