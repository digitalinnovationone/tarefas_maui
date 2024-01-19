using System.Windows.Input;
using Tarefas.Constantes;
using Tarefas.Models;
using Tarefas.Servicos;

namespace Tarefas.Paginas;

public partial class MainPage : ContentPage
{
	DatabaseServico<Tarefa> _tarefaServico;

	public ICommand IrParaDetalhesCommand { get; private set; }

	public MainPage()
	{
		InitializeComponent();
		_tarefaServico = new DatabaseServico<Tarefa>(Db.DB_PATH);

        IrParaDetalhesCommand = new Command<Tarefa>(async (tarefa) => await IrParaDetalhes(tarefa));

		CarregarTarefas();
	}

	protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarTarefas();
    }

	private async Task IrParaDetalhes(Tarefa tarefa)
    {
        await Navigation.PushAsync(new TarefasDetalhePage(tarefa));
    }

	private async void CarregarTarefas()
	{
		CardBacklog.ItemsSource = await _tarefaServico.Query().Where(t => t.Status == Enums.Status.Backlog).ToArrayAsync();
		CardAnalise.ItemsSource = await _tarefaServico.Query().Where(t => t.Status == Enums.Status.Analise).ToArrayAsync();
		CardParaFazer.ItemsSource = await _tarefaServico.Query().Where(t => t.Status == Enums.Status.ParaFazer).ToArrayAsync();
		CardDesenvolvimento.ItemsSource = await _tarefaServico.Query().Where(t => t.Status == Enums.Status.Desenvolvimento).ToArrayAsync();
		CardFeito.ItemsSource = await _tarefaServico.Query().Where(t => t.Status == Enums.Status.Feito).ToArrayAsync();
	}

	private async void NovoClicked(object sender, EventArgs e)
    {
		var botao = sender as Button;
		if(sender != null)
		{
			var status = (Enums.Status)botao.CommandParameter;
        	await Navigation.PushAsync(new TarefasSalvarPage(new Tarefa { Status = status }));
		}
    }

}

