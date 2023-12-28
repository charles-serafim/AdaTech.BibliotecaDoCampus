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

        public void AtualizarCadastro(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void AutorizarEmprestimo(Livro livro, Usuario usuario, int? idReserva = 0)
        {
            throw new NotImplementedException();
        }
    }
}
