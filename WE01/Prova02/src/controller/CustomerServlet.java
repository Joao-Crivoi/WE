package controller;

import java.io.IOException;
import java.sql.Connection;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import dao.CustomerDAO;
import model.Customer;
import util.ConnectionFactory;

/**
 * Atilio Almeida Costa
 * João Victor Crivoi Cesar Souza
 */
@WebServlet("/CustomerServlet")
public class CustomerServlet extends HttpServlet {
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        Customer c = new Customer();
        c.setCustName(request.getParameter("name"));
        c.setCity(request.getParameter("city"));
        c.setGrade(Integer.parseInt(request.getParameter("grade")));
        c.setSalesmanId(Integer.parseInt(request.getParameter("salesmanId")));

        try (Connection conn = ConnectionFactory.getConnection()) {
            CustomerDAO dao = new CustomerDAO(conn);
            dao.insert(c);
            response.sendRedirect("index.html");
        } catch (Exception e) {
            throw new ServletException(e);
        }
    }
}
