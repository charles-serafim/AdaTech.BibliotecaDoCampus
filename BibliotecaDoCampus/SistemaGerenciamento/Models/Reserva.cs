using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public class Reserva
{
    private static int s_contadorReserva = 0;
    public int IdReserva { get; set; }
    public Usuario _usuario;
    public Livro _livro;
    public DateTime _dataInicio;
    public DateTime _dataFim;

    public Reserva(Usuario usuario, Livro livro, DateTime dataInicio, DateTime dataFim)
    {
        IdReserva = ++s_contadorReserva;
        _usuario = usuario;
        _livro = livro;
        _dataInicio = dataInicio;
        _dataFim = dataFim;
    }

    public void MostrarDados()
    {
        Console.WriteLine($"Id da reserva: {IdReserva}\n"
                        + $"Usuário: {_usuario._nome}\n"
                        + $"Livro: {_livro._titulo}\n"
                        + $"Data de início: {_dataInicio.ToString()}\n"
                        + $"Data de fim: {_dataFim.ToString()}\n");
    }
}
