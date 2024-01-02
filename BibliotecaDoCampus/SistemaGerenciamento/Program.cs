﻿// Luana:
// ListarLivros(): List<Livro>
// ListarLivrosPorSetor(setor:string) :List<Livro>
// ListarLivrosDisponiveis(): List<Livro>
// ListarLivrosEmprestados(): List<Livro>

// Monalisa:
// AutenticarUsuario(codigoAcesso:string, nivelAcesso:NivelAcesso, senha:string) : Usuario
// Inicializar()
// AtualizarExemplar(livro:Livro, estado:Estado, acervo:Acervo)
// VerificarDisponibilidade(codigoLivro:int): Livro
// VerificarHistoricoLivro(idLivro:int)

// Charles:
// SolicitarLivro(idLivro:int, usuario:Usuario): Reserva
// AutorizarEmprestimo(Reserva reserva): Emprestimo
// ReservarLivro(): Emprestimo
// CancelarReserva(codigoAcesso:string, codigoLivro:int)


// Concluído:
// DevolverLivro() // 

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
                              "6 - Solicitar livro\n" +
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
                    Console.Clear();
                    DevolverLivro();
                    break;

                case 6:
                    SolicitarLivro();
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

    static void SolicitarLivro()
    {
        Livro livroEscolhido;
        List<Livro> exemplaresDoLivro = new List<Livro>();
        string titulo;
        bool repeat = true;
        bool realizarReserva;

        // localiza todos os exemplares do livro
        while (repeat)
        {
            Console.WriteLine("Digite o título do livro: ");
            titulo = Console.ReadLine();
            
            foreach(var livro in listaDeLivros)
            {
                if(livro._titulo.Contains(titulo)) exemplaresDoLivro.Add(livro);
            }

            if(listaDeLivros == null)
            {
                Console.WriteLine("Livro não localizado no acervo.");
                repeat = Utils.ReadYesOrNo("Tentar novamente");
            }
            else break;
        }

        // exibe as reservas para cada livro
        while (repeat)
        {
            Console.WriteLine($"Exemplares localizados: {exemplaresDoLivro.Count()}");
            int i = 1;
            foreach(var livro in exemplaresDoLivro)
            {
                Console.WriteLine($"Reservas para o exemplar {i++}");
                var reservasDoLivro = listaDeEspera.Where(reserva => reserva._idLivro == livro.IdLivro).ToList();
                if (reservasDoLivro.Any())
                {
                    foreach (var reserva in reservasDoLivro) reserva.MostrarDados(listaDeUsuarios, listaDeLivros);
                }
                else Console.WriteLine("Não há reservas agendadas para este exemplar.");

                if (livro._estadoLivro == EstadoLivro.Disponível)
                {
                    realizarReserva = Utils.ReadYesOrNo("Realizar reserva");
                    if (realizarReserva) ReservarLivro(livro, reservasDoLivro);
                    else break;
                }
                else
                {
                    Console.WriteLine("Exemplar indisponível no momento.");
                    Utils.GoOn();
                }
            }
        }
    }

    static void ReservarLivro(Livro livro, List<Reserva> reservasDoLivro)
    {
        bool reservaCompativel = false;
        Reserva novaReserva;
        DateTime dataInicio, dataFim;

        Usuario usuarioDaReserva = LocalizarUsuario();

        // recebe um periodo valido para a reserva
        while (true)
        {
            Console.Clear();
            foreach (var reserva in reservasDoLivro) reserva.MostrarDados(listaDeUsuarios, listaDeLivros);
            dataInicio = Utils.ReadDateTime();
            dataFim = Utils.ReadDateTime();

            if (dataInicio >= dataFim) reservaCompativel = false;

            foreach (var reserva in reservasDoLivro)
            {
                if (dataInicio >= reserva._dataInicio && dataInicio <= reserva._dataFim) reservaCompativel = false;
                if (dataFim >= reserva._dataInicio && dataFim <= reserva._dataFim) reservaCompativel = false;
                if (dataInicio <= reserva._dataInicio && dataFim >= reserva._dataFim) reservaCompativel = false;
            }

            if (reservaCompativel)
            {
                novaReserva = new Reserva(usuarioDaReserva.IdUsuario, livro.IdLivro, dataInicio, dataFim);
                listaDeEspera.Add(novaReserva);
                Console.WriteLine("Livro reservado com sucesso!");
                Utils.GoOn();
                novaReserva.MostrarDados(listaDeUsuarios, listaDeLivros);
                break;
            }
            else
            {
                Console.WriteLine("Datas inválidas. Digite novas datas, por favor.");
            }
        }
    }

    static void DevolverLivro()
    {
        bool repeat = true;
        int sair;
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
                
                DateTime dataDevolucao = DateTime.Today;

                EstadoLivro novoEstadoLivro;

                while (repeat)
                {
                    Console.WriteLine("Digite o novo estado do livro (Disponível, Reservado, Emprestado, Danificado, Perdido) ou 0 para sair:");
                    string inputEstadoLivro = Console.ReadLine();

                    if (int.TryParse(inputEstadoLivro, out sair))
                    {
                        repeat = false;
                        break;
                    }

                    if (Enum.TryParse(inputEstadoLivro, out novoEstadoLivro))
                    {
                        Livro livroDevolvido = listaDeLivros.Find(livro => livro.IdLivro == emprestimoLocalizado._idLivro);
                        Usuario usuarioDevolucao = listaDeUsuarios.Find(usuario => usuario.IdUsuario == emprestimoLocalizado._idUsuario);

                        livroDevolvido.DevolverLivro(novoEstadoLivro);
                        emprestimoLocalizado.DevolverLivro(dataDevolucao, usuarioDevolucao);

                        Console.WriteLine("Livro devolvido com sucesso!");
                        break;
                    }
                    else Console.WriteLine("Estado do livro inválido. Certifique-se de digitar um valor válido ou 0 para sair.");
                }
            }
        }
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

    static Livro LocalizarLivro()
    {
        string titulo;
        int idLivro;
        Livro livroLocalizado;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Digite o título ou o ID do livro: ");
            titulo = Console.ReadLine();

            if (int.TryParse(titulo, out idLivro))
            {
                livroLocalizado = listaDeLivros.FirstOrDefault(livro => livro.IdLivro == idLivro);
                return livroLocalizado;
            }

            livroLocalizado = listaDeLivros.FirstOrDefault(livro => livro._titulo.Contains(titulo));
        }
        return livroLocalizado;
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
}