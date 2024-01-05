using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public class Emprestimo
{
    private static int s_contadorEmprestimos = 0;
    public static double ValorMultaDiaria = 1;
    public int _idEmprestimo;
    public int _idUsuario;
    public int _idLivro;
    public double multa = 0;
    public DateTime _dataInicio;
    public DateTime _dataLimite;
    public DateTime? _dataDevolucao;

    public Emprestimo(int idUsuario, int idLivro, DateTime dataInicio, DateTime dataLimite, DateTime dataDevolucao)
    {
        _idEmprestimo = ++s_contadorEmprestimos;
        _idUsuario = idUsuario;
        _idLivro = idLivro;
        _dataInicio = dataInicio;
        _dataLimite = dataLimite;
    }

    public void MostrarDados(List<Livro> listaDeLivros)
    {
        Console.WriteLine($"Id do empréstimo: {_idEmprestimo}\n"
                        + $"Livro: {listaDeLivros[_idLivro - 1]._titulo}\n"
                        + $"Data de início: {_dataInicio.ToString()}\n"
                        + $"Data limite para devolução: {_dataLimite.ToString()}\n");
    }

    public double DevolverLivro(DateTime dataDevolucao, Usuario usuario)
    {
        _dataDevolucao = dataDevolucao;
        TimeSpan atraso = dataDevolucao.Subtract(_dataLimite);

        if (atraso.Days > 0)
        {
            multa += atraso.Days * ValorMultaDiaria;
            usuario._multaTotal += multa;

            Console.WriteLine($"Livro devolvido com {atraso.Days} dias de atraso."
                            + $"Multa: R$ {multa:F2}");
            return multa;
        }

        Console.WriteLine($"Livro devolvido com sucesso!"
                        + $"Muito obrigado por utilizar nossos serviços.");
        return 0;
    }
}
