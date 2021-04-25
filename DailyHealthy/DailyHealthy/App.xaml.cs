using DailyHealthy.Views;
using System;
using Xamarin.Forms;

namespace DailyHealthy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SceduleView());
        }

        public static DateTime dateTime = DateTime.Today;

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
