using SQLite;
using Tarefas.Enums;
using Tarefas.Servicos;

namespace Tarefas.Models;

public class Anexo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get;set; }
    public string Arquivo { get;set; }
    public int TarefaId { get;set; }
    public double Latitude { get;set; }
    public double Longitude { get;set; }
}