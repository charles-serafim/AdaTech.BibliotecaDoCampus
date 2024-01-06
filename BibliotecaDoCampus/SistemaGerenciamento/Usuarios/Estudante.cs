using SistemaGerenciamento.Models;

namespace SistemaGerenciamento.Usuarios
{
    public class Estudante : Usuario
    {
        int matricula;
        public Estudante()
        {
            nivelAcesso = NivelAcesso.Estudante;
        }

        public override bool ReservarLivro(int idLivro)
        {
            int idUsuario = matricula;
            return Program.ReservarLivro(idLivro, idUsuario);
        }
        public override bool CancelarReserva(int idLivro)
        {
            return Program.CancelarReserva(idLivro, matricula);
        }
        public override void DevolverLivro(int idLivro)
        {
            Program.DevolverLivro(idLivro, matricula, null);
        }
        public override List<Emprestimo> ExibirHistorico()
        {
            return Program.ExibirHistoricoDoUsuario();
        }
        public override List<Emprestimo> ListarReservas(List<Emprestimo> reservas)
        {
            return reservas = reservas.FindAll(x => x.idUsuario == matricula);
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
            if (Program.ObterLivro(idLivro) != null)
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
            Livro livro = Program.ObterLivro(idLivro);
            if (livro._estadoLivro == EstadoLivro.Disponivel)
            {
                livro._estadoLivro = EstadoLivro.AguardandoAprovacao;
                livro.solicitante = matricula;
            }
        }
        public Estudante SolicitarAlteracaoCadastro(string? nome, string? sobrenome, string? email)
        {
            Estudante estudante = new Estudante();
            if (nome != null) estudante.nome = nome;
            else estudante.nome = this.nome;
            if (sobrenome != null) estudante.sobrenome = sobrenome;
            else estudante.sobrenome = this.sobrenome;
            if (email != null) estudante.email = email;
            else estudante.email = this.email;
            estudante.matricula = matricula;
            estudante.debitoTotal = debitoTotal;
            estudante.nivelAcesso = nivelAcesso;

            return estudante;
        }
        public override double ConsultarDebitos()
        {
            return debitoTotal;
        }
    }
}
