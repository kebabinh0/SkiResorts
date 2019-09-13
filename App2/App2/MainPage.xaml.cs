using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.IO;
using Microsoft.Data.Sqlite;
using SQLitePCL;


namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DoSomeDataAccess();
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    BackgroundImage = "main2.jpg";

                    break;
                case Device.UWP:
                    BackgroundImage = "Assets/main2.jpg";
                    break;
                default:
                    break;
            }
            


        }
        public static void DoSomeDataAccess()
        {
            //string applicationFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "CanFindLocation");
            //System.IO.Directory.CreateDirectory(applicationFolderPath);
            //Console.WriteLine("Creating database, if it doesn't already exist");


              string dbPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                  "ski_resorts.db3");

           

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                 "ski_resorts.db3");

                    break;
                case Device.UWP:
                    dbPath = "ski_resorts.db3";
                    break;
                default:
                    break;
            }

            var db = new SQLiteConnection(dbPath);

        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

      
    }
}
