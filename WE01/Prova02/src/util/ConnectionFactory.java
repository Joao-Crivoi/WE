package util;

import java.sql.Connection;
import java.sql.DriverManager;



/**
 * Atilio Almeida Costa
 * João Victor Crivoi Cesar Souza
 */



public class ConnectionFactory {
    public static Connection getConnection() throws Exception {
        String url = "jdbc:mysql://localhost:3306/java_sw01_prova2";
        String user = "root";
        String password = "@Mysqlt6y777";

        Class.forName("com.mysql.cj.jdbc.Driver");
        return DriverManager.getConnection(url, user, password);
    }
}