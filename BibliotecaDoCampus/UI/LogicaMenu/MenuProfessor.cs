using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Users;

namespace UI.LogicaMenu
{
     internal class MenuProfessor
    {
        internal static void Menu(Usuario professor)
        {
            Console.WriteLine($"Logado como: {professor.nome}");

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
                    professor.ListarLivros();       //sistema
                    break;
                case 2:
                    professor.VerificarDisponibilidade(); //usuario
                    break;
                case 3:
                    professor.SolicitarLivro();     //professor
                    break;
                case 4:
                    professor.ReservarLivro();      //sistema
                    break;
                case 5:
                    professor.DevolverLivro();      //usuario
                    break;
                case 6:
                    professor.CancelarReserva();    //usuario
                    break;
                case 7:
                    professor.ExibirHistorico();    //usuario
                    break;
                case 8:
                    professor.ConsultarDebitos();   //sistema
                    break;
                case 9:
                    professor.LocalizarReserva();   //usuario
                    break;
                case 10:
                    professor.ListarReservas();     //usuario
                    break;
                case 11:
                    professor.SolicitarAlteracaoCadastro();     //sistema -> atendente
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
