using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public class Reserva
{
    protected static int s_contadorReserva = 0;
    public int IdReserva { get; set; }
    public int _idUsuario;
    public int _idLivro;
    public DateTime _dataInicio;
    public DateTime _dataFim;
    public EstadoReserva _estadoReserva;

    public Reserva(int idusuario, int idLivro, DateTime dataInicio, DateTime dataFim)
    {
        IdReserva = ++s_contadorReserva;
        _idUsuario = idusuario;
        _idLivro = idLivro;
        _dataInicio = dataInicio;
        _dataFim = dataFim;
    }

    public void MostrarDados(List<Usuario> listaDeUsuarios, List<Livro> listaDeLivros)
    {
        Console.WriteLine($"Id da reserva: {IdReserva}\n"
                        + $"Usuário: {listaDeUsuarios[_idUsuario - 1]._nome}\n"
                        + $"Livro: {listaDeLivros[_idLivro - 1]._titulo}\n"
                        + $"Data de início: {_dataInicio.ToString()}\n"
                        + $"Data de fim: {_dataFim.ToString()}\n");
    }
}
