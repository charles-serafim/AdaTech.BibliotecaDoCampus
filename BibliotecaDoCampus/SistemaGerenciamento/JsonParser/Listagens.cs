using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGerenciamento;
using SistemaGerenciamento.Models;

namespace SistemaGerenciamento.JsonParser
{
    public class Listagem : JsonParser<Livro>
    {
        private List<Livro> livros;

        public Listagem()
        {
            livros = ReceberJson();
        }

        public static List<Livro> GetLivros()
        {
            List<Livro> livros2;
            return livros2 = ReceberJson(); ;
        }
        
        protected List<Livro> ListarLivros(Func<Livro, bool> predicate) =>
            livros.Where(predicate).ToList();

        protected List<Livro> ListarTodosLivros() =>
            ListarLivros(livro => true);

        protected List<Livro> ListarLivrosPorSetor(Acervo setor) =>
            ListarLivros(livro => livro._acervo == setor);

        protected List<Livro> ListarLivrosPorEstado(EstadoLivro estado) =>
            ListarLivros(livro => livro._estadoLivro == estado);

        public void MostrarAutores(List<Livro> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine($"Authors for Id {item.IdLivro}:");
                foreach (var autor in item._autores)
                {
                    Console.WriteLine(autor);
                }
                Console.WriteLine();
            }
        }

        public void MostrarDados(List<Livro> items)
        {
            if (items == null)
            {
                Console.WriteLine("The list of items is null.");
                return;
            }

            Console.WriteLine("--- Livros em Questão ---");

            var itensOrdenados = items.OrderBy(item => item?.IdLivro);
            foreach (var item in itensOrdenados)
            {
                Console.WriteLine($"Id: {item.IdLivro} - Título: {item._titulo} - Acervo: {item._acervo}"
                    + $" Autores: - Edição: {item._edicao} - Estado: {item._estadoLivro}");

                // Call MostrarAutores to print authors for the current book
                MostrarAutores(new List<Livro> { item });
            }
        }



        public List<Livro> ColecaoLivros => ReceberJson();
    }
}
