using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Funcionarios;
using SistemaGerenciamento;
using SistemaGerenciamento.Models;
using Usuarios;

namespace UI.LogicaMenu
{
    internal class MenuAtendente
    {
        public static void Menu(Atendente atendente)
        {
            Console.WriteLine($"Logado como: {atendente.nome}");
            Console.WriteLine($"Privilegio: {atendente.nivelAcesso}\n\n");
            Console.WriteLine("1 - Listar Livros");
            Console.WriteLine("2 - Verificar disponibilidade");
            Console.WriteLine("3 - Solicitar livro");
            Console.WriteLine("4 - Reservar livro");
            Console.WriteLine("5 - Devolver Livro");
            Console.WriteLine("6 - Cancelar reserva");
            Console.WriteLine("7 - Exibir histórico");
            Console.WriteLine("8 - Consultar débitos");
            Console.WriteLine("9 - Localizar reserva");
            Console.WriteLine("10 - Listar reservas");
            Console.WriteLine("11 - Cadastrar livro");
            Console.WriteLine("12 - Atualizar exemplar");
            Console.WriteLine("13 - Cadastrar funcionario");
            Console.WriteLine("0 - Sair");

            int opcao = int.TryParse(Console.ReadLine(), out opcao) ? opcao : 0;
            switch (opcao)
            {
                case 1:
                    List<Livro> listaLivros = Program.ListarLivros();
                    foreach (var livro in listaLivros) livro.MostrarDados();
                    break;
                case 2:
                    Console.WriteLine("Digite o título ou o ID do livro: ");
                    string input = Console.ReadLine();
                    Console.WriteLine(Program.VerificarDisponibilidade(input) ? "Livro disponível." : "Livro indisponível.");
                    break;
                case 3:
                    atendente.SolicitarLivro();       //estudante
                    break;
                case 4:
                    atendente.ReservarLivro();        //funcionario
                    break;
                case 5:
                    atendente.DevolverLivro();        //usuario
                    break;
                case 6:
                    atendente.CancelarReserva();      //usuario
                    break;
                case 7:
                    atendente.ExibirHistorico();      //usuario
                    break;
                case 8:
                    atendente.ConsultarDebitos();     //sistema
                    break;
                case 9:
                    atendente.LocalizarReserva();     //usuario
                    break;
                case 10:
                    atendente.ListarReservas();       //usuario > funcionario
                    break;
                case 11:
                    atendente.CadastrarLivro();       //funcionario -> JsonParser
                    break;
                case 12:
                    atendente.AtualizarExemplar();    //funcionario
                    break;
                case 13:
                    atendente.AtualizarCadastro();    //atendente -> JsonParser
                    break;
                case 14:
                    atendente.AutorizarEmprestimo();  //atendente
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

    }
}
