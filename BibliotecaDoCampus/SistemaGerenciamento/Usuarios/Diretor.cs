using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciamento.Usuarios
{
    internal class Diretor : Funcionario
    {
        int idDiretor;

        public Diretor()
        {
            nivelAcesso = NivelAcesso.Diretor;
        }
        public Atendente CadastrarFuncionario(string nome, int idAtendente, NivelAcesso acesso, string? codigoDeAcesso, string? senha)
        {
            Atendente a = new Atendente();
            a.nome = nome;
            a.idAtendente = idAtendente;
            a.nivelAcesso = acesso;
            a.codigoDeAcesso = codigoDeAcesso;
            a.senha = senha;


            return a;
        }
    }
}
