using UI.LogicaMenu;
using Usuarios.Users;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {                                    
                Console.WriteLine("Bem-vindo(a) à biblioteca do nosso campus!");     
                Console.WriteLine("Informe seu Usuário e senha para fazer login no sistema.");
                Console.Write("Usuario: ");
                var usuario = Console.ReadLine();
                Console.Write("Senha: ");
                var senha = Console.ReadLine();
                Usuario user = SistemaBiblioteca.AutenticarUsuario(usuario, senha);
            
                if(user != null)
                {
                    if(user.nivelAcesso == Usuarios.Enums.NivelAcesso.Diretor)
                    {
                        MenuDiretor.Menu(user);                      
                    }
                    if(user.nivelAcesso == Usuarios.Enums.NivelAcesso.Atendente)
                    {
                        MenuAtendente.Menu(user);
                    }
                    if(user.nivelAcesso == Usuarios.Enums.NivelAcesso.Professor)
                    {
                        MenuProfessor.Menu(user);
                    }
                    if(user.nivelAcesso == Usuarios.Enums.NivelAcesso.Estudante)
                    {
                        MenuEstudante.Menu(user);
                    }
                }
                else
                {
                    Console.WriteLine("Usuario ou senha invalidos");
                }            
        }
    }
}
