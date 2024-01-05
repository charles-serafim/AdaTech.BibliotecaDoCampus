using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SistemaGerenciamento.JsonParser
{
    public abstract class JsonParser<T> where T : class
    {
        public static List<T> ReceberJson()
        {
            string tipo = typeof(T).Name; // Recebe o nome da classe

            // Recebe os paths
            string absolutePath = $@"C:\Users\luanar\source\repos\AdaTech.BibliotecaDoCampus\BibliotecaDoCampus\SistemaGerenciamento\JsonParser\db.json";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.GetRelativePath(baseDirectory, absolutePath);

            try
            {
                string jsonString = File.ReadAllText(absolutePath);
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(jsonString);
                return itemList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public static void Salvar(List<T> list)
        {
            string absolutePath = $@"C:\Users\luanar\source\repos\AdaTech.BibliotecaDoCampus\BibliotecaDoCampus\SistemaGerenciamento\JsonParser\db.json";
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(absolutePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
