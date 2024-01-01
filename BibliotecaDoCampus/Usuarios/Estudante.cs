using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios
{
    internal class Estudante : Usuario
    {
        int matricula;
        public Estudante()
        {
            this.nivelAcesso = NivelAcesso.Estudante;
        }
        public override void CancelarReserva(int idEmprestimo)
        {
            this.ListarReservas().Find(x => x.idEmprestimo == idEmprestimo).estadoEmprestimo = EstadoEmprestimo.Cancelado;

        }

        public override void DevolverLivro(int idEmprestimo, DateTime dataDevolucao)
        {
            Emprestimo emprestimo = Emprestimo.Find(x => x.idEmprestimo == idEmprestimo);
            if(emprestimo.dataLimite > dataDevolucao)
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
            return emprestimos=emprestimos.FindAll(x => x.idUsuario == this.matricula);
        }

        public override List<Emprestimo> ListarReservas(List<Emprestimo> reservas)
        {
            return reservas = reservas.FindAll(x => x.idUsuario == this.matricula);
        }

        public override int LocalizarReserva(string? nomeLivro, int? idLivro)
        {
            return ControleDeReservas.Consultar(string? nomeLivro int? idLivro);
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
                livro.requerente = this.matricula;
            }
        }
    }
}
