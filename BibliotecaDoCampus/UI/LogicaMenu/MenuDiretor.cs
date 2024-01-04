using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Users;

namespace UI.LogicaMenu
{
    internal class MenuDiretor
    {
        public static void Menu(Usuario diretor)
        {
            Console.WriteLine($"Logado como: {diretor.nome}");
            Console.WriteLine($"Privilegio: {diretor.nivelAcesso}");
            Console.WriteLine("\n\n1 - Listar Livros");
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
                    diretor.ListarLivros();         //sistema
                    break;
                case 2:
                    diretor.VerificarDisponibilidade(); //usuario
                    break;
                case 3:
                    diretor.SolicitarLivro();       //estudante
                    break;
                case 4:
                    diretor.ReservarLivro();        //funcionario
                    break;
                case 5:
                    diretor.DevolverLivro();        //usuario
                    break;
                case 6:
                    diretor.CancelarReserva();      //usuario
                    break;
                case 7:
                    diretor.ExibirHistorico();      //usuario
                    break;
                case 8:
                    diretor.ConsultarDebitos();     //sistema
                    break;
                case 9:
                    diretor.LocalizarReserva();     //usuario
                    break;
                case 10:
                    diretor.ListarReservas();       //usuario > funcionario
                    break;
                case 11:
                    diretor.CadastrarLivro();       //atendente
                    break;
                case 12:
                    diretor.AtualizarExemplar();    //atendente
                    break;
                case 13:
                    diretor.CadastrarFuncionario();  //diretor
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
