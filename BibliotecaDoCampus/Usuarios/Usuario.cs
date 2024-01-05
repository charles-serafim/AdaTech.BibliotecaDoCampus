using SistemaGerenciamento.Models;

namespace Usuarios
{
    public abstract class Usuario
    {
        public string nome;
        public string sobrenome;
        public string email;
        public double debitoTotal;
        public NivelAcesso nivelAcesso;
        public string? codigoDeAcesso;
        public abstract double ConsultarDebitos();
        public abstract bool VerificarDisponibilidade(int idLivro);
        public abstract List<Emprestimo> ExibirHistorico();
        public abstract List<Emprestimo> ListarReservas(List<Emprestimo> reservas);
        public abstract List<Livros> ListarLivros();
        public abstract int LocalizarReserva(string? nomeLivro, int? idLivro);
        public abstract void CancelarReserva(int idEmprestimo);
        public abstract void DevolverLivro(int idEmprestimo, DateTime dataDevolucao);
    }
}
