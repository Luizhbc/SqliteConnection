using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace dia30
{
    class Database
    {
        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=database.sqlite");
            if (!File.Exists("./database.sqlite"))
            {            
                SQLiteConnection.CreateFile("database.sqlite");
                Console.WriteLine("Arquivo database criado");
                CreatePlaylist newlist = new();
                newlist.CreateTable();
            }
        }

        public void OpenConnection()
        {
            if(myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if(myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
     class CreatePlaylist
    {
        public void CreateTable()
        {
            Database databaseobject = new();
            string query = "CREATE TABLE album(Id INTEGER NOT NULL, Musica VARCHAR(50), Banda VARCHAR(50), PRIMARY KEY(Id));";
            SQLiteCommand myCommand = new(query, databaseobject.myConnection);
            databaseobject.OpenConnection();
            myCommand.ExecuteNonQuery();
            databaseobject.CloseConnection();
            Console.WriteLine("Playlist criada");

        }
    }
}
