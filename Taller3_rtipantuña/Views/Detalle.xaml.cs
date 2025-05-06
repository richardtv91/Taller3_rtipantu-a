using System.Text;
using Microsoft.Maui.ApplicationModel.Communication;

namespace Taller3_rtipantuña.Views;

public partial class Detalle : ContentPage
{
    private readonly ContactData _contact;
    public Detalle(ContactData datos)
    {
		InitializeComponent();
        _contact = datos;
        DisplayContactData();
    }

   

    private void DisplayContactData()
    {
        LabelTipoIdentificacion.Text = _contact.TipoIdentificacion;
        LabelIdentificacion.Text = _contact.Identificacion;
        LabelNombres.Text = _contact.Nombres;
        LabelApellidos.Text = _contact.Apellidos;
        LabelFecha.Text = _contact.Fecha.ToString("dd/MM/yyyy");
        LabelCorreo.Text = _contact.Correo;
        LabelSalario.Text = $"${_contact.Salario:N2}";

        // Calculate IESS contribution (9.45%)
        decimal aporte = _contact.Salario * 0.0945m;
        LabelIESS.Text = $"${aporte:N2}";
    }

    private async void ButtonExportar_Clicked(object sender, EventArgs e)
    {
        string content =
$@"Detalle de Contacto
--------------------

Tipo de Identificación: {_contact.TipoIdentificacion}
Identificación: {_contact.Identificacion}
Nombres: {_contact.Nombres}
Apellidos: {_contact.Apellidos}
Fecha: {_contact.Fecha:dd/MM/yyyy}
Correo: {_contact.Correo}
Salario: ${_contact.Salario:N2}
Aporte al IESS (9.45%): ${(_contact.Salario * 0.0945m):N2}
";

        try
        {
            // Get a writable path
            string fileName = $"Contacto_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            // Get local app data directory that is writable on all platforms
            string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            File.WriteAllText(filePath, content, Encoding.UTF8);

            await DisplayAlert("Éxito", $"Archivo exportado correctamente:\n{filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo exportar el archivo: {ex.Message}", "OK");
        }
    }
}
	
