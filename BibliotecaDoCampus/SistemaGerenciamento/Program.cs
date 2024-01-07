using SistemaGerenciamento.JsonParser;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Usuarios;
using NivelAcesso = SistemaGerenciamento.Usuarios.NivelAcesso;

namespace SistemaGerenciamento;

public static class Program
{
    public static List<Livro> listaDeLivros = new List<Livro>();

    static List<Usuario> listaDeUsuarios = new List<Usuario>();
    
    static List<Tuple<Usuario, Usuario>> solicitacoesAlteracaoCadastro = new List<Tuple<Usuario, Usuario>>();

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
    public static bool VerificarDisponibilidade(string dadoLivro)
    {
        Livro? livro = ObterLivro(dadoLivro);
        return livro?._estadoLivro == EstadoLivro.Disponivel;
    }    
    public static bool ReservarLivro(string dadoLivro, int idUsuario) // retorna se houve sucesso; implementar regras de adição de acordo com a prioridade
    {
        Livro? livro = ObterLivro(dadoLivro);
        Usuario? usuario = ObterUsuario(idUsuario);

        if (usuario.nivelAcesso == NivelAcesso.Atendente || usuario.nivelAcesso == NivelAcesso.Diretor) return false;

        if (usuario.nivelAcesso == NivelAcesso.Estudante)
        {
            livro._filaDeEspera.Add(usuario);
            return true;
        }

        for (int i = 0; i < livro._filaDeEspera.Count; i++)
        {
            if (livro._filaDeEspera[i + 1].nivelAcesso == NivelAcesso.Estudante)
            {
                livro._filaDeEspera.Insert(i, usuario);
                return true;
            }
        }

        livro._filaDeEspera.Add(usuario);
        return true;
    }
    public static bool DevolverLivro(string dadoLivro, int? idUsuario, EstadoLivro? novoEstadoLivro) // se for o atendente que está logado, ele pode realizar a devolução de um emprestimo de um outro usuario, se for o proprio usuario, ele não precisa utilizar a variavel idUsuario
    {
        Livro livro;
        Usuario usuario;
        Emprestimo emprestimo;
        DateTime dataDevolucao;
        double multa;
        
        if (usuarioLogado.nivelAcesso != NivelAcesso.Atendente) usuario = usuarioLogado;
        else usuario = ObterUsuario((int)idUsuario);
        livro = ObterLivro(dadoLivro);
        emprestimo = ObterEmprestimo(livro.IdLivro, (int)idUsuario);

        if (usuario == null || emprestimo == null || livro == null) return false;

        dataDevolucao = DateTime.Now;
        multa = emprestimo.DevolverLivro(dataDevolucao, usuario);
        usuario.debitoTotal += multa;
        if(novoEstadoLivro == null) novoEstadoLivro = EstadoLivro.Disponivel;
        else livro.DevolverLivro((EstadoLivro)novoEstadoLivro);

        return true;
    }
    public static bool CancelarReserva(string dadoLivro, int idUsuario) // recebe livro e usuario para localizar a reserva
    {
        Livro? livro = ObterLivro(dadoLivro);

        if(livro != null)
        {
            int indiceReserva = livro._filaDeEspera.FindIndex(usuario => usuario.codigoDeAcesso == idUsuario);

            if(indiceReserva != -1)
            {
                livro._filaDeEspera.RemoveAt(indiceReserva);
                return true;
            }
        }

        return false;
    }
    public static List<Emprestimo> ExibirHistorico()
    {
        return historicoDeEmprestimos;
    }
    public static List<Emprestimo> ExibirHistoricoDoUsuario()
    {
        return historicoDeEmprestimos.FindAll(x => x._idUsuario == usuarioLogado.codigoDeAcesso);
    }
    void ListarReservasDoLivro(string dadoLivro); // exibe a fila de espera para um livro    
    void ListarReservasDoUsuario(); // exibe todas as reservas do usuarioLogado        
    public static void SolicitarAlteracaoCadastro(Usuario usuario, Usuario alteracao)
    {
        solicitacoesAlteracaoCadastro.Add(Tuple.Create(usuario, alteracao));
    } // produz uma instancia de usuario com as informações novas e adiciona à lista solicitacoesAlteracaoCadastro a serem analisadas pelos atendentes    
    bool AnalisarPedidosDeAlteracao(); // exibe o conteudo de solicitacoesAlteracaoCadastro (se houver) para análise e aprovação do atendente e chama AlterarCadastro()    
    bool AlterarCadastro(); // substitui na listaDeUsuarios a instancia original de um usuario pela em solicitacoesAlteracaoCadastro, após a aprovação da alteração de seu cadastro
    public static Livro? ObterLivro(string dadoLivro)
    {
        if (int.TryParse(dadoLivro, out int idLivro)) return listaDeLivros.FirstOrDefault(livro => livro.IdLivro == idLivro);

        return listaDeLivros.FirstOrDefault(livro => livro._titulo.Contains(dadoLivro));
    }
    public static Usuario? ObterUsuario(int idUsuario)
    {
        if (listaDeUsuarios.Find(x => x.codigoDeAcesso == idUsuario) != null)
        {
            return listaDeUsuarios.Find(x => x.codigoDeAcesso == idUsuario);
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