using SistemaGerenciamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Usuarios;
using Usuario = Usuarios.Usuario;

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
                case 1:                                                             //Implementado ListarLivros()
                    List<Livro> listaLivros = estudante.ListarLivros();       
                    foreach (var item in listaLivros)
                    {
                        Console.WriteLine(item.IdLivro);
                        Console.WriteLine(item._titulo);
                        Console.WriteLine(item._autores);
                        Console.WriteLine(item._edicao);
                    };  
                    break;                          //Implementado ListarLivros()
                case 2:                                                      
                    Console.WriteLine("Informe o id do livro a consultar");
                    int idLivro = int.TryParse(Console.ReadLine(), out idLivro) ? idLivro : 0;
                    estudante.VerificarDisponibilidade(idLivro); 
                    break;                          //Implementado VerificarDisponibilidade()
                case 3:
                    estudante.SolicitarLivro();     //Aguardando implementação do sistema
                    break;
                case 4:
                    Console.WriteLine("Informe o id do livro que deseja reservar");
                    idLivro = int.TryParse(Console.ReadLine(), out idLivro) ? idLivro : 0;
                    estudante.ReservarLivro(idLivro);      
                    break;                          //Implementado ReservarLivro()
                case 5:
                    estudante.DevolverLivro();      //Aguardando implementação do sistema
                    break;
                case 6:
                    estudante.CancelarReserva();    //Aguardando implementação do sistema
                    break;
                case 7:                                                     
                    List<Emprestimo> historico = estudante.ExibirHistorico();
                    foreach (var item in historico)
                    {
                        Console.WriteLine(item.idLivro);
                        Console.WriteLine(item.idLivro);
                        Console.WriteLine(item._dataDevolucao);
                        Console.WriteLine(item.multa);
                    };
                    break;                          // Implementado ExibirHistorico()
                case 8:                                                      
                    Console.WriteLine(estudante.ConsultarDebitos());
                    break;                          // Implementado ConsultarDebitos()
                case 9:
                    estudante.LocalizarReserva();   //Aguardando implementação do sistema
                    break;
                case 10:
                    estudante.ListarReservas();     //Aguardando implementação do sistema
                    break;
                case 11:
                    estudante.SolicitarAlteracaoCadastro();     //Aguardando implementação do sistema
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
