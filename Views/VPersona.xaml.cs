using S7.Models;

namespace S7.Views;

public partial class VPersona : ContentPage
{
	public VPersona()
	{
		InitializeComponent();
	}

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        lblStauts.Text = "";
        if (string.IsNullOrEmpty(txtId.Text))
        {
            App.personRepo.AddNewPerson(txtName.Text);
            lblStauts.Text = App.personRepo.StatusMessage;
        }
        else 
        {
            App.personRepo.UpdatePerson(int.Parse(txtId.Text), txtName.Text);
            lblStauts.Text = App.personRepo.StatusMessage;
        }
        
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStauts.Text = "";
        List<Persona> people = App.personRepo.getAllPeople();
        listaPersona.ItemsSource = people;
        lblStauts.Text = App.personRepo.StatusMessage;
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        lblStauts.Text = "";
        if (!string.IsNullOrEmpty(txtId.Text))
        {
            App.personRepo.DeletePerson(int.Parse(txtId.Text));
            lblStauts.Text = App.personRepo.StatusMessage;
        }
        else {
            DisplayAlert("Error", "Debe ingresar un ID antes para eliminar", "Cerrar");
        }
    }
}