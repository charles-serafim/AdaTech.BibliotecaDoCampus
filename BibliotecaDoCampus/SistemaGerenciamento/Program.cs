using SistemaGerenciamento.Models;

namespace SistemaGerenciamento;

internal class Program
{
    static List<Livro> listaDeLivros = new List<Livro>();

    static List<Usuario> listaDeUsuarios = new List<Usuario>();

    static List<Usuario> solicitacoesAlteracaoCadastro = new List<Usuario>();

    static List<Emprestimo> historicoDeEmprestimos = new List<Emprestimo>();

    Usuario usuarioLogado;

    void Inicializar(); // realiza o carregamento do conteúdo dos arquivos JSON para as listas locais da classe

    void SalvarTudo(); // realiza o salvamento do conteúdo das listas locais no arquivo JSON, viabilizando a persistência dos dados gerados e modificações

    void ListarLivros(); // exibe livros aplicando o filtro do acervo de acordo com usuarioLogado

    bool VerificarDisponibilidade(int idLivro); // retorna a disponibilidade do livro

    bool ReservarLivro(int idLivro); // retorna se houve sucesso; implementar regras de adição de acordo com a prioridade
    
    bool DevolverLivro(int idLivro, int? idUsuario); // se for o atendente que está logado, ele pode realizar a devolução de um emprestimo de um outro usuario, se for o proprio usuario, ele não precisa utilizar a variavel idUsuario
    
    bool CancelarReserva(int idLivro, int idUsuario); // recebe livro e usuario para localizar a reserva
    
    void ExibirHistorico(); // retorna o historico completo de emprestimos armazenados

    void ExibirHistoricoDoUsuario(); // retorna o historico filtrando pelo usuarioLogado
    
    void ListarReservasDoLivro(int idLivro); // exibe a fila de espera para um livro
    
    void ListarReservasDoUsuario(); // exibe todas as reservas do usuarioLogado
    
    bool SolicitarAlteracaoCadastro(); // produz uma instancia de usuario com as informações novas e adiciona à lista solicitacoesAlteracaoCadastro a serem analisadas pelos atendentes

    bool AnalisarPedidosDeAteracao(); // exibe o conteudo de solicitacoesAlteracaoCadastro (se houver) para análise e aprovação do atendente e chama AlterarCadastro()

    bool AlterarCadastro(); // substitui na listaDeUsuarios a instancia original de um usuario pela em solicitacoesAlteracaoCadastro, após a aprovação da alteração de seu cadastro
}