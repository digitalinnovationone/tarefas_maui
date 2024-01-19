using SQLite;
using Tarefas.Enums;
using Tarefas.Servicos;

namespace Tarefas.Models;

public class Comentario
{
    public Comentario()
    {
        this.Data = DateTime.Now;
    }

    [PrimaryKey, AutoIncrement]
    public int Id { get;set; }
    public string Texto { get;set; }
    public DateTime Data { get;set; }
    public int TarefaId { get;set; }
    public int UsuarioId { get;set; }

    [Ignore]
    public Usuario Usuario
    {
        get
        {
            if(this.UsuarioId == 0) return null;
            return UsuariosServico.Instancia().Todos().Find(u => u.Id == this.UsuarioId);
        }
    }

    [Ignore]
    public string NomeUsuario
    {
        get
        {
            if(this.Usuario == null) return "Sem Usuario";
            return Usuario?.Nome;
        }
    }
}