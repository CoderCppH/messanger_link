using System.Data;
using System.Data.SQLite;
namespace ResetApiTcp.Cliernt.Command.Sql.DataBase
{
    class SQLiteDataBase
    {
        
        private SQLiteConnection _connection = default(SQLiteConnection);
        public SQLiteDataBase(string NameDb) 
        {
            if (NameDb != String.Empty && NameDb.Length > GeneralMeaning.GeneralMeaning.EmptyOfValue) {
                string StringConnection = $"Data Source={NameDb}";
                _connection = new SQLiteConnection(StringConnection);
            }
        }
        public void Open() 
        {
            if (_connection != null) {
                if (_connection.State.Equals(ConnectionState.Closed))
                {
                    _connection.Open();
                }
            }
        }
        public void Close()
        {
            if (_connection != null)
            {
                if (_connection.State.Equals(ConnectionState.Open))
                {
                    _connection.Close();
                }
            }
        }
        public SQLiteConnection GetConnection() 
        {
            if (_connection != null) 
            {
                return _connection;
            }
            return default(SQLiteConnection);
            
        }
    }
}
