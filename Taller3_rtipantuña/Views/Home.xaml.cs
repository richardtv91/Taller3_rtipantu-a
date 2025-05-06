using System.Text.RegularExpressions;

namespace Taller3_rtipantuña.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
        PickerIdentificacion.SelectedIndex = 0; // Default to CI
    }
    private void PickerIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = PickerIdentificacion.SelectedItem?.ToString();
        EntryIdentificacion.Text = string.Empty;

        // Adjust keyboard and placeholder as per selection
        if (selected == "CI")
        {
            EntryIdentificacion.Keyboard = Keyboard.Numeric;
            EntryIdentificacion.Placeholder = "10 números";
            EntryIdentificacion.MaxLength = 10;
        }
        else if (selected == "RUC")
        {
            EntryIdentificacion.Keyboard = Keyboard.Numeric;
            EntryIdentificacion.Placeholder = "13 números";
            EntryIdentificacion.MaxLength = 13;
        }
        else if (selected == "Pasaporte")
        {
            EntryIdentificacion.Keyboard = Keyboard.Text;
            EntryIdentificacion.Placeholder = "Ingrese pasaporte";
            EntryIdentificacion.MaxLength = int.MaxValue;
        }
    }

    private  async void ButtonVerDetalles_Clicked(object sender, EventArgs e)
    {
        if (!ValidateInputs())
            return;

        // Gather data
        var contactData = new ContactData
        {
            TipoIdentificacion = PickerIdentificacion.SelectedItem.ToString(),
            Identificacion = EntryIdentificacion.Text.Trim(),
            Nombres = EntryNombres.Text.Trim(),
            Apellidos = EntryApellidos.Text.Trim(),
            Fecha = DatePickerFecha.Date,
            Correo = EntryCorreo.Text.Trim(),
            Salario = decimal.Parse(EntrySalario.Text.Trim())
        };

         await Navigation.PushAsync(new Views.Detalle(contactData));
       // Navigation.PushModalAsync(new Views.Detalle(contactData));
    }

    private bool ValidateInputs()
    {
        var tipo = PickerIdentificacion.SelectedItem?.ToString();
        string id = EntryIdentificacion.Text?.Trim();
        string nombres = EntryNombres.Text?.Trim();
        string apellidos = EntryApellidos.Text?.Trim();
        string correo = EntryCorreo.Text?.Trim();
        string salarioStr = EntrySalario.Text?.Trim();

        if (string.IsNullOrEmpty(tipo))
        {
            DisplayAlert("Error", "Seleccione un tipo de identificación.", "OK");
            return false;
        }
        if (string.IsNullOrEmpty(id))
        {
            DisplayAlert("Error", $"Ingrese la {tipo}.", "OK");
            return false;
        }
        if (tipo == "CI" && (id.Length != 10 || !Regex.IsMatch(id, @"^\d{10}$")))
        {
            DisplayAlert("Error", "CI debe tener exactamente 10 números.", "OK");
            return false;
        }
        if (tipo == "RUC" && (id.Length != 13 || !Regex.IsMatch(id, @"^\d{13}$")))
        {
            DisplayAlert("Error", "RUC debe tener exactamente 13 números.", "OK");
            return false;
        }
        // Pasaporte no has length restrictions

        if (string.IsNullOrEmpty(nombres))
        {
            DisplayAlert("Error", "Ingrese los nombres.", "OK");
            return false;
        }
        if (string.IsNullOrEmpty(apellidos))
        {
            DisplayAlert("Error", "Ingrese los apellidos.", "OK");
            return false;
        }
        if (string.IsNullOrEmpty(correo) || !IsValidEmail(correo))
        {
            DisplayAlert("Error", "Ingrese un correo válido.", "OK");
            return false;
        }
        if (string.IsNullOrEmpty(salarioStr) || !decimal.TryParse(salarioStr, out decimal salario) || salario <= 0)
        {
            DisplayAlert("Error", "Ingrese un salario válido y mayor que cero.", "OK");
            return false;
        }

        return true;
    }

    bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        try
        {
            // Use simple regex for email validation
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
        catch
        {
            return false;
        }
    }
}

// Class to hold contact data to pass between pages
public class ContactData
{
    public string TipoIdentificacion { get; set; }
    public string Identificacion { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public DateTime Fecha { get; set; }
    public string Correo { get; set; }
    public decimal Salario { get; set; }


}