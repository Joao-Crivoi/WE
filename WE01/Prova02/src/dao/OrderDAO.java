package dao;


import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import model.Order;

public class OrderDAO {
    private Connection conn;

    public OrderDAO(Connection conn) {
        this.conn = conn;
    }

    public void insert(Order o) throws SQLException {
        String sql = "INSERT INTO orders (purch_amt, ord_date, customer_id, salesman_id) VALUES (?, ?, ?, ?)";
        try (PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setDouble(1, o.getPurchAmt());
            stmt.setDate(2, o.getOrdDate());
            stmt.setInt(3, o.getCustomerId());
            stmt.setInt(4, o.getSalesmanId());
            stmt.executeUpdate();
        }
    }
    
}
