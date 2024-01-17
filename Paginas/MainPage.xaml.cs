using System.Windows.Input;
using Tarefas.Constantes;
using Tarefas.Models;
using Tarefas.Servicos;

namespace Tarefas.Paginas;

public partial class MainPage : ContentPage
{
	DatabaseServico<Tarefa> _tarefaServico;

	public ICommand NavigateToDetailCommand { get; private set; }
	public ICommand DeleteCommand { get; private set; }
	public ICommand NavigateToChangeCommand { get; private set; }
	
	public MainPage()
	{
		InitializeComponent();
		_tarefaServico = new DatabaseServico<Tarefa>(Db.DB_PATH);

        NavigateToDetailCommand = new Command<Tarefa>(async (tarefa) => await NavigateToDetail(tarefa));
        NavigateToChangeCommand = new Command<Tarefa>(async (tarefa) => await NavigateToChange(tarefa));
		DeleteCommand = new Command<Tarefa>(ExecuteDeleteCommand);

		TarefasCollectionTable.BindingContext = this;

		CarregarTarefas();
	}

	protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarTarefas();
    }

	private async void ExecuteDeleteCommand(Tarefa tarefa)
	{
		bool confirm = await DisplayAlert("Confirmação", "Deseja excluir esta tarefa?", "Sim", "Não");
		if (confirm)
		{
			await _tarefaServico.DeleteAsync(tarefa);
			CarregarTarefas();
		}
	}

	private async Task NavigateToChange(Tarefa tarefa)
    {
        await Navigation.PushAsync(new TarefasSalvarPage(tarefa));
    }

	private async Task NavigateToDetail(Tarefa tarefa)
    {
        await Navigation.PushAsync(new TarefasDetalhePage(tarefa));
    }

	private async void CarregarTarefas()
	{
		var tarefas = await _tarefaServico.TodosAsync();
		TarefasCollectionTable.ItemsSource = tarefas;
	}

	private async void NovoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TarefasSalvarPage());
    }

}

