using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Funcionarios
{
    public class Atendente : Funcionario
    {
        public int idAtendente;

        public void AtualizarCadastro(Usuario usuario, string? nome, string? sobrenome, string? email, NivelAcesso? nivelAcesso)
        {
            if(nome != null) usuario.nome = nome;
            if(sobrenome != null) usuario.sobrenome = sobrenome;
            if(email != null) usuario.email = email;
            if(nivelAcesso != null) usuario.nivelAcesso = (NivelAcesso)nivelAcesso;
        }

        public void AutorizarEmprestimo(Livro livro, Usuario usuario, int? idReserva = 0)
        {
            throw new NotImplementedException();
        }
    }
}
