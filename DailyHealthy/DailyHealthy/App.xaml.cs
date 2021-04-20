using DailyHealthy.Views;
using Xamarin.Forms;

namespace DailyHealthy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SceduleView();
        }

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
