﻿using SistemaGerenciamento;
using SistemaGerenciamento.Models;
using SistemaGerenciamento.Usuarios;
using Usuario = SistemaGerenciamento.Usuarios.Usuario;

namespace UI.LogicaMenu
{
    internal static class MenuEstudante
    {
        internal static void Menu(Estudante estudante)
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
            Console.WriteLine("9 - Solicitar alteração do cadastro");
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
                    Console.WriteLine("Informe o id do livro que deseja alugar");
                    idLivro = int.TryParse(Console.ReadLine(), out idLivro) ? idLivro : 0;
                    if (estudante.VerificarDisponibilidade(idLivro))
                    {
                        estudante.SolicitarLivro(idLivro);
                    }
                    break;                          //Implementado SolicitarLivro()
                case 4:
                    Console.WriteLine("Informe o id do livro que deseja reservar");
                    idLivro = int.TryParse(Console.ReadLine(), out idLivro) ? idLivro : 0;
                    estudante.ReservarLivro(idLivro);      
                    break;                          //Implementado ReservarLivro()
                case 5:
                    Console.WriteLine("Informe o id do livro que deseja devolver");
                    idLivro = int.TryParse(Console.ReadLine(), out idLivro) ? idLivro : 0;
                    estudante.DevolverLivro(idLivro);
                    break;                          //Implementado DevolverLivro()
                case 6:
                    Console.WriteLine("Informe o id do livro que deseja cancelar a reserva");
                    idLivro = int.TryParse(Console.ReadLine(), out idLivro) ? idLivro : 0;
                    if(estudante.CancelarReserva(idLivro)) Console.WriteLine("Reserva cancelada com sucesso");
                    else Console.WriteLine("Erro no cancelamento da reserva");
                    break;                          //Implementado CancelarReserva()
                case 7:                                                     
                    List<Emprestimo> historico = estudante.ExibirHistorico();
                    foreach (var item in historico)
                    {
                        Console.WriteLine(item._idEmprestimo);
                        Console.WriteLine(item._idLivro);
                        Console.WriteLine(item._dataDevolucao);
                        Console.WriteLine(item.multa);
                    };
                    break;                          //Implementado ExibirHistorico()
                case 8:                                                      
                    Console.WriteLine(estudante.ConsultarDebitos());
                    break;                          //Implementado ConsultarDebitos()
                case 9:
                    string nome;
                    string sobrenome;
                    string email;
                    
                    Console.WriteLine("Deseja alterar o nome? 1-SIM  OUTRO VALOR-NAO");
                    int opcaoAlteracao = int.TryParse(Console.ReadLine(), out opcaoAlteracao) ? opcaoAlteracao : 0;
                    if (opcaoAlteracao == 1)
                    {
                        Console.WriteLine("Informe o novo nome");
                        nome = Console.ReadLine();
                    } else nome = null;

                    Console.WriteLine("Deseja alterar o sobrenome? 1-SIM OUTRO VALOR-NAO");
                    opcaoAlteracao = int.TryParse(Console.ReadLine(), out opcaoAlteracao) ? opcaoAlteracao : 0;
                    if (opcaoAlteracao == 1)
                    {
                        Console.WriteLine("Informe o novo sobrenome");
                        sobrenome = Console.ReadLine();
                    } else sobrenome = null;
                    
                    Console.WriteLine("Deseja alterar o email? 1-SIM OUTRO VALOR-NAO");
                    opcaoAlteracao = int.TryParse(Console.ReadLine(), out opcaoAlteracao) ? opcaoAlteracao : 0;
                    if (opcaoAlteracao == 1)
                    {
                        Console.WriteLine("Informe o novo email");
                        email = Console.ReadLine();
                    } else email = null;
                    Usuario alteracao = estudante.SolicitarAlteracaoCadastro(nome, sobrenome, email);

                    Program.SolicitarAlteracaoCadastro(estudante, alteracao);
                    break;                          //Implementado SolicitarAlteracaoCadastro()
                case 0:
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
    }
}
