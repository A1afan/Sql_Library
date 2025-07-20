using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;

namespace Sql_library
{
    public class DBClient
    {

        string conection = "Server=localhost;Database=CNAP;Uid=root;Pwd=936fydqq471kk63";
        static DBClient db_client;
        private DBClient()
        {
            
        }

        string GetDataBaseName()
        {
            int Start = conection.IndexOf("Database=") + "Database=".Length;
            int End = conection.IndexOf(";", Start);
            string databaseName = conection.Substring(Start, End - Start);
            return databaseName;
        }

        public string[] ColumsName(string tablename) // отримує назви стовпців для вказаної таблиці
        {
            //шукаємо назву бази даних
            string databaseName = GetDataBaseName();
            //

            List<string> database = new List<string>();
            string Query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{databaseName}' AND TABLE_NAME = '{tablename}'";
            try
            {
                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(conection))
                {
                    connection.Open();
                    using (var command = new MySql.Data.MySqlClient.MySqlCommand(Query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                database.Add(reader["COLUMN_NAME"].ToString());
                            }
                        }
                    }
                }
                return database.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new string[0];
            }
            
        }

        
        public void LoadData(string tablename , DataGridView DataView) // завантажує дані з таблиці у DataGridView
        {
            string[] columns = ColumsName(tablename);
            List<List<string>> rows = Select(tablename);
            DataView.Rows.Clear();
            DataView.Columns.Clear();
            foreach (var col in columns)
            {
                DataView.Columns.Add(col, col);
            }

            foreach (var row in rows)
            {
                DataView.Rows.Add(row.ToArray());
            }
        }

        public void LoadData(string tablename, DataGridView DataView, List<List<string>> rows) // завантажує дані з таблиці у DataGridView
        {
            string[] columns = ColumsName(tablename);
            DataView.Rows.Clear();
            DataView.Columns.Clear();
            foreach (var col in columns)
            {
                DataView.Columns.Add(col, col);
            }

            foreach (var row in rows)
            {
                DataView.Rows.Add(row.ToArray());
            }
        }

        public static DBClient GetInstance()
        {
            if (db_client == null)
            {
                db_client = new DBClient();
            }
            return db_client;
        }

        public List <List<string>> Select(string TableName) //вибирає всі дані з вказаної таблиці
        {
            List<List<string>> result = new List<List<string>>();
            try
            {
                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(conection))
                {
                    connection.Open();
                    string query = $"SELECT * FROM {TableName}";
                    using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<string> row = new List<string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader[i].ToString());
                                }
                                result.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }

        public void Insert(string TableName, List<string> values) //додає новий запис до таблиці
        {
            try
            {
                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(conection))
                {
                    connection.Open();
                    string query = $"INSERT INTO {TableName} VALUES ({string.Join(", ", values.Select(v => $"'{v}'"))})";
                    using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //отримання даних про структуру таблиці таблиці (таблиця яка надає дані про таблицю), щоб створити візуальні компоненти
        public List<(string, string, bool)> GetSchema(string TableName) 
        {
            string databaseName = GetDataBaseName();
            var result = new List<(string, string, bool)>();
            try
            {
                string query = $@"
                SELECT COLUMN_NAME, DATA_TYPE, 
                COLUMN_KEY = 'MUL' as IsForeignKey
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_SCHEMA = '{databaseName}' 
                AND TABLE_NAME = '{TableName}'";

                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(conection))
                {
                    connection.Open();
                    using (var command = new MySql.Data.MySqlClient.MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader[0].ToString();
                                string type = reader[1].ToString().ToLower();
                                bool key = reader.GetBoolean("IsForeignKey");
                                result.Add((name, type, key));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

    }

    
}
