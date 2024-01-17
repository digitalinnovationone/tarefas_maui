namespace Tarefas;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell(); // mudo totalmente minha pagina
		// MainPage = new NavigationPage(new AppShell()); // mudo somente um frame referente ao menu
	}
}
