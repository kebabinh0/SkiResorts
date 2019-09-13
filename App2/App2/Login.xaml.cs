using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent();
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

        private async void Login_Clicked(object sender, EventArgs e)
        {      
            Console.WriteLine("Creating database, if it doesn't already exist");
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



            var table_user = db.Table<User>();
            bool correct_credentials = false;
            foreach (var s in table_user)
            {

 


                if (username.Text == s.Username && password.Text == s.Password)
                {                   
                    await Navigation.PushAsync(new ResortMenu());
                    correct_credentials = true;
                    Token.loggedUser = s.Id;
                    break;
                }


            }
            if (correct_credentials == false)
            {
                await DisplayAlert("Login Failed", "Use proper credentials.", "OK");
            }

        }



        }
       
    }
