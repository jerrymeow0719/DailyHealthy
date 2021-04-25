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
            labelTitle.Text = dateTime.ToString("yyyy/MM/dd");
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

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            //Save to db
            Constants constants = new Constants();
            ConstantsModel constantsModel = new ConstantsModel()
            {
                Datetime = Selecteddatatime,
                Path = PicturePath
            };

            if (entry_name != null && !string.IsNullOrWhiteSpace(entry_name.Text))
                constantsModel.Name = entry_name.Text.ToString();

            if (entry_PC != null && !string.IsNullOrWhiteSpace(entry_PC.Text))
                constantsModel.GLU_PC = Convert.ToInt32(entry_PC.Text.ToString().Trim());
            else
                constantsModel.GLU_PC = 0;

            if (entry_AC != null && !string.IsNullOrWhiteSpace(entry_AC.Text))
                constantsModel.GLU_AC = Convert.ToInt32(entry_AC.Text.ToString().Trim());
            else
                constantsModel.GLU_AC = 0;

            if (entry_note != null && !string.IsNullOrWhiteSpace(entry_note.Text))
                constantsModel.Description = entry_note.Text.ToString();
            else
                constantsModel.Description = "";

            ConstantsModel Checkitem = await constants.GetItemsAsync(constantsModel.Datetime, constantsModel.Name);
            if (Checkitem == null)
                constants.InsertItemAsync(constantsModel);
            else
                constants.UpdateItemAsync(constantsModel);
            await App.Current.MainPage.DisplayAlert("Save", "Save complete", "Ok");
            await Navigation.PopAsync();
        }
    }
}