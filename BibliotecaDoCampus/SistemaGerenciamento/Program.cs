using SistemaGerenciamento.JsonParser;
using SistemaGerenciamento.Models;



namespace SistemaGerenciamento;

public class Program
{
    public static List<Livro> listaDeLivros = new List<Livro>();

    static List<Usuario> listaDeUsuarios = new List<Usuario>();

    static List<Usuario> solicitacoesAlteracaoCadastro = new List<Usuario>();

    static List<Emprestimo> historicoDeEmprestimos = new List<Emprestimo>();

    static Usuario? usuarioLogado;

    void Inicializar()
    {
        listaDeLivros = Listagem.GetLivros();
        listaDeUsuarios = Listagem.GetUsuarios();
        historicoDeEmprestimos = Listagem.GetEmprestimos();
        solicitacoesAlteracaoCadastro = Listagem.GetSolicitacoesDeAlteracaoCadastro();
    } // realiza o carregamento do conteúdo dos arquivos JSON para as listas locais da classe

    void SalvarTudo()
    {
        JsonParser<Livro>.SalvarLivros(listaDeLivros);
        JsonParser<Usuario>.SalvarUsuarios(listaDeUsuarios);
        JsonParser<Emprestimo>.SalvarEmprestimos(historicoDeEmprestimos);
        JsonParser<Usuario>.SalvarUsuarios(solicitacoesAlteracaoCadastro);
    } // realiza o salvamento do conteúdo das listas locais no arquivo JSON, viabilizando a persistência dos dados gerados e modificações

    public static List<Livro> ListarLivros(); // exibe livros aplicando o filtro do acervo de acordo com usuarioLogado

    public static bool VerificarDisponibilidade(int idLivro)
    {
        Livro? livro = ObterLivro(idLivro);
        return livro?._estadoLivro == EstadoLivro.Disponível;
    }

    public static bool ReservarLivro(int idLivro, int idUsuario) // retorna se houve sucesso; implementar regras de adição de acordo com a prioridade
    {
        Livro? livro = ObterLivro(idLivro);
        Usuario? usuario = ObterUsuario(idUsuario);

        if (usuario._nivelAcesso == NivelAcesso.Atendente || usuario._nivelAcesso == NivelAcesso.Diretor) return false;

        if (usuario._nivelAcesso == NivelAcesso.Estudante)
        {
            livro._filaDeEspera.Add(usuario);
            return true;
        }

        for (int i = 0; i < livro._filaDeEspera.Count; i++)
        {
            if (livro._filaDeEspera[i + 1]._nivelAcesso == NivelAcesso.Estudante)
            {
                livro._filaDeEspera.Insert(i, usuario);
                return true;
            }
        }

        livro._filaDeEspera.Add(usuario);
        return true;
    }
    
    public static bool DevolverLivro(int idLivro, int? idUsuario, EstadoLivro? novoEstadoLivro) // se for o atendente que está logado, ele pode realizar a devolução de um emprestimo de um outro usuario, se for o proprio usuario, ele não precisa utilizar a variavel idUsuario
    {
        Livro livro;
        Usuario usuario;
        Emprestimo emprestimo;
        DateTime dataDevolucao;
        double multa;
        
        if (usuarioLogado._nivelAcesso != NivelAcesso.Atendente) usuario = usuarioLogado;
        else usuario = ObterUsuario((int)idLivro);
        emprestimo = ObterEmprestimo(idLivro, (int)idUsuario);
        livro = ObterLivro(idLivro);

        if (usuario == null || emprestimo == null || livro == null) return false;

        dataDevolucao = DateTime.Now;
        multa = emprestimo.DevolverLivro(dataDevolucao, usuario);
        usuario._multaTotal += multa;
        if(novoEstadoLivro == null) novoEstadoLivro = EstadoLivro.Disponivel;
        else livro.DevolverLivro((EstadoLivro)novoEstadoLivro);

        return true;
    }

    public static bool CancelarReserva(int idLivro, int idUsuario) // recebe livro e usuario para localizar a reserva
    {
        Livro? livro = ObterLivro(idLivro);

        if(livro != null)
        {
            int indiceReserva = livro._filaDeEspera.FindIndex(usuario => usuario.IdUsuario == idUsuario);

            if(indiceReserva != -1)
            {
                livro._filaDeEspera.RemoveAt(indiceReserva);
                return true;
            }
        }

        return false;
    }

    public List<Emprestimo> ExibirHistorico()
    {
        return historicoDeEmprestimos;
    }

    public static List<Emprestimo> ExibirHistoricoDoUsuario()
    {
        return historicoDeEmprestimos.FindAll(x => x._idUsuario == usuarioLogado.IdUsuario);
    }
    
    void ListarReservasDoLivro(int idLivro); // exibe a fila de espera para um livro
    
    void ListarReservasDoUsuario(); // exibe todas as reservas do usuarioLogado
    
    bool SolicitarAlteracaoCadastro(); // produz uma instancia de usuario com as informações novas e adiciona à lista solicitacoesAlteracaoCadastro a serem analisadas pelos atendentes

    bool AnalisarPedidosDeAteracao(); // exibe o conteudo de solicitacoesAlteracaoCadastro (se houver) para análise e aprovação do atendente e chama AlterarCadastro()

    bool AlterarCadastro(); // substitui na listaDeUsuarios a instancia original de um usuario pela em solicitacoesAlteracaoCadastro, após a aprovação da alteração de seu cadastro

    public static Livro? ObterLivro(int idLivro)
    {
        if (listaDeLivros.Find(x => x.IdLivro == idLivro) != null)
        {
            return listaDeLivros.Find(x => x.IdLivro == idLivro);
        }
        else
        {
            return null;
        }
    }
    
    public static Usuario? ObterUsuario(int idUsuario)
    {
        if (listaDeUsuarios.Find(x => x.IdUsuario == idUsuario) != null)
        {
            return listaDeUsuarios.Find(x => x.IdUsuario == idUsuario);
        }
        else
        {
            return null;
        }
    }
    public static Emprestimo? ObterEmprestimo(int idLivro, int idUsuario)
    {
        if (historicoDeEmprestimos.Find(x => x._idLivro == idLivro && x._idUsuario == idUsuario) != null )
        {
            return historicoDeEmprestimos.Find(x => x._idLivro == idLivro && x._idUsuario == idUsuario);
        }
        else
        {
            return null;
        }
    }
}