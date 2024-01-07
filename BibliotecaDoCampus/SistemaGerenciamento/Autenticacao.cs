using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SistemaGerenciamento.Usuarios;


namespace SistemaGerenciamento
{
    public class Autenticacao
    {

        internal byte[] GerarSenhaHash(string senha)
        {
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public static Usuario LogarAtendente(int idAtendente, string senha)
        {
            var atendente = BuscarAtendentePorId(idAtendente);

            if (atendente == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var senhaHash = GerarSenhaHash(senha);

            if (atendente.senhaHash.SequenceEqual(senhaHash))
            {
                AtualizarSenhaExpiracao(atendente);
                return atendente;
            }

            return null;

        }

        public static Usuario LogarDiretor(int idDiretor, string senha)
        {
            var diretor = BuscarDiretorPorId(idDiretor);
            if (diretor == null || !ValidaSenha(senha))
            {
                throw new ArgumentException("Usuário não encontrado");
            }


            var senhaHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(senha));
            if (!diretor.senhaHash.SequenceEqual(senhaHash))
            {
                return null;
            }

            AtualizarSenhaExpiracao(diretor);
            return diretor;
        }

        public static Usuario LogarProfessor(int idProfessor, string senha)
        {
            var professor = BuscarProfessorPorId(idProfessor);
            if (professor == null || !ValidaSenha(senha))
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var senhaHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(senha));

            if (!professor.senhaHash.SequenceEqual(senhaHash))
            {
                return null;
            }

            AtualizarSenhaExpiracao(professor);
            return professor;
        }

        /* aluno
        public static Usuario LogarAluno(int idAluno, string senha)
        {
            var aluno = BuscarAlunoPorId(idAluno);
            if (aluno == null || !ValidaSenha(senha))
            {
                return "Usuário ou senha inválidos!";
            }
            else
            {
                return "Usuário autenticado com sucesso!"
            }

            byte[] hashSenhaFornecida = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(senha));

            return hashSenhaFornecida.SequenceEqual(aluno.senha);

            aluno.senhaExpiracao = DateTimeOffset.Now.AddDays(30);

            return true
        }
        */

        public static void AtualizarSenhaExpiracao(Usuario usuario)
        {
            usuario.senhaExpiracao = DateTimeOffset.Now.AddDays(30);
        }



        private static Atendente BuscarAtendentePorId(int idAtendente)
        {
            var atendente = File.ReadAllText("atendente.json");
            var json = JsonConvert.DeserializeObjeto<List<Atendente>>(atendente);
            var atendente = json.FirstOrDefault(f => f.Id == idAtendente);

            if (atendente == null)
            {
                throw new ArgumentException("Atendente não encontrado");
            }

            return atendente;
        }

        private static Diretor BuscarDiretorPorId(int idDiretor)
        {
            var diretor = File.ReadAllText("diretor.json");
            var json = JsonConvert.DeserializeObjeto<List<Diretor>>(diretor);
            var diretor = json.FirstOrDefault(f => f.Id == idDiretor);

            return diretor;
        }

        private static Professor BuscarProfessorPorId(int idProfessor)
        {
            var professor = File.ReadAllText("professor.json");
            var json = JsonConvert.DeserializeObjeto<List<Professor>>(professor);
            var professor = json.FirstOrDefault(f => f.Id == idProfessor);

            return professor;
        }
        /* aluno
        private static Aluno BuscarAlunoPorId(int idAluno)
        {
            var aluno = File.ReadAllText("aluno.json");
            var json = JsonConvert.DeserializeObjeto<List<Aluno>>(aluno);
            var aluno = json.FirstOrDefault(f => f.Id == idAluno);

            return aluno;
        }

        */

        private static bool ValidaSenha(string senha)
        {
            if (senha.Length < 8)
            {
                return false;
            }

            int contadorLetraMaius = 0;
            int contadorLetraMinus = 0;
            int contadorNum = 0;

            for (int i = 0; i < senha.Length; i++)
            {
                char caractere = senha[i];

                if (char.IsUpper(caractere))
                {
                    contadorLetraMaius++;
                }
                else if (char.IsLower(caractere))
                {
                    contadorLetraMinus++;
                }
                else if (char.IsDigit(caractere))
                {
                    contadorNum++;
                }

                return (contadorLetraMaius >= 1 && contadorLetraMinus >= 1 && contadorNum >= 1);


            }
        }
    }

}
