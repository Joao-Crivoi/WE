package dao;


import java.sql.*;
import java.util.*;

import model.Customer;

/**
 * Atilio Almeida Costa
 * João Victor Crivoi Cesar Souza
 */

public class CustomerDAO {
	private Connection conn;

    public CustomerDAO(Connection conn) {
        this.conn = conn;
    }

    public void insert(Customer c) throws SQLException {
        String sql = "INSERT INTO customer (cust_name, city, grade, salesman_id) VALUES (?, ?, ?, ?)";
        try (PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, c.getCustName());
            stmt.setString(2, c.getCity());
            stmt.setInt(3, c.getGrade());
            stmt.setInt(4, c.getSalesmanId());
            stmt.executeUpdate();
        }
    }
}
