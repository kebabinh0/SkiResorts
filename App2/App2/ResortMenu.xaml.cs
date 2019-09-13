using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResortMenu : ContentPage
	{
		public ResortMenu ()
		{
			InitializeComponent ();
		}

        private async void Select_resort_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new Resort1());
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Token.loggedUser = 0;
            await Navigation.PushAsync(new MainPage());
        }
    }
}