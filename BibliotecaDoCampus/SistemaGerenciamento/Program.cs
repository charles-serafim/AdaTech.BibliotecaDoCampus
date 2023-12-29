namespace SistemaGerenciamento;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Sistema de Biblioteca do Campus");

        List<Usuario> listaDeUsuarios = new List<Usuario>()
        {
            new Usuario("Charles Serafim", "c.s@mail.com", NivelAcesso.Estudante),
            new Usuario("Luana Ritter", "l.r@mail.com", NivelAcesso.Professor),
            new Usuario("Monalisa Brito", "m.b@mail.com", NivelAcesso.Diretor)
        };

        foreach(var usuario in listaDeUsuarios)
        {
            usuario.MostrarDados();
        }
    }
}
