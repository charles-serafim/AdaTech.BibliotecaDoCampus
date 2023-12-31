using SistemaGerenciamento;
using SistemaGerenciamento.Models;



// 
// TESTAR!!!
//

// Luana:
// ListarLivros(): List<Livro>
// ListarLivrosPorSetor(setor:string) :List<Livro> // R: Por setor você quer dizer Acervo?
// ListarLivrosDisponiveis(): List<Livro>
// ListarLivrosEmprestados(): List<Livro>


public class Listagem
{
    private static List<Livro> livros = new List<Livro>();

    public Listagem()
    {
        // Leitura de Json aqui no futuro????

    }

    public static List<Livro> ListarLivros(Func<Livro, bool> predicate) =>
        livros.Where(predicate).ToList();
    public static List<Livro> ListarTodosLivros() =>
        ListarLivros(livro => true);

    internal static List<Livro> ListarLivrosPorSetor(Acervo setor) =>
        ListarLivros(livro => livro._acervo == setor);

    internal static List<Livro> ListarLivrosPorEstado(EstadoLivro estado) =>
        ListarLivros(livro => livro._estadoLivro == estado);
}
