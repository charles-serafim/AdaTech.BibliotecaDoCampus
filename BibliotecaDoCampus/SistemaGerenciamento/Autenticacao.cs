using System;

using System.Security.Cryptography;
using Usuarios.Funcionarios;


namespace SistemaGerenciamento
{
    public class Autenticacao
    {
        public static void AutenticarUsuario(int idAtendente, string senha)
        {
            var funcionario = BuscarFuncionarioPorId(idAtendente);

            if (funcionario == null || !ValidaSenha(senha))
            {
                return "Usuário ou senha inválidos!";
            }
            else
            {
                return "Usuário autenticado com sucesso!"
            }


            byte[] hashSenhaFornecida = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(senha));

            return hashSenhaFornecida.SequenceEqual(funcionario.senha);

            funcionario.senhaExpiracao = DateTimeOffset.Now.AddDays(30);

            return true
        }

        private static Funcionario BuscarFuncionarioPorId(int idAtendente)
        {
            var funcionarios = File.ReadAllText("funcionarios.json");
            var json = JsonConvert.DeserializeObjeto<List<Funcionarios>>(funcionarios);
            var funcionario = json.FirstOrDefault(f => f.Id == idAtendente);

            return funcionario;
        }

        private static bool ValidaSenha(string senha)
        {
            if (senha.lengh < 8)
            {
                return false;
            }

            int contadorLetraMaius = 0;
            int contadorLetraMinus = 0;
            int contadorNum = 0;

            for (int i = 0; i < senha.length; i++)
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

                return contadorLetraMaius >= 1 && contadorLetraMinus >= 1 && contadorNum >= 1;


            }
        }
    }

