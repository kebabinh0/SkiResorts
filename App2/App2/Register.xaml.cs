using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
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

        private void Register_Clicked(object sender, EventArgs e)
        {
            User user = new User();
            UserInfo userInfo = new UserInfo();
            Regex regex_email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex regex_password = new Regex(@"^(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$");


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


            db.CreateTable<User>();
            db.CreateTable<UserInfo>();

            int correct_counter = 0;
            var table_user = db.Table<User>();
            if (login.Text.Length == 0)
            {
                login_empty_alert.IsVisible = true;
            }
            else
            {
                
                foreach (var s in table_user)
                {
                    if (login.Text == s.Username)
                    {
                        login_alert.IsVisible = true;
                        
                    }
                    else
                    {
                        user.Username = login.Text;
                        login_alert.IsVisible = false;
                        correct_counter++;
                        Console.WriteLine("password" + correct_counter);
                       
                        break;
                    }
                }
            }
            
            if (password.Text.Length==0)
            {
                password_alert.IsVisible = true;
            }
            else
            {
                Match match_password = regex_password.Match(password.Text);
                if (match_password.Success)
                {
                    user.Password = password.Text;
                    password_alert.IsVisible = false;
                    correct_counter++;
                    Console.WriteLine("password" + correct_counter);
                   
                }
                else
                {
                    password_alert.IsVisible = true;
                }
            }
           
            user.Password = password.Text;


            userInfo.First_Name = first_name.Text;
            userInfo.Last_Name = last_name.Text;

            if (email.Text.Length==0)
            {
                email_alert.IsVisible = true;
            }
            else
            {
                Match match_email = regex_email.Match(email.Text);
                if (match_email.Success)
                {
                    userInfo.Email = email.Text;
                    email_alert.IsVisible = false;
                    correct_counter++;
                    Console.WriteLine("email" +correct_counter);
                   
                }
                else
                {
                    email_alert.IsVisible = true;
                }
            }

            

            if (correct_counter == 3)
            {
                db.Insert(user);
                var select_user_id = db.Query<User>("select Id from User where Id=" + user.Id);
                int user_id = select_user_id[0].Id;
                userInfo.User_Id = user_id;
                db.Insert(userInfo);
                DisplayAlert("Registration", "You created account", "OK");
                user.Username = login.Text = "";
                user.Password = password.Text = "";
                userInfo.First_Name = first_name.Text = "";
                userInfo.Last_Name = last_name.Text = "";
                userInfo.Email = email.Text = "";
                correct_counter = 0;
            }
            else
            {
                DisplayAlert("Registration", "Failed to create account.\nCheck entry fields.", "OK");
                correct_counter = 0;
            }

            


            /*

            foreach (var s in table_user)
            {
                Console.WriteLine(s.Id + " Username: " + s.Username + " Password: " + s.Password);
            }


            var table_userInfo = db.Table<UserInfo>();

            foreach (var s in table_userInfo)
            {
                Console.WriteLine(s.Id + " User ID " + s.User_Id + " FristName " + s.First_Name + " LastName " + s.Last_Name + " Email " + s.Email);
            }

             */
        }
    }
}