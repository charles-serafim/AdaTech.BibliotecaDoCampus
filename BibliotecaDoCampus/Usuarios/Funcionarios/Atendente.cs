using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Funcionarios
{
    internal class Atendente : Funcionario
    {
        int idAtendente;

        public void AtualizarCadastro(Usuario usuario, string? nome, string? sobrenome, string? email, NivelAcesso? nivelAcesso)
        {
            usuario.nome = nome;
            usuario.sobrenome = sobrenome;
            usuario.email = email;
            usuario.nivelAcesso = (NivelAcesso)nivelAcesso;
        }

        public void AutorizarEmprestimo(Livro livro, Usuario usuario, int? idReserva = 0)
        {
            throw new NotImplementedException();
        }
    }
}
