using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppService025
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btnStart.Clicked += BtnStart_Clicked;
            btnStop.Clicked += BtnStop_Clicked;

            MessagingCenter.Unsubscribe<string>(this, "Location");
            MessagingCenter.Subscribe<MyLocationMessage>(this, "Location", (location) =>
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            
                Device.BeginInvokeOnMainThread(() =>
                {
                    lblCounterValue.Text += $"{Environment.NewLine}{location.Latitude}, {location.Longitude}, {DateTime.Now.ToLongTimeString()}";

                }
                //lblCounterValue.Text = sloc

                );

            });
        }
        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IServiceOnOff>().StartService();

            //var startMessage = new MyStartMessage()
            //{
            //    IntervalSecond = int.Parse(entSecond.Text)
            //};

            //MessagingCenter.Send<MainPage, MyStartMessage>(this, "StartService", startMessage);
            ////Preferences.Set("MyTrackingServiceRunning", true);
            //lblMainMessage.Text = "Send Start Service Message";
        }
        private void BtnStop_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IServiceOnOff>().StopService();

            //var stopMessage = new MyStopMessage();
            //MessagingCenter.Send<MainPage, MyStopMessage>(this, "StopService", stopMessage);
            ////Preferences.Set("MyTrackingServiceRunning", false);

            //lblMainMessage.Text = "Send Stop Service Message";
        }

        
    }
}
