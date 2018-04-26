using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace scanner
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class escribe : ContentPage
	{
        cuerpos.restClient cliente;
        public escribe ()
		{
			InitializeComponent ();
            
        }
        
        private async Task Buscar(object sender, EventArgs e) {
            if (contentEntry.Text != string.Empty || contentEntry.Text != "") {
                
                cliente = new cuerpos.restClient();

                var respuesta = await cliente.getOrden(contentEntry.Text);
              
                if (respuesta.exists == false)
                {
                    await DisplayAlert("Resultado", "No se encuentra la orden", "Aceptar");
                }
                else {
                    
                    await Navigation.PushAsync(new decision(JsonConvert.SerializeObject(respuesta, Formatting.Indented)));
                    //Navigation.RemovePage(this);
                }
                cliente = null;
            }
            else {
                await DisplayAlert("Error", "Instroduce el Código", "Aceptar");
            }
        }
        private void Cancelar(object sender, EventArgs e) {
            Navigation.PopToRootAsync();
        }

    }
}