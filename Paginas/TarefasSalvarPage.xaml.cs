using Tarefas.Models;
using Tarefas.Enums;
using Tarefas.Servicos;
using Tarefas.Constantes;

namespace Tarefas.Paginas;

public partial class TarefasSalvarPage : ContentPage
{
	DatabaseServico<Tarefa> _tarefaServico;
	public Tarefa Tarefa { get; set; }

	public TarefasSalvarPage(Tarefa tarefa = null)
	{
		InitializeComponent();

		_tarefaServico = new DatabaseServico<Tarefa>(Db.DB_PATH);

		Tarefa = tarefa ?? new Tarefa();
        BindingContext = tarefa;

		StatusPicker.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        UsuarioPicker.ItemsSource = UsuariosServico.Instancia().Todos();

		StatusPicker.SelectedItem = Tarefa.Status ?? Status.Backlog;
    	UsuarioPicker.SelectedItem = Tarefa.Usuario;
	}

	private async void OnSaveClicked(object sender, EventArgs e)
    {
		if(string.IsNullOrEmpty(TituloEntry.Text))
		{
			await DisplayAlert("Erro", "O Nome é obrigatório", "Ok");
			TituloEntry.Focus();
			return;
		}

		Tarefa.Titulo = TituloEntry.Text;
        Tarefa.Descricao = DescricaoEditor.Text;

		if(StatusPicker.SelectedItem != null)
        	Tarefa.Status = (Status)StatusPicker.SelectedItem;
		else
        	Tarefa.Status = Status.Backlog;

		if(UsuarioPicker.SelectedItem != null)
        	Tarefa.UsuarioId = ((Usuario)UsuarioPicker.SelectedItem).Id;

		if(Tarefa.Id == 0)
			await _tarefaServico.IncluirAsync(Tarefa);
		else
		{
			Tarefa.DataAtualizacao = DateTime.Now;
			await _tarefaServico.AlterarAsync(Tarefa);
		}

        await Navigation.PopAsync();
    }

	private async void VoltarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

