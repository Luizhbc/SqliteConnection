using System;
using System.Data.SQLite;

namespace dia30
{
    class Program
    {
        static void Main()
        {
            Database databaseobject = new();
            Console.WriteLine("O que deseja fazer? ");
            Console.WriteLine("1 - Adicionar músicas na playlist");
            Console.WriteLine("2 - Ver músicas da playlist");
            Console.WriteLine("3 - Buscar músicas na playlist");
            Console.WriteLine("4 - Apagar uma música da playlist");
            string escolhaAcao = Console.ReadLine();
            if (escolhaAcao == "1")
            {
                InsertQuery();
            }
            if (escolhaAcao == "2")
            {
                AllFromDatabase();
            }
            if (escolhaAcao == "3")
            {
                SearchQuery();
            }
            if (escolhaAcao == "4")
            {
                DeleteQuery();
            }
        }
        static void InsertQuery()
        {
            Database databaseobject = new();
            string query = "INSERT INTO album('Id','Musica', 'Banda') VALUES(@id, @musica, @banda)";
            SQLiteCommand myCommand = new(query, databaseobject.myConnection);
            databaseobject.OpenConnection();
            Console.WriteLine("Nome da musica: ");
            myCommand.Parameters.AddWithValue("@musica", Console.ReadLine());
            Console.WriteLine("Nome da banda: ");
            myCommand.Parameters.AddWithValue("@banda", Console.ReadLine());
            Console.WriteLine("id da Musica");
            myCommand.Parameters.AddWithValue("@id", Console.ReadLine());
            var result = myCommand.ExecuteNonQuery();
            databaseobject.CloseConnection();
            Console.WriteLine("Adicionado: {0}", result);
            Console.ReadKey();
            Console.Clear();
            
            Main();
        }
        static void AllFromDatabase()
        {
            Database databaseobject = new();
            string query = "SELECT * FROM album";
            SQLiteCommand myCommand = new(query, databaseobject.myConnection);
            databaseobject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Console.WriteLine("Id: {0}, Música: {1}, Banda: {2}", result["id"], result["musica"], result["banda"]);
                }
            }
            databaseobject.CloseConnection();
            Console.ReadKey();
            Console.Clear();
            
            Main();
        }
        static void SearchQuery()
        {
            Database databaseobject = new();
            string query = "SELECT * FROM album WHERE Musica=@musica";
            SQLiteCommand myCommand = new(query, databaseobject.myConnection);
            Console.WriteLine("Nome da música: ");
            myCommand.Parameters.AddWithValue("@musica", Console.ReadLine());
            databaseobject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.Read())
            {                
                Console.WriteLine("Id: {0}, Música: {1}, Banda: {2}", result["id"], result["musica"], result["banda"]);
            }
            else
            {
                Console.WriteLine("Música não encontrada");
            }
            databaseobject.CloseConnection();
            Console.ReadKey();
            Console.Clear();
            
            Main();
        }
        static void DeleteQuery()
        {
            Database databaseobject = new();
            string query = "DELETE FROM album WHERE id =@value";
            SQLiteCommand myCommand = new(query, databaseobject.myConnection);
            Console.WriteLine("Digite o id da música: ");
            myCommand.Parameters.AddWithValue("@value", Console.ReadLine());
            databaseobject.OpenConnection();
            myCommand.ExecuteNonQuery();
            databaseobject.CloseConnection();
            Console.WriteLine("Música excluida");
            Console.ReadKey();
            Console.Clear();
            
            Main();
        }
    }
}

