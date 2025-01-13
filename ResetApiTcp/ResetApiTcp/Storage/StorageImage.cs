using ResetApiTcp.Cliernt.Command.Sql.DataBase;
using ResetApiTcp.Patterns.sql_data_base;
using System.Data.SQLite;

namespace ResetApiTcp.Storage
{
    internal class StorageImage
    {
        private string GlobalNameTableImageStore = "store_image";
        private SQLiteDataBase _db;
        public StorageImage(SQLiteDataBase db)
        {
            if (db != null)
            {
                this._db = db;
                Init();
            }
            else
                throw new Exception("Data Base equals NULL");
        }
        private void InitCraeteTable() 
        {
            string CreateTableStorageImage = $"create table if not exists [{GlobalNameTableImageStore}] ({p_store_image.id} INTEGER PRIMARY KEY AUTOINCREMENT, {p_store_image.name} TEXT not null, {p_store_image.image} Blob not null)";
            using (SQLiteCommand command = new SQLiteCommand(CreateTableStorageImage, _db.GetConnection())) 
            {
                command.ExecuteNonQuery();
            }
        }
        private void Init() 
        {
            InitCraeteTable();
        }
        public bool Add(string NameImage ,byte[] Image) 
        {
            bool r = false;
            try
            {
                string AddItemImage = $"insert into [{GlobalNameTableImageStore}] ({p_store_image.name}, {p_store_image.image}) values (@{p_store_image.name}, @{p_store_image.image})";
                using (SQLiteCommand command = new SQLiteCommand(AddItemImage, _db.GetConnection()))
                {
                    command.Parameters.AddWithValue($"@{p_store_image.name}", NameImage);
                    command.Parameters.AddWithValue($"@{p_store_image.image}", Image);
                    command.ExecuteNonQuery();
                    r = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return r;
        }
        public byte[] GetImage(int Id) 
        {
            byte[] image = null;
            string getAllImage = $"select * from {GlobalNameTableImageStore}";
            using (SQLiteDataReader read = new SQLiteCommand(getAllImage, _db.GetConnection()).ExecuteReader()) 
            {
                while (read.Read()) 
                {
                    if (read.GetInt32(0) == Id)
                    {
                        image = read[p_store_image.image] as byte[];
                        break;
                    }
                }
            }

            return image;
        }
        public void Close() 
        {
            _db.Close();
        }
    }
}
