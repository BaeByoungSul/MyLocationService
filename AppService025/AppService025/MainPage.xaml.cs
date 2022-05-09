using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //ObservableCollection<PP0370_Model> lstStock = new ObservableCollection<PP0370_Model>();
        //List<MyLocationMessage> lstLocation = new List<MyLocationMessage>();
        ObservableCollection<MyLocationMessage> lstLocation = new ObservableCollection<MyLocationMessage>();

        public MainPage()
        {
            InitializeComponent();

            
            btnStart.Clicked += BtnStart_Clicked;
            btnStop.Clicked += BtnStop_Clicked;

            
            MessagingCenter.Unsubscribe<MyLocationMessage>(this, "Location");
            MessagingCenter.Subscribe<MyLocationMessage>(this, "Location", (location) =>
            {
                //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            
                Device.BeginInvokeOnMainThread(() =>
                {
                    lstLocation.Add(new MyLocationMessage() {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Altitude = location.Altitude,
                        PointTime = location.PointTime
                        }
                    );
                    locCollectionView.ItemsSource = lstLocation;


                    //lblCounterValue.Text += $"{Environment.NewLine}{location.Latitude}, {location.Longitude}, {location.PointTime.ToLongTimeString()}";

                }
                //lblCounterValue.Text = sloc

                );

            });

            MessagingCenter.Unsubscribe<MyLocationErrorMessage>(this, "LocationError");
            MessagingCenter.Subscribe<MyLocationErrorMessage>(this, "LocationError", (location) =>
            {
                //Console.WriteLine($"Error Message: {location.ErrorMessage}");

                Device.BeginInvokeOnMainThread(() =>
                {
                    lblLocatonError.Text += $"{Environment.NewLine}{location.ErrorMessage}, {location.ErrorTime.ToLongTimeString()}";

                }
                //lblCounterValue.Text = sloc

                );

            });

        }
        private async void BtnStart_Clicked(object sender, EventArgs e)
        {
           // var permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationAlways>();
            //var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            var status = await Permissions.RequestAsync<Permissions.LocationAlways>();

            if (status == PermissionStatus.Denied)
            {
                return;
            }
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
