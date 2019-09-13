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
	public partial class Resort8 : ContentPage
	{
		public Resort8 ()
		{
			InitializeComponent ();
		}

        private async void Main_menu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResortMenu());
        }

        private async void Next_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Resort9());
        }

        private async void Make_reservation_Clicked(object sender, EventArgs e)
        {
            Resortprice.ResortNumber = 8;
            await Navigation.PushAsync(new MakeReservaton());
        }
    }
}