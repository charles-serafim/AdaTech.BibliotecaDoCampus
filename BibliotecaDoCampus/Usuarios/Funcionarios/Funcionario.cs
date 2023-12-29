using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Funcionarios
{
    public class Funcionario : Usuario
    {
        string codigoDeAcesso;
        string senha;
        public string nome;
        private NivelAcesso acesso;

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

        public void ReservarLivro(Usuario usuario, Livro livro, DateTime dataInicio, DateTime dataFim)
        {
            throw new NotImplementedException();
        }

        public void AtualizarExemplar(Livro livro, EstadoLivro estadoLivro, Acervo acervo)
        {
            throw new NotImplementedException();
        }

        public void CadastrarLivro(Livro livro, Acervo acervo)
        {
            throw new NotImplementedException();
        }
    }
}
