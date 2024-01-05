using SistemaGerenciamento;
using SistemaGerenciamento.Models;
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

        public override bool ReservarLivro(int idLivro)
        {
            int idUsuario = matricula;
            return Program.ReservarLivro(idLivro, idUsuario);
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
        public override List<Emprestimo> ExibirHistorico()
        {
            return Program.ExibirHistoricoDoUsuario();
        }
        public override List<Emprestimo> ListarReservas(List<Emprestimo> reservas)
        {
            return reservas = reservas.FindAll(x => x.idUsuario == this.matricula);
        }
        public override List<Livro> ListarLivros()
        {
            return Program.ListarLivros().FindAll(x => x._acervo == Acervo.AcervoPublico);
        }
        public override int LocalizarReserva(string? nomeLivro, int? idLivro)
        {
            return ControleDeReservas.Consultar(nomeLivro, idLivro);
        }
        public override bool VerificarDisponibilidade(int idLivro)
        {
            if(Program.ObterLivro(idLivro) != null)
            {
                Livro livro = Program.ObterLivro(idLivro);
                if (livro._acervo == Acervo.AcervoPublico)
                {
                    return Program.VerificarDisponibilidade(idLivro);
                }
                return false;
            }
            return false;
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

        public override double ConsultarDebitos()
        {
            return this.debitoTotal;
        }
    }
}
