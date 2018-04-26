using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace scanner
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class scanner : ContentPage
	{
		public scanner ()
		{
			
            var scanPage = new ZXingScannerPage();
            Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) => {
                // Stop scanning
                //scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async() => {
                    await Navigation.PopAsync();
                    mycode.Text = result.Text;
                });
            };

            // Navigate to our scanner page
             

        }
	}
}