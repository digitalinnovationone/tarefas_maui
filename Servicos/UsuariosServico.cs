using SQLite;
using Tarefas.Models;

namespace Tarefas.Servicos;

public class UsuariosServico
{
    private static UsuariosServico _usuariosServico = new UsuariosServico();
    private List<Usuario> _usuarios = new List<Usuario>();

    private UsuariosServico()
    {
        _usuarios.Add(new Usuario { Id = 1, Nome = "Danilo" });
        _usuarios.Add(new Usuario { Id = 2, Nome = "Sheila" });
        _usuarios.Add(new Usuario { Id = 3, Nome = "Lana" });
        _usuarios.Add(new Usuario { Id = 4, Nome = "Liah" });
    }

    public static UsuariosServico Instancia()
    {
        return _usuariosServico;
    }

    public List<Usuario> Todos()
    {
        return _usuarios;
    }
}