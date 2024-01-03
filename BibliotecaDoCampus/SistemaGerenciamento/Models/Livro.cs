using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public class Livro
{
    private static int s_contadorLivro = 0;
    public int IdLivro { get; set; }
    public string _titulo { get; set; }
    public string[] _autores { get; set; }
    public int _edicao { get; set; }
    public Acervo _acervo { get; set; }
    public EstadoLivro _estadoLivro { get; set; }


    // Possivelmente será necessário alterrar
    public Livro(string titulo, string[] autores, int edicao, Acervo acervo, EstadoLivro estadoLivro)
    {
        IdLivro = ++s_contadorLivro;
        _titulo = titulo;
        _autores = autores;
        _edicao = edicao;
        _acervo = acervo;
        _estadoLivro = estadoLivro;
    }

    [JsonConstructor]
    public Livro(int IdLivro, string _titulo, Acervo _acervo, string[] _autores, int _edicao, EstadoLivro _estadoLivro)
    {
        this.IdLivro = IdLivro;
        this._titulo = _titulo;
        this._acervo = _acervo;
        this._autores = _autores;
        this._edicao = _edicao;
        this._estadoLivro = _estadoLivro;
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
}
