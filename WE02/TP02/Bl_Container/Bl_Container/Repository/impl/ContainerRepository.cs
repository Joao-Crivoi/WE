using Bl_Container.Models;
using MySql.Data.MySqlClient;


namespace Bl_Container.Repository
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly string _connectionString;

        public ContainerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int insert(Container dtoContainer)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "INSERT INTO Container (Numero, Tipo, Tamanho, IDBl) VALUES (@Numero, @Tipo, @Tamanho, @IDBl)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Numero", dtoContainer.Numero);
            cmd.Parameters.AddWithValue("@Tipo", dtoContainer.Tipo);
            cmd.Parameters.AddWithValue("@Tamanho", dtoContainer.Tamanho);
            cmd.Parameters.AddWithValue("@IDBl", dtoContainer.IDBl);

            return cmd.ExecuteNonQuery();
        }

        public Container select(Container dtoContainer)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "SELECT * FROM Container WHERE ID=@ID";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", dtoContainer.ID);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                dtoContainer.Numero = reader.GetString("Numero");
                dtoContainer.Tipo = reader.GetString("Tipo");
                dtoContainer.Tamanho = reader.GetInt32("Tamanho");
                dtoContainer.IDBl = reader.GetInt32("IDBl");
                return dtoContainer;
            }

            return new Container();
        }

        public List<Container> selectAll()
        {
            var list = new List<Container>();
            using var con = new MySqlConnection(_connectionString);
            con.Open();
            var sql = "SELECT * FROM Container";
            using var cmd = new MySqlCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Container
                {
                    ID = reader.GetInt32("ID"),
                    Numero = reader.GetString("Numero"),
                    Tipo = reader.GetString("Tipo"),
                    Tamanho = reader.GetInt32("Tamanho"),
                    IDBl = reader.GetInt32("IDBl")
                });
            }

            return list;
        }

        public int update(Container dtoContainer)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "UPDATE Container SET Numero=@Numero, Tipo=@Tipo, Tamaho=@Tamaho, IDBl=@IDBl WHERE ID=@ID";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Numero", dtoContainer.Numero);
            cmd.Parameters.AddWithValue("@Tipo", dtoContainer.Tipo);
            cmd.Parameters.AddWithValue("@Tamaho", dtoContainer.Tamanho);
            cmd.Parameters.AddWithValue("@IDBl", dtoContainer.IDBl);
            cmd.Parameters.AddWithValue("@ID", dtoContainer.ID);

            return cmd.ExecuteNonQuery();
        }

        public int delete(Container dtoContainer)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "DELETE FROM Container WHERE ID=@ID";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", dtoContainer.ID);

            return cmd.ExecuteNonQuery();
        }

        public System.ComponentModel.Container select(System.ComponentModel.Container dtoContainer)
        {
            throw new NotImplementedException();
        }
    }
}
