package controller;


import javax.servlet.ServletException;
import javax.servlet.http.*;

import dao.OrderDAO;
import model.Order;
import util.ConnectionFactory;

import java.io.IOException;
import java.sql.Connection;
import java.sql.Date;

public class OrderServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        Order o = new Order();
        o.setPurchAmt(Double.parseDouble(request.getParameter("purchAmt")));
        o.setOrdDate(Date.valueOf(request.getParameter("ordDate"))); // formato: yyyy-MM-dd
        o.setCustomerId(Integer.parseInt(request.getParameter("customerId")));
        o.setSalesmanId(Integer.parseInt(request.getParameter("salesmanId")));

        try (Connection conn = ConnectionFactory.getConnection()) {
            OrderDAO dao = new OrderDAO(conn);
            dao.insert(o);
            response.sendRedirect("index.html");
        } catch (Exception e) {
            throw new ServletException(e);
        }
    }
}