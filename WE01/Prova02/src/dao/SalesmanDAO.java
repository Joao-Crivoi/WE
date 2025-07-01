package dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import model.Salesman;

/**
 * Atilio Almeida Costa
 * João Victor Crivoi Cesar Souza
 */


public class SalesmanDAO {
    private Connection conn;

    public SalesmanDAO(Connection conn) {
        this.conn = conn;
    }

    public void insert(Salesman s) throws SQLException {
        String sql = "INSERT INTO salesman (name, city, commission) VALUES (?, ?, ?)";
        try (PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, s.getName());
            stmt.setString(2, s.getCity());
            stmt.setDouble(3, s.getCommission());
            stmt.executeUpdate();
        }
    }
}


