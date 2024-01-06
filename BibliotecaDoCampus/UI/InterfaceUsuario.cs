using SistemaGerenciamento;
using UI.LogicaMenu;
using Usuarios;

namespace UI
{
    internal class InterfaceUsuario
    {
        static void Main(string[] args)
        {                                    
                Console.WriteLine("Bem-vindo(a) à biblioteca do nosso campus!");     
                Console.WriteLine("Informe seu Usuário e senha para fazer login no sistema.");
                Console.Write("Usuario: ");
                var login = Console.ReadLine();
                Console.Write("Senha: ");
                var senha = Console.ReadLine();
                Usuario user = Program.AutenticarUsuario(login, senha);
            
                if(user != null)
                {
                    if(user.nivelAcesso == NivelAcesso.Diretor)
                    {
                        MenuDiretor.Menu(user);                      
                    }
                    if(user.nivelAcesso == NivelAcesso.Atendente)
                    {
                        MenuAtendente.Menu(user);
                    }
                    if(user.nivelAcesso == NivelAcesso.Professor)
                    {
                        MenuProfessor.Menu(user);
                    }
                    if(user.nivelAcesso == NivelAcesso.Estudante)
                    {
                        MenuEstudante.Menu((Estudante)user);
                    }
                }
                else
                {
                    Console.WriteLine("Usuario ou senha invalidos");
                }            
        }
    }
}
