using SQLite;
using Tarefas.Enums;
using Tarefas.Servicos;

namespace Tarefas.Models;

public class Tarefa
{
    public Tarefa()
    {
        this.DataCriacao = DateTime.Now;
        this.DataAtualizacao = DateTime.Now;
    }

    [PrimaryKey, AutoIncrement]
    public int Id { get;set; }
    public string Titulo { get;set; }
    public string Descricao { get;set; }
    public DateTime DataCriacao { get;set; }
    public DateTime DataAtualizacao { get;set; }
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
    public Status? Status { get;set; }
}