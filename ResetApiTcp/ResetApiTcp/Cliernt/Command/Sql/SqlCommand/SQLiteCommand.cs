using ResetApiTcp.Cliernt.Command.Sql.DataBase;
using ResetApiTcp.Cliernt.Command.Sql.SqlCommand.MyExeption.Users;
using ResetApiTcp.Patterns.sql_data_base;
using System.Data.SQLite;

namespace ResetApiTcp.Cliernt.Command.Sql.SqlCommand
{
    class SQLiteCommands
    {
        private string GlobalNameTableUsers = "users";

        private SQLiteDataBase _db = default(SQLiteDataBase);
        public SQLiteCommands(DataBase.SQLiteDataBase db) 
        {
            if (db != null) {
                _db = db;
                Init();
            }
        }
        private void InitTableCreate()
        {
            string requestCreateTable1 = $"create table if not exists [{GlobalNameTableUsers}]( {p_user.id} INTEGER PRIMARY KEY AUTOINCREMENT, {p_user.first_name} TEXT not null, {p_user.last_name} TEXT not null, {p_user.gmail} TEXT not null unique, {p_user.password} TEXT not null)";
            if (_db != null)
            {
                using (SQLiteCommand command = new SQLiteCommand(requestCreateTable1, _db.GetConnection()))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        private void Init()
        {
            InitTableCreate();
        }
        public object Command(string command, object obj) 
        {
            object result = null;
            if (obj != null) 
            {
                if (command != String.Empty && command.Length > GeneralMeaning.GeneralMeaning.EmptyOfValue) {
                    switch (command)
                    {
                        case "user_register":
                            {
                                SqlUserType userType = obj as SqlUserType;
                                ResultUserType result_t = new ResultUserType();

                                List<SqlUserType> list = SelectFromTableUser();
                                bool find_user = false;
                                foreach (SqlUserType user in list)
                                {
                                    if (userType.Gmail.Equals(user.Gmail))
                                    {
                                        find_user = true;
                                        break;
                                    }
                                }
                                if (find_user == false)
                                {
                                    string insertUser = $"Insert into [{GlobalNameTableUsers}] ({p_user.first_name}, {p_user.last_name}, {p_user.gmail}, {p_user.password}) values (@{p_user.first_name}, @{p_user.last_name}, @{p_user.gmail}, @{p_user.password})";
                                    using (SQLiteCommand commandSql = new SQLiteCommand(insertUser, _db.GetConnection()))
                                    {
                                        commandSql.Parameters.AddWithValue($"@{p_user.first_name}", userType.FirstName);
                                        commandSql.Parameters.AddWithValue($"@{p_user.last_name}", userType.LastName);
                                        commandSql.Parameters.AddWithValue($"@{p_user.password}", userType.Password);
                                        commandSql.Parameters.AddWithValue($"@{p_user.gmail}", userType.Gmail);
                                        commandSql.ExecuteNonQuery();
                                    }
                                    result_t.UserId = 0;
                                    result_t.Satus = "SUCCESS";
                                }
                                else 
                                {
                                    result_t.UserId = -1;
                                    result_t.Satus = "find_user not false";
                                }
                                result = result_t;
                            }
                            break;

                        case "user_login":
                            {
                                SqlUserType userType = obj as SqlUserType;
                                ResultUserType result_t = new ResultUserType();

                                List<SqlUserType> list = SelectFromTableUser();
                                bool find_user = false;
                                string token = "NULL";
                                foreach (SqlUserType user in list)
                                {
                                    if (userType.Gmail.Equals(user.Gmail))
                                    {
                       
                                        find_user = true;
                                        break;
                                    }
                                }
                                if (find_user == true)
                                {
                                    
                                    result_t.UserId = 0;
                                    result_t.Satus = "SUCCESS";
                                }
                                else
                                {
                                    result_t.UserId = -1;
                                    result_t.Satus = "FAILED";
                                }
                                result = result_t;
                            }
                            break;
                        
                    }
                }
            }
            return result;
        }
        private List<SqlUserType> SelectFromTableUser() 
        {
            List<SqlUserType> t_gets = new List<SqlUserType>();
            
                string requestSelectFromTable = $"Select * From {GlobalNameTableUsers}";
            using (SQLiteDataReader read = new SQLiteCommand(requestSelectFromTable, _db.GetConnection()).ExecuteReader())
            {
                while (read.Read())
                {
                    SqlUserType user = new SqlUserType();
                    user.Id = read.GetInt32(0);
                    user.FirstName = read.GetString(1);
                    user.LastName = read.GetString(2);
                    user.Gmail = read.GetString(3);
                    t_gets.Add(user);
                }
            }
            
            return t_gets;
        }
        public void Close() 
        {
            if (_db != null) 
            {
                _db.Close();
            }
        }
    }
}
