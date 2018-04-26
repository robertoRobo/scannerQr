using Newtonsoft.Json;
using scanner.cuerpos;
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
	public partial class decision : ContentPage
	{
        orden data;
		public decision (string json)
		{
            data = JsonConvert.DeserializeObject<orden>(json);
            InitializeComponent();
            
            num_orden.Text = "Número de Orden: "+data.num_orden.ToString();
            id_usuario.Text = "Número de Usuario: "+data.id_usuario.ToString();
            id_sucursal.Text = "Sucursal: "+data.id_sucursal.ToString();
            descripcion.Text = "Descripción Pedido: "+data.descripcion.ToString();
            total.Text = "Cantidad Pagada: "+data.total.ToString();
            codigo.Text = "Código Orden: "+data.codigo.ToString();
            fecha.Text = "Fecha del Pedido:"+data.fecha.Year.ToString()+"/"+data.fecha.Month+"/"+data.fecha.Day;
        }

        
        async void Aceptar(object sender, EventArgs e)
        {
            restClient cliente = new restClient();
            var respuesta = await cliente.DeleteOrden(data.codigo);
            if (respuesta.exists) {
                await DisplayAlert("Exito", "La orden numero: "+data.num_orden.ToString()+"fue eliminada", "Aceptar");
                await Navigation.PopToRootAsync();
            }
        }
        async void Cancelar(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

    }
}