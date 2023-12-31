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

        public override void CancelarReserva(int idEmprestimo)
        {
            this.ListarReservas().Find(x => x.idEmprestimo == idEmprestimo).estadoEmprestimo = EstadoEmprestimo.Cancelado;
        }

        public override void DevolverLivro(int idEmprestimo, DateTime dataDevolucao)
        {
            Emprestimo emprestimo = Emprestimo.Find(x => x.idEmprestimo == idEmprestimo);
            if (emprestimo.dataLimite > dataDevolucao)
            {
                emprestimo.estadoEmprestimo = EstadoEmprestimo.Finalizado;
            }
            else
            {
                emprestimo.estadoEmprestimo = EstadoEmprestimo.FinalizadoComMulta;
                this.multaTotal += SistemaBiblioteca.CalcularMulta(emprestimo.dataLimite, dataDevolucao);
            }
        }

        public override List<Emprestimo> ExibirHistorico(List<Emprestimo> emprestimos)
        {
            return emprestimos.FindAll(x => x.idUsuario == this.idProfessor);
        }

        public override List<Emprestimo> ListarReservas(List<Emprestimo> reservas)
        {
            return reservas.FindAll(x => x.idUsuario == this.idProfessor);
        }

        public override int LocalizarReserva(string? nomeLivro, int? idLivro)
        {
            if (!string.IsNullOrEmpty(nomeLivro))
            {
                return ControleDeReservas.ConsultarPorNomeDoLivro(nomeLivro);
            }
            else if (idLivro.HasValue)
            {
                return ControleDeReservas.ConsultarPorIdDoLivro(idLivro.Value);
            }
            else
            {
                throw new ArgumentException("É necessário fornecer o nome do livro ou o ID do livro para realizar a busca.");
            }
        }


        public override bool VerificarDisponibilidade(Livro livro)
        {
            return livro.estadoLivro == EstadoLivro.Disponivel;
        }

        public void SolicitarLivro(int idLivro)
        {
            Livro livro = Livro.Consultar(idLivro);
            if (livro.estadoLivro == EstadoLivro.Disponivel)
            {
                livro.estadoLivro = EstadoLivro.AguardandoAprovacao;
                livro.requerente = this.idProfessor;
            }
        }
    }
}
