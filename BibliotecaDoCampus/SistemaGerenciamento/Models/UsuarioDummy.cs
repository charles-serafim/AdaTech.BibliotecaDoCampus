using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Models;

public class UsuarioDummy
{
    private static int s_contadorUsuario = 0;
    public int IdUsuario { get; set; }
    public string _nome;
    public string _email;
    public double _multaTotal = 0;
    public NivelAcesso _nivelAcesso;

    public UsuarioDummy(string nome, string email, NivelAcesso nivelAcesso)
    {
        IdUsuario = ++s_contadorUsuario;
        _nome = nome;
        _email = email;
        _multaTotal = 0;
        _nivelAcesso = nivelAcesso;
    }

    public void MostrarDados()
    {
        Console.WriteLine($"Id do usuário: {IdUsuario}\n"
                        + $"Nome: {_nome}\n"
                        + $"Email: {_email}\n"
                        + $"Multa total: R$ {_multaTotal:F2}\n"
                        + $"Tipo do usuário: {_nivelAcesso}\n");
    }
}
