using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Usuarios.Users;

namespace UI.LogicaMenu
{
    internal static class MenuEstudante
    {
        internal static void Menu(Usuario estudante)
        {
            Console.WriteLine($"Logado como: {estudante.nome}");
            
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
            Console.WriteLine("11 - Solicitar alteração do cadastro");
            Console.WriteLine("0 - Sair");

            int opcao = int.TryParse(Console.ReadLine(), out opcao) ? opcao : 0;
            switch (opcao)
            {
                case 1:
                    estudante.ListarLivros();       //sistema
                    break;
                case 2:
                    estudante.VerificarDisponibilidade(); //usuario
                    break;
                case 3:
                    estudante.SolicitarLivro();     //estudante
                    break;
                case 4:
                    estudante.ReservarLivro();      //sistema
                    break;
                case 5:
                    estudante.DevolverLivro();      //usuario
                    break;
                case 6:
                    estudante.CancelarReserva();    //usuario
                    break;
                case 7:
                    estudante.ExibirHistorico();    //usuario
                    break;
                case 8:
                    estudante.ConsultarDebitos();   //sistema
                    break;
                case 9:
                    estudante.LocalizarReserva();   //usuario
                    break;
                case 10:
                    estudante.ListarReservas();     //usuario
                    break;
                case 11:
                    estudante.SolicitarAlteracaoCadastro();     //sistema -> atendente
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
