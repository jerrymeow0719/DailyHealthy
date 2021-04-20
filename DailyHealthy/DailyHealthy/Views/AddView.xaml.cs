using DailyHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyHealthy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddView : ContentPage
    {
        private DateTime Selecteddatatime;
        private string PicturePath; 
        public AddView(DateTime dateTime)
        {
            InitializeComponent();
            Selecteddatatime = dateTime;
        }

        private async void BtnLoadPic_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo",
            });

            if (result != null)
            {
                PicturePath = result.FullPath;

                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        private async void BtnTakePic_Clicked(object sender, EventArgs e)
        {
              var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Please take a photo",
            });

            if (result != null)
            {
                PicturePath = result.FullPath;

                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            //Save to db
            Constants constants = new Constants();
            EventModel eventModel = new EventModel()
            {
                Datetime = Selecteddatatime,
                Name = entry_name.Text.ToString().Trim(),
                GLU_PC = Convert.ToInt32(entry_PC.Text.ToString().Trim()),
                GLU_AC = Convert.ToInt32(entry_AC.Text.ToString().Trim()),
                Description = entry_note.Text.ToString().Trim(),
                Path = PicturePath
            };

            List<EventModel> Checkitem = constants.GetItemsAsync(eventModel.Datetime, eventModel.Name);
            if (Checkitem.Count == 0)
                constants.InsertItemAsync(eventModel);
            else
                constants.UpdateItemAsync(eventModel);
            Navigation.PopAsync();
        }
    }
}