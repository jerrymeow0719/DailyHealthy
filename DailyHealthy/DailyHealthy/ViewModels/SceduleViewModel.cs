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
        public string PhotoPath;
        public ICommand TodayCommand => new Command(() => { Year = DateTime.Today.Year; Month = DateTime.Today.Month; SelectedDate = DateTime.Today; });
        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));
        public ICommand AddCommand => new Command ( () => 
        {
            List<EventModel> eventModels = new List<EventModel>();
            EventModel test = new EventModel()
            {
                Name = "test1",
            };
            eventModels.Add(test);
            eventModels.Add(test);

            EventModel test2 = new EventModel()
            {
                Name = "test2",
            };
            eventModels.Add(test2);
            Events.Add(DateTime.Today, eventModels);
        });

        public SceduleViewModel() : base()
        {
            // testing all kinds of adding events
            // when initializing collection
            Events = new EventCollection() 
            {
            };

            //    Task.Delay(3000).ContinueWith(t =>
            //    {
            //        // get observable collection later
            //        var todayEvents = Events[DateTime.Now] as ObservableCollection<EventModel>;

            //        // insert/add items to observable collection
            //        todayEvents.Insert(0, new EventModel { Name = "Cool event insert", Description = "This is Cool event's description!" });
            //        todayEvents.Add(new EventModel { Name = "Cool event add", Description = "This is Cool event's description!" });

            //        Month += 1;
            //    }, TaskScheduler.FromCurrentSynchronizationContext());
            //}, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private IEnumerable<EventModel> GenerateEvents(int count, string name)
        {
            return Enumerable.Range(1, count).Select(x => new EventModel
            {
                Name = $"{name} event{x}",
                Description = $"This is {name} event{x}'s description!",
                GLU_AC = 80,
                GLU_PC = 120
            }) ;
        }

        public EventCollection Events { get; }

        private int _month = DateTime.Today.Month;
        public int Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
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
                //Parse db events first
                //Events.Add(value, new List<EventModel>(GenerateEvents(1, "test")));
                ParsedbEvents(value);
                SetProperty(ref _selectedDate, value);
            }
        }

        private void ParsedbEvents(DateTime dateTime)
        {
            List<EventModel> eventModels = new List<EventModel>();
            eventModels.Add(new EventModel()
            {
                Name = "test",
            });
            //Parse data in db
            Events[dateTime] = eventModels;
        }

        private DateTime _minimumDate = new DateTime(2019, 4, 29);
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        private DateTime _maximumDate = DateTime.Today.AddMonths(5);

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