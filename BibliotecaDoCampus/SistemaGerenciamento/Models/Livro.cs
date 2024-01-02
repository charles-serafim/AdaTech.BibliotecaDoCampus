using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public class Livro
{
    private static int s_contadorLivro = 0;
    public int IdLivro { get; set; }
    public string _titulo;
    public string _autores;
    public int _edicao;
    public Acervo _acervo;
    public EstadoLivro _estadoLivro;

    public Livro(string titulo, string autores, int edicao, Acervo acervo, EstadoLivro estadoLivro)
    {
        IdLivro = ++s_contadorLivro;
        _titulo = titulo;
        _autores = autores;
        _edicao = edicao;
        _acervo = acervo;
        _estadoLivro = estadoLivro;
    }

    public void MostrarDados()
    {
        Console.WriteLine($"Id do livro: {IdLivro}\n"
                        + $"Título: {_titulo}\n"
                        + $"Autor: {_autores}\n"
                        + $"Edição: {_edicao}a edição\n"
                        + $"Condição: {_estadoLivro}\n"
                        + $"Acervo: {_acervo}\n");
    }

    public void DevolverLivro(EstadoLivro novoEstadoLivro)
    {
        this._estadoLivro = novoEstadoLivro;
    }
}
