package controller;



import javax.servlet.ServletException;
import javax.servlet.http.*;

import dao.SalesmanDAO;
import model.Salesman;
import util.ConnectionFactory;

import java.io.IOException;
import java.sql.Connection;


/**
 * Atilio Almeida Costa
 * João Victor Crivoi Cesar Souza
 */


public class SalesmanServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        Salesman s = new Salesman();
        s.setName(request.getParameter("name"));
        s.setCity(request.getParameter("city"));
        s.setCommission(Double.parseDouble(request.getParameter("commission")));

        try (Connection conn = ConnectionFactory.getConnection()) {
            SalesmanDAO dao = new SalesmanDAO(conn);
            dao.insert(s);
            response.sendRedirect("index.html");
        } catch (Exception e) {
            throw new ServletException(e);
        }
    }
}
