// Luana:
// ListarLivros(): List<Livro>
// ListarLivrosPorSetor(setor:string) :List<Livro>
// ListarLivrosDisponiveis(): List<Livro>
// ListarLivrosEmprestados(): List<Livro>

// Monalisa:
// AutenticarUsuario(codigoAcesso:string, nivelAcesso:NivelAcesso, senha:string) : Usuario OK

// Inicializar()

// AtualizarExemplar(livro:Livro, estado:Estado, acervo:Acervo) OK
// VerificarDisponibilidade(codigoLivro:int): Livro -
// VerificarHistoricoLivro(idLivro:int)

// Charles:
// SolicitarLivro(idLivro:int)
// RegistrarListaDeEspera(idLivro:int, usuario:Usuario)
// AutorizarEmprestimo(): Emprestimo
// DevolverLivro() // 
// ReservarLivro(): Emprestimo
// CancelarReserva(codigoAcesso:string, codigoLivro:int)

using SistemaGerenciamento.Models;

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

    static List<Reserva> listaDeEspera = new List<Reserva>();
    static List<Emprestimo> historico = new List<Emprestimo>();

    static void Main(string[] args)
    {
        Console.WriteLine("Sistema de Biblioteca do Campus");
        int option;
        bool mainLoop = true;
        bool repeat = true;

        while(mainLoop)
        {
            Console.Clear();
            Console.WriteLine("1 - Cadastrar usuário\n" +
                              "2 - Listar usuários\n" +
                              "3 - Cadastrar livro\n" +
                              "4 - Listar livros\n" +
                              "5 - Devolver um livro\n" +
                              "0 - Sair\n" +
                              "Digite uma opção ou 0 para sair: ");

            option = Utils.ReadOption(0, 4);

            switch(option)
            {
                case 0:
                    mainLoop = false;
                    Console.Clear();
                    Console.WriteLine("Saindo...\n");
                    Utils.GoOn();
                    break;

                case 1:
                    Console.Clear();
                    CadastrarUsuario();
                    break;

                case 2:
                    Console.Clear();
                    MostrarDados(listaDeUsuarios);
                    break;

                case 3:
                    Console.Clear();
                    CadastrarLivro();
                    break;

                case 4:
                    Console.Clear();
                    MostrarDados(listaDeLivros);
                    break;

                case 5:
                    while (repeat)
                    {
                        Console.Clear();
                        Usuario cliente = LocalizarUsuario();
                        if (cliente == null)
                        {
                            Console.WriteLine("Usuário não localizado.");
                            repeat = Utils.ReadYesOrNo("Realizar nova busca");
                            continue;
                        }
                        while (repeat)
                        {
                            Emprestimo emprestimoLocalizado = LocalizarEmprestimo(cliente);
                            if (emprestimoLocalizado == null)
                            {
                                Console.WriteLine("Usuário não localizado.");
                                repeat = Utils.ReadYesOrNo("Realizar nova busca");
                                continue;
                            }
                            DevolverLivro(emprestimoLocalizado);
                        }
                    }
                    
                    break;

                default:
                    break;

            }
        }        
    }

    static void MostrarDados<T>(List<T> listaDeElementos)
    {
        foreach (var elemento in listaDeElementos)
        {
            dynamic objeto = elemento;
            objeto.MostrarDados();
        }
        Utils.GoOn();
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

    static void DevolverLivro(Emprestimo emprestimo)
    {
        DateTime dataDevolucao = DateTime.Today;
        
    }

    static Usuario LocalizarUsuario()
    {
        string nome;
        int idUsuario;
        Usuario usuarioLocalizado;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Digite o nome ou o ID do usuário: ");
            nome = Console.ReadLine();
            
            if(int.TryParse(nome, out idUsuario))
            {
                usuarioLocalizado = listaDeUsuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
                return usuarioLocalizado;
            }

            usuarioLocalizado = listaDeUsuarios.FirstOrDefault(u => u._nome == nome);
        }
        return usuarioLocalizado;
    }

    static Emprestimo LocalizarEmprestimo(Usuario usuario)
    {
        string tituloLivro;
        int idEmprestimo;
        Emprestimo emprestimoLocalizado;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Digite o título do livro ou o ID do empréstimo: ");
            tituloLivro = Console.ReadLine();

            if (int.TryParse(tituloLivro, out idEmprestimo))
            {
                emprestimoLocalizado = historico.FirstOrDefault(e => e._idUsuario == usuario.IdUsuario && e.IdReserva == idEmprestimo);
                return emprestimoLocalizado;
            }

            // guardar Livro em uma outra variavel pra procurar na lista de livros

            emprestimoLocalizado = historico.FirstOrDefault(e => e._idUsuario == usuario.IdUsuario  && true);
        }
        return emprestimoLocalizado;
    }

    static void AtualizarExemplar(Usuario usuario, int IdLivro, EstadoLivro estadoLivro, Acervo acervo) 
    {
        Console.WriteLine("Informe o IdLivro do livro que deseja atualizar: ");
        int idLivro = int.Parse(Console.ReadLine());

        if (idLivro <= 0)
        {
            throw new Exception("IdLivro inválido!");
        }

        Console.WriteLine("Informe o estado do livro: ");
        EstadoLivro estadoLivro = (EstadoLivro)Enum.Parse(typeof(EstadoLivro), Console.ReadLine());

        if (!Enum.IsDefined(typeof(EstadoLivro), estadoLivro))
        {
            throw new Exception("Estado inválido!");
        }

        Console.WriteLine("Informe o tipo de acervo: ");
        Acervo acervo = (Acervo)Enum.Parse(typeof(Acervo), Console.ReadLine());

        if (!Enum.IsDefined(typeof(Acervo), acervo))
        {
            throw new Exception("Acervo inválido!");
        }

        //Livro livro = MostrarDados(listaDeLivros).FindById(IdLivro); *conferir

        Livro._estadoLivro = estadoLivro;
        Livro._acervo = acervo;

        // MostrarDados(listaDeLivros).Save(livro) *conferir
    }

    static void VerificarDisponibilidades(int Idlivro, EstadoLivro estadoLivro, Acervo acervo)
    {
        Console.WriteLine("Qual o Id do livro a ser verificado a disponibilidade? ")
            int idLivro = int.Parse(Console.ReadLine());
        if (idLivro <= 0)
        {
            throw new Exception("IdLivro inválido!");
        }

        Livro livro = MostrarDados(listaDeLivros).FindById(idLivro);

        if (livro == null)
        {
            throw new Exception("Livro não encontrado!");
        }

        if(livro._acervo == Acervo.AcervoPublico  && livro._estadoLivro == EstadoLivro.Disponível)
        {
            return "Livro Disponível!"
        }
        else
        {
            return "Livro Indisponível!"
        }
    }
}