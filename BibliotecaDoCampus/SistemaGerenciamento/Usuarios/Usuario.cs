using SistemaGerenciamento.Models;

namespace SistemaGerenciamento.Usuarios
{
    public abstract class Usuario
    {
        public string nome;
        public string sobrenome;
        public string email;
        public double debitoTotal;
        public NivelAcesso nivelAcesso;
        public int? codigoDeAcesso;
        public abstract double ConsultarDebitos();
        public abstract bool VerificarDisponibilidade(int idLivro);
        public abstract List<Emprestimo> ExibirHistorico();
        public abstract List<Emprestimo> ListarReservas(List<Emprestimo> reservas);
        public abstract List<Livro> ListarLivros();
        public abstract bool ReservarLivro(int idLivro);
        public abstract int LocalizarReserva(string? nomeLivro, int? idLivro);
        public abstract bool CancelarReserva(int idLivro);
        public abstract void DevolverLivro(int idLivro);
    }
}
