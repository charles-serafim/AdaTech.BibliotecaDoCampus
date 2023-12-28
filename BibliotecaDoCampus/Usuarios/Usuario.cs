namespace Usuarios
{
    public abstract class Usuario
    {
        string nome;
        string sobrenome;
        string email;
        double multaTotal;
        NivelAcesso nivelAcesso;


        public abstract void VerificarDisponibilidade();
        public abstract void ExibirHistorico(List<Emprestimo> emprestimos);
        public abstract void ListarReservas(List<Emprestimo> reservas);
        public abstract int LocalizarReserva(string? nomeLivro, int? idLivro);
        public abstract void CancelarReserva(int idEmprestimo);
        public abstract void DevolverLivro(int idEmprestimo);
    }
}
