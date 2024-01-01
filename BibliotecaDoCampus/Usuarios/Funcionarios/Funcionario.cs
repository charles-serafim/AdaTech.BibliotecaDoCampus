using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Funcionarios
{
    public class Funcionario : Usuario
    {
        public string codigoDeAcesso;
        public string senha;
        public string nome;
        public NivelAcesso acesso;

        public override bool VerificarDisponibilidade(Livro livro)
        {
            throw new NotImplementedException();
        }

        public override List<Emprestimo> ExibirHistorico(List<Emprestimo> emprestimos)
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

        public override void CancelarReserva(int idEmprestimo)
        {
            throw new NotImplementedException();
        }

        public override void DevolverLivro(int idEmprestimo, DateTime dataDevolucao)
        {
            throw new NotImplementedException();
        }
    }
}
