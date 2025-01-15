using ResetApiTcp.Cliernt.Command.Sql.DataBase;
using ResetApiTcp.Cliernt.Command.Sql.SqlCommand.MyExeption.Users;
using ResetApiTcp.Patterns;
using ResetApiTcp.Patterns.sql_data_base;
using ResetApiTcp.Storage;
using System.Data.SQLite;

namespace ResetApiTcp.Cliernt.Command.Sql.SqlCommand
{
    class SQLiteCommands
    {
        private string GlobalNameTableUsers = "users";

        private SQLiteDataBase _db = default(SQLiteDataBase);

        private StorageImage store_img = default(StorageImage);
        public SQLiteCommands(DataBase.SQLiteDataBase db)
        {
            if (db != null)
            {
                _db = db;
                store_img = new StorageImage(db);
                Init();
            }
        }
        private void InitTableCreate()
        {
            string requestCreateTable1 = $"create table if not exists [{GlobalNameTableUsers}]( {Patterns.sql_data_base.p_user_sql.id} INTEGER PRIMARY KEY AUTOINCREMENT, {Patterns.sql_data_base.p_user_sql.first_name} TEXT not null, {Patterns.sql_data_base.p_user_sql.last_name} TEXT not null, {Patterns.sql_data_base.p_user_sql.gmail} TEXT not null unique, {Patterns.sql_data_base.p_user_sql.password} TEXT not null)";
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
        private r_user login(p_user user)
        {
            r_user result_t = new r_user();
            List<MyExeption.Users.p_user> list = get_all_user();
            bool find_user = false;
            int id_user = 0;
            foreach (MyExeption.Users.p_user i_user in list)
            {
                Console.WriteLine($"gmail: {i_user.Gmail} password: {i_user.Password}");
                if (user.Gmail.Equals(i_user.Gmail) && user.Password.Equals(i_user.Password))
                {
                    find_user = true;
                    id_user = user.Id;
                    break;
                }
            }
            if (find_user == true)
            {
                result_t.UserId = id_user;
                result_t.Status = "SUCCESS";
            }
            else
            {
                result_t.UserId = -1;
                result_t.Status = "FAILED_LOGIN";
            }
            return result_t;
        }
        public object Command(string command, object obj)
        {
            object result = null;
            if (obj != null)
            {
                if (command != String.Empty && command.Length > GeneralMeaning.GeneralMeaning.EmptyOfValue)
                {
                    switch (command)
                    {
                        case "register":
                            {
                                p_user userType = obj as p_user;
                                r_user result_t = new r_user();

                                List<MyExeption.Users.p_user> list = get_all_user();
                                bool find_user = false;
                                foreach (MyExeption.Users.p_user user in list)
                                {
                                    if (userType.Gmail.Equals(user.Gmail))
                                    {
                                        find_user = true;
                                        break;
                                    }
                                }
                                if (find_user == false)
                                {
                                    string insertUser = $"Insert into [{GlobalNameTableUsers}] ({Patterns.sql_data_base.p_user_sql.first_name}, {Patterns.sql_data_base.p_user_sql.last_name}, {Patterns.sql_data_base.p_user_sql.gmail}, {Patterns.sql_data_base.p_user_sql.password}) values (@{Patterns.sql_data_base.p_user_sql.first_name}, @{Patterns.sql_data_base.p_user_sql.last_name}, @{Patterns.sql_data_base.p_user_sql.gmail}, @{Patterns.sql_data_base.p_user_sql.password})";
                                    using (SQLiteCommand commandSql = new SQLiteCommand(insertUser, _db.GetConnection()))
                                    {
                                        commandSql.Parameters.AddWithValue($"@{Patterns.sql_data_base.p_user_sql.first_name}", userType.FirstName);
                                        commandSql.Parameters.AddWithValue($"@{Patterns.sql_data_base.p_user_sql.last_name}", userType.LastName);
                                        commandSql.Parameters.AddWithValue($"@{Patterns.sql_data_base.p_user_sql.password}", userType.Password);
                                        commandSql.Parameters.AddWithValue($"@{Patterns.sql_data_base.p_user_sql.gmail}", userType.Gmail);
                                        commandSql.ExecuteNonQuery();
                                    }
                                    result_t.UserId = 0;
                                    result_t.Status = "SUCCESS";
                                }
                                else
                                {
                                    result_t.UserId = -1;
                                    result_t.Status = "FAILED_REGISTER_HAVE_USER";
                                }
                                result = result_t;
                            }
                            break;

                        case "login":
                            {
                                p_user userType = obj as p_user;
                                result = login(userType);
                            }
                            break;
                        case "user_get_info_img":
                            {
                                p_cmu obj_p_cmu = obj as p_cmu;
                                if (obj_p_cmu.command.Equals("user_get_info_img"))
                                {
                                    //lock user data
                                    p_img_u_info r_p_img_u_info = new p_img_u_info();
                                    if (login(obj_p_cmu.user).Status.Equals("SUCCESS"))
                                    {
                                        string name_img_user = $"user_img_({obj_p_cmu.user.Gmail})";
                                        byte[] img_data;
                                        if (store_img.CheckNameImg(name_img_user))
                                        {
                                            img_data = store_img.GetImage(name_img_user);
                                        }
                                        else
                                        {
                                            img_data = File.ReadAllBytes(GeneralMeaning.GeneralMeaning.imgs_defult["user_defult_img"]);
                                            store_img.Add(name_img_user, img_data);
                                        }
                                        r_p_img_u_info.img = img_data;
                                        r_p_img_u_info.user = get_user(obj_p_cmu.user);
                                    }
                                    else
                                    {
                                        result = "FAILED_GET_INFO_IMG";
                                    }
                                    result = r_p_img_u_info;
                                }

                            }
                            break;
                        case "user_set_img_profile":
                            {
                                p_cm_img obj_p_cm_img = obj as p_cm_img;
                                if (obj_p_cm_img.command.Equals("user_set_img_profile"))
                                {
                                    if (login(obj_p_cm_img.user).Status.Equals("SUCCESS"))
                                    {
                                        string name_img_user = $"user_img_({obj_p_cm_img.user.Gmail})";
                                        store_img.ChangeImg(name_img_user, obj_p_cm_img.image);
                                        result = "SUCCESS";
                                    }
                                    else
                                    {
                                        result = "FAILED_SET_IMAGE_PROFILE";
                                    }
                                }

                            }
                            break;

                    }

                }

            }
            return result;
        }
        private p_user get_user(p_user user)
        {
            p_user r_p_user = new p_user();
            foreach (p_user i_user in get_all_user())
            {
                if (i_user.Gmail.Equals(user.Gmail) && i_user.Password.Equals(user.Password))
                    r_p_user = i_user;
                break;
            }
            return r_p_user;
        }
        private List<MyExeption.Users.p_user> get_all_user()
        {
            List<MyExeption.Users.p_user> t_gets = new List<MyExeption.Users.p_user>();

            string requestSelectFromTable = $"Select * From {GlobalNameTableUsers}";
            using (SQLiteDataReader read = new SQLiteCommand(requestSelectFromTable, _db.GetConnection()).ExecuteReader())
            {
                while (read.Read())
                {
                    MyExeption.Users.p_user user = new MyExeption.Users.p_user();
                    user.Id = read.GetInt32(0);
                    user.FirstName = read.GetString(1);
                    user.LastName = read.GetString(2);
                    user.Gmail = read.GetString(3);
                    user.Password = read.GetString(4);
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
