
using MySql.Data.MySqlClient;

namespace Minecraft_Launcher.Class
{
    class MySQL
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public MySQL()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "192.168.0.100";
            database = "skymin_strona";
            uid = "skymin_strona";
            password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            } catch(MySqlException ex) {
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Clone();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }
    }
}
