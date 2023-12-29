namespace SistemaGerenciamento;

internal class Program
{
    static List<Usuario> listaDeUsuarios = new List<Usuario>()
        {
            new Usuario("Charles Serafim", "c.s@mail.com", NivelAcesso.Estudante),
            new Usuario("Luana Ritter", "l.r@mail.com", NivelAcesso.Professor),
            new Usuario("Monalisa Brito", "m.b@mail.com", NivelAcesso.Diretor)
        };

    static List<Livro> listaDeLivros = new List<Livro>()
        {
            new Livro( "Compiladores: Princípios, Técnicas e Ferramentas", "Alfred V. Aho, Monica S. Lam, Ravi Sethi, Jeffrey D. Ullman", 1, Acervo.AcervoRestrito, EstadoLivro.Disponível ),
            new Livro( "O Hobbit", "J. R. R. Tolkien", 3, Acervo.AcervoPublico, EstadoLivro.Reservado ),
            new Livro( "Domain-driven design: atacando as complexidades no coração do software", "Eric Evans", 3, Acervo.AcervoPublico, EstadoLivro.Emprestado ),
            new Livro( "A Origem das Espécies", "Charles Darwin", 1, Acervo.ForaDeEstoque, EstadoLivro.Danificado ),
            new Livro( "Cem anos de solidão", "Gabriel García Márquez", 129, Acervo.ForaDeEstoque, EstadoLivro.Perdido )
        };

    static void Main(string[] args)
    {
        Console.WriteLine("Sistema de Biblioteca do Campus");
        int option;
        bool repeat = true;

        while(repeat)
        {
            Console.Clear();
            Console.WriteLine("1 - Cadastrar usuário\n" +
                              "2 - Listar usuários\n" +
                              "3 - Cadastrar livro\n" +
                              "4 - Listar livros\n" +
                              "0 - Sair\n" +
                              "Digite uma opção ou 0 para sair: ");

            option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 0:
                    repeat = false;
                    Console.Clear();
                    Console.WriteLine("Saindo...\n");
                    break;

                case 1:
                    Console.Clear();
                    CadastrarUsuario();
                    break;

                case 2:
                    Console.Clear();
                    MostrarUsuarios();
                    break;

                case 3:
                    Console.Clear();
                    CadastrarLivro();
                    break;

                case 4:
                    Console.Clear();
                    MostrarLivros();
                    break;

                default:
                    break;

            }
        }        
    }

    static void MostrarUsuarios()
    {
        foreach (var usuario in listaDeUsuarios)
        {
            usuario.MostrarDados();
        }
        Console.WriteLine("Continuar...");
        Console.ReadLine();
    }

    static void MostrarLivros()
    {
        foreach (var livro in listaDeLivros)
        {
            livro.MostrarDados();
        }
        Console.WriteLine("Continuar...");
        Console.ReadLine();
    }

    static void CadastrarUsuario()
    {
        string nome, email;
        NivelAcesso nivelAcesso;

        Console.WriteLine("Nome: ");
        nome = Console.ReadLine();
        
        Console.WriteLine("\nE-mail: ");
        email = Console.ReadLine();

        Console.WriteLine("\nNível de acesso:\n" +
                         $"{((int)NivelAcesso.Estudante)} - {NivelAcesso.Estudante}\n" +
                         $"{((int)NivelAcesso.Professor)} - {NivelAcesso.Professor}\n" +
                         $"{((int)NivelAcesso.Atendente)} - {NivelAcesso.Atendente}\n" +
                         $"{((int)NivelAcesso.Diretor)} - {NivelAcesso.Diretor}\n" +
                          "Digite o código referente ao tipo do usuário: ");
        while (!Enum.TryParse(Console.ReadLine(), out nivelAcesso))
        {
            Console.WriteLine("Código inválido. Digite novamente o código referente ao tipo do usuário: ");
        }

        Usuario novoUsuario = new Usuario(nome, email, nivelAcesso);

        listaDeUsuarios.Add(novoUsuario);
    }
    static void CadastrarLivro()
    {
        string titulo, autores;
        int edicao;
        Acervo acervo;
        EstadoLivro estadoLivro;

        Console.WriteLine("Título do livro: ");
        titulo = Console.ReadLine();

        Console.WriteLine("\nAutor (separe os autores com vírgula): ");
        autores = Console.ReadLine();
        
        Console.WriteLine("\nEdição: ");
        while (!int.TryParse(Console.ReadLine(), out edicao))
        {
            Console.WriteLine("Por favor, digite um número");
        }

        Console.WriteLine("\nAcervos:");
        foreach(Acervo tipoAcervo in Enum.GetValues(typeof(Acervo))) Console.WriteLine($"{(int)tipoAcervo} - {tipoAcervo}");
        Console.WriteLine("Digite o código referente ao acervo: ");
        while (!Enum.TryParse(Console.ReadLine(), out acervo))
        {
            Console.WriteLine("Código inválido. Digite novamente o código referente ao tipo do acervo: ");
        }

        Console.WriteLine("\nCondição do livro:");
        foreach (EstadoLivro condicaoLivro in Enum.GetValues(typeof(EstadoLivro))) Console.WriteLine($"{(int)condicaoLivro} - {condicaoLivro}");
        Console.WriteLine("Digite o código referente à condição atual do livro: ");
        while (!Enum.TryParse(Console.ReadLine(), out estadoLivro))
        {
            Console.WriteLine("Código inválido. Digite novamente o código referente à condição atual do livro: ");
        }

        Livro novoLivro = new Livro(titulo, autores, edicao, acervo, estadoLivro);

        listaDeLivros.Add(novoLivro);
    }
}
