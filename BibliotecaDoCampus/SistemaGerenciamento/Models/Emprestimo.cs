using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public class Emprestimo : Reserva
{
    public static int totalEmprestimos;
    public int idEmprestimo;
    public static double ValorMultaDiaria = 1;
    public double multa = 0;
    public DateTime _dataLimite;
    public DateTime? _dataDevolucao;
    public int idUsuario;
    public string idLivro;

    public Emprestimo(int idusuario, int idLivro, DateTime dataInicio, DateTime dataLimite, DateTime dataDevolucao)
    {
        IdReserva = ++s_contadorReserva;
        _idUsuario = idusuario;
        _idLivro = idLivro;
        _dataInicio = dataInicio;
        _dataLimite = dataLimite;
        totalEmprestimos++;
        idEmprestimo = totalEmprestimos;
    }

    public void MostrarDados(List<Livro> listaDeLivros)
    {
        Console.WriteLine($"Id do empréstimo: {IdReserva}\n"
                        + $"Livro: {listaDeLivros[_idLivro - 1]._titulo}\n"
                        + $"Data de início: {_dataInicio.ToString()}\n"
                        + $"Data limite para devolução: {_dataFim.ToString()}\n");
    }

    public void DevolverLivro(DateTime dataDevolucao, Usuario usuario)
    {
        _dataDevolucao = dataDevolucao;
        TimeSpan atraso = dataDevolucao.Subtract(_dataLimite);

        if (atraso.Days > 0)
        {
            multa += atraso.Days * ValorMultaDiaria;
            usuario._multaTotal += multa;
            this._estadoReserva = EstadoReserva.FinalizadaComMulta;

            Console.WriteLine($"Livro devolvido com {atraso.Days} dias de atraso."
                            + $"Multa: R$ {multa:F2}");
            return;
        }

        this._estadoReserva = EstadoReserva.Finalizada;
        Console.WriteLine($"Livro devolvido com sucesso!"
                        + $"Muito obrigado por utilizar nossos serviços.");
    }
}
