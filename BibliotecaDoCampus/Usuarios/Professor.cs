using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios
{
    internal class Professor : Usuario
    {
        int idProfessor;
        string codigoDeAcesso;
        string senha;

        public Professor()
        {
            this.nivelAcesso = NivelAcesso.Professor;
        }
        public override void CancelarReserva(int idEmprestimo)
        {
            throw new NotImplementedException();
        }

        public override void DevolverLivro(int idEmprestimo)
        {
            throw new NotImplementedException();
        }

        public override void ExibirHistorico(List<Emprestimo> emprestimos)
        {
            throw new NotImplementedException();
        }

        public override void ListarReservas(List<Emprestimo> reservas)
        {
            throw new NotImplementedException();
        }

        public override int LocalizarReserva(string? nomeLivro, int? idLivro)
        {
            throw new NotImplementedException();
        }

        public override void VerificarDisponibilidade()
        {
            throw new NotImplementedException();
        }

        public void SolicitarLivro(int idLivro)
        {
            throw new NotImplementedException();
        }
    }
}
