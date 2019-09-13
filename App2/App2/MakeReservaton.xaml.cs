using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;


namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MakeReservaton : ContentPage
    {
        public MakeReservaton()
        {
            InitializeComponent();


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


            db.CreateTable<Reservation>();
           db.CreateTable<Resort>();

            var Vfirst_name =db.Query<UserInfo>("SELECT First_Name FROM UserInfo WHERE User_Id=" + Token.loggedUser);
            var Vlast_name =db.Query<UserInfo>("SELECT Last_Name FROM UserInfo WHERE User_Id=" + Token.loggedUser);
            var Vemail =db.Query<UserInfo>("SELECT Email FROM UserInfo WHERE User_Id=" + Token.loggedUser);

            first_name.Text = Vfirst_name[0].First_Name;
            last_name.Text = Vlast_name[0].Last_Name; 
            email.Text = Vemail[0].Email;

            

        }

        private void Number_of_people_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (number_of_people.Text.Length > 0)
            {
                if (Convert.ToInt32(number_of_people.Text) > 10 || Convert.ToInt32(number_of_people.Text) < 1)
                {
                    people_alert.IsVisible = true;
                }
                else
                {
                    people_alert.IsVisible = false;
                    Token.people = Convert.ToInt32(number_of_people.Text);
                    Changeprice_text();
                }
            }

        }

        private void Days_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (days.Text.Length > 0)
            {
                if (Convert.ToInt32(days.Text) > 6 || Convert.ToInt32(days.Text) < 1)
                {
                    days_alert.IsVisible = true;
                }
                else
                {
                    days_alert.IsVisible = false;
                    Token.days = Convert.ToInt32(days.Text);
                    Changeprice_text();
                }
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (days.Text.Length > 0 && number_of_people.Text.Length > 0)
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



                    Random rnd = new Random();

                    ResortSelected();

                    int good_counter = 0;

                    Reservation reserv = new Reservation();
                    Resort resort = new Resort();


                    if (Convert.ToInt32(days.Text) > 6 || Convert.ToInt32(days.Text) < 1)
                    {
                        days_alert.IsVisible = true;
                        good_counter = 0;
                    }
                    else
                    {
                        days_alert.IsVisible = false;
                        good_counter++;
                    }

                    if (Convert.ToInt32(number_of_people.Text) > 10 || Convert.ToInt32(number_of_people.Text) < 1)
                    {
                        people_alert.IsVisible = true;
                        good_counter = 0;
                    }
                    {
                        people_alert.IsVisible = false;
                        good_counter++;
                    }

                    if (good_counter == 2)
                    {
                        reserv.Resort = Resortprice.ResortNumber;
                        reserv.Prize = Changeprice();
                        reserv.SkiDays = Token.days;
                        reserv.PersonNumber = Token.people;
                        reserv.UserInfo_Id = Token.loggedUser;
                        reserv.Reservation_Id = rnd.Next(1, 1000);
                       db.Insert(reserv);

                        Console.WriteLine(reserv.Prize);

                        var Vfirst_name =db.Query<UserInfo>("SELECT First_Name FROM UserInfo WHERE User_Id=" + Token.loggedUser);
                        var Vlast_name =db.Query<UserInfo>("SELECT Last_Name FROM UserInfo WHERE User_Id=" + Token.loggedUser);
                        var Vemail =db.Query<UserInfo>("SELECT Email FROM UserInfo WHERE User_Id=" + Token.loggedUser);

                        first_name.Text = Vfirst_name[0].First_Name;
                        last_name.Text = Vlast_name[0].Last_Name;
                        email.Text = Vemail[0].Email;

                        await DisplayAlert("Your Reservation",
                         "First Name: " + first_name.Text + "\n" +
                         "Last Name: " + last_name.Text + "\n" +
                         "Email: " + email.Text + "\n" +
                         "Prize: " + reserv.Prize.ToString() + "\n" +
                         "Persons: " + reserv.PersonNumber.ToString() + "\n" +
                         "Days of Skiing: " + reserv.SkiDays.ToString() + "\n", "OK");

                        await Navigation.PushAsync(new ResortMenu());
                    }

                }
                else
                {
                    people_alert.IsVisible = true;
                    days_alert.IsVisible = true;

                    await DisplayAlert("Warning", "Check number of days or people and try again.", "OK");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {

        }
        private void Changeprice_text()
        {
            ResortSelected();
            price.Text = "Price: " + (Token.people * Resortprice.ResortPrice *  Token.days).ToString() + " EUR";
            
        }
        private int Changeprice()
        {
        return (Token.people* Resortprice.ResortPrice* Token.days);
        }

        private void ResortSelected()
        {

            switch (Resortprice.ResortNumber)
            {
                case 1:
                    Resortprice.ResortPrice = 500;
                    break;
                case 2:
                    Resortprice.ResortPrice = 400;
                    break;
                case 3:
                    Resortprice.ResortPrice = 350;
                    break;
                case 4:
                    Resortprice.ResortPrice = 299;
                    break;
                case 5:
                    Resortprice.ResortPrice = 150;
                    break;
                case 6:
                    Resortprice.ResortPrice = 50;
                    break;
                case 7:
                    Resortprice.ResortPrice = 80;
                    break;
                case 8:
                    Resortprice.ResortPrice = 900;
                    break;
                case 9:
                    Resortprice.ResortPrice = 452;
                    break;
                case 10:
                    Resortprice.ResortPrice = 99;
                    break;
                default:
                    break;
                   
            }

          
        }
    }
}