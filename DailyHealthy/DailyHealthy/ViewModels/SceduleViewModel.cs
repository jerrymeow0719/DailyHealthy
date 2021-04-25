using DailyHealthy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace DailyHealthy.ViewModels
{ 
    public class SceduleViewModel : BaseViewModel,  INotifyPropertyChanged
    {
        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));

        public SceduleViewModel() : base()
        {
            Events = new EventCollection() 
            {
            };
            ParsedbEvents(DateTime.Today.Year, DateTime.Today.Month);
        }

        public EventCollection Events { get; }

        private int _month = DateTime.Today.Month;
        public int Month
        {
            get => _month;
            set
            {
                ParsedbEvents(_year, value);
                SetProperty(ref _month, value);
            }
        }

        public int _year = DateTime.Today.Year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }


        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                App.dateTime = value;
                SetProperty(ref _selectedDate, value);
            }
        }

        private async void ParsedbEvents(int year,int month)
        {
            Events.Clear();
            //List<EventModel> eventModels = new List<EventModel>();
            List<ConstantsModel> constantsModels = new List<ConstantsModel>();
            Constants constants = new Constants();
            constantsModels = await constants.GetItemsAsync();

            var queryDate = from data in constantsModels
                       where data.Datetime.Year == year && data.Datetime.Month == month
                       group data by data.Datetime into newGroup
                       select newGroup;
            foreach (var dateGroup in queryDate)
            {
                List<EventModel> eventModels = new List<EventModel>();
                int index = dateGroup.Key.Day - DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
                foreach (var CM in dateGroup)
                {
                    EventModel eventModel = new EventModel()
                    {
                        Name = CM.Name,
                        GLU = CM.GLU_PC.ToString() + "    " + CM.GLU_AC.ToString(),
                        Description = CM.Description,
                        Path = CM.Path
                    };
                    eventModels.Add(eventModel);
                }
                Events.Add(new DateTime(year, month, DateTime.DaysInMonth(year, month)).AddDays(index), eventModels);
            }
        }

        private DateTime _minimumDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month,1).AddMonths(-6);
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        private DateTime _maximumDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

        public DateTime MaximumDate
        {
            get => _maximumDate;
            set => SetProperty(ref _maximumDate, value);
        }

        private async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is EventModel eventModel)
            {
                await App.Current.MainPage.DisplayAlert(eventModel.Name, eventModel.Description, "Ok");
            }
        }
    }
}