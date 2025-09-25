using Bl_Container.Models;
using MySql.Data.MySqlClient;

namespace Bl_Container.Repository.impl
{
    public class BLRepository: IBLRepository
    {

        private readonly string _connectionString;

        public BLRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int insert(BL dtoBl)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "INSERT INTO BL (Numero, Consignee, Navio) VALUES (@Numero, @Consignee, @Navio)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Numero", dtoBl.Numero);
            cmd.Parameters.AddWithValue("@Consignee", dtoBl.Consignee);
            cmd.Parameters.AddWithValue("@Navio", dtoBl.Navio);

            return cmd.ExecuteNonQuery();
        }

        public BL select(BL dtoBl)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "SELECT * FROM BL WHERE ID = @ID";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", dtoBl.ID);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new BL(
                    reader.GetInt32("ID"),
                    reader.GetString("Numero"),
                    reader.GetString("Consignee"),
                    reader.GetString("Navio")
                );
            }

            return null!;
        }

        public List<BL> selectAll()
        {
            var list = new List<BL>();
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "SELECT * FROM BL";
            using var cmd = new MySqlCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new BL(
                    reader.GetInt32("ID"),
                    reader.GetString("Numero"),
                    reader.GetString("Consignee"),
                    reader.GetString("Navio")
                ));
            }

            return list;
        }

        public int update(BL dtoBl)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "UPDATE BL SET Numero=@Numero, Consignee=@Consignee, Navio=@Navio WHERE ID=@ID";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Numero", dtoBl.Numero);
            cmd.Parameters.AddWithValue("@Consignee", dtoBl.Consignee);
            cmd.Parameters.AddWithValue("@Navio", dtoBl.Navio);
            cmd.Parameters.AddWithValue("@ID", dtoBl.ID);

            return cmd.ExecuteNonQuery();
        }

        public int delete(BL dtoBl)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            var sql = "DELETE FROM BL WHERE ID=@ID";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", dtoBl.ID);

            return cmd.ExecuteNonQuery();
        }
    }
}
