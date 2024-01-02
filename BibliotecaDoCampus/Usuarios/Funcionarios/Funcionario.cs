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
            return livro.estadoLivro == EstadoLivro.Disponivel;
        }

        public override int LocalizarReserva(string? nomeLivro, int? idLivro)
        {
            return ControleDeReservas.Consultar(nomeLivro, idLivro);
        }

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
            return emprestimos = emprestimos.FindAll(x => x.idUsuario == this.matricula);
        }

        public override List<Emprestimo> ListarReservasUsuario(List<Emprestimo> reservas, Usuario u)
        {
            return reservas = reservas.FindAll(x => x.idUsuario == u.codigoDeAcesso);
        }

        public override List<Emprestimo> ExibirHistoricoUsuario(List<Emprestimo> emprestimos, Usuario u)
        {
            return emprestimos = emprestimos.FindAll(x => x.idUsuario == u.codigoDeAcesso);
        }

        public override List<Emprestimo> ListarReservas(List<Emprestimo> reservas)
        {
            return reservas = reservas.FindAll(x => x.idUsuario == this.matricula);
        }

    }
}
