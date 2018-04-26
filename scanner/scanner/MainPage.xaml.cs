using Newtonsoft.Json;
using scanner.cuerpos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace scanner
{
    public partial class MainPage : ContentPage
    {
        cuerpos.restClient cliente;
        public MainPage()
        {
            InitializeComponent();
        }
        private void Escribe(object sender, EventArgs e)
        {
            Navigation.PushAsync(new escribe());
        }
        private void Scanea(object sender, EventArgs e)
        {

            var scanPage = new ZXingScannerPage();
            Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) => {
                Device.BeginInvokeOnMainThread(async () => {
                    //await Navigation.PopAsync();
                    //Navigation.RemovePage(this);
                    //await DisplayAlert("alert", "codigo: " + result.Text, "Aceptar");
                    await enviar_datosAsync(result.Text,scanPage);
                });
            };
        }
        public async Task enviar_datosAsync(String codigo, ZXingScannerPage scanPage)
        {
            cliente =  new cuerpos.restClient();
            var respuesta =  await cliente.getOrden(codigo);
            if (respuesta.exists == false)
            {
                await Navigation.PopToRootAsync();
                await DisplayAlert("Resultado", "No se encuentra la orden", "Aceptar");
            }
            else
            {
                await Navigation.PushAsync(new decision(JsonConvert.SerializeObject(respuesta, Formatting.Indented)));
                Navigation.RemovePage(scanPage);
            }
            //DisplayAlert("mensaje", JsonConvert.SerializeObject(respuesta, Formatting.Indented), "Aceptar");
            
        }
    }
}
