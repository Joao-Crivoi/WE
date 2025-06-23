package servlet;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Arrays;
import java.util.Optional;

@WebServlet(name = "removeCookieServlet",
        urlPatterns = {"/remove-currency-pair"},
        loadOnStartup = 1)
public class RemoveExchangeRateCookieServlet extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp)
            throws ServletException, IOException {
        Optional<Cookie> cookieOptional = Arrays.stream(req.getCookies())
                                                .filter(c -> ExchangeRatesServlet
                                                        .currencyPairKey.equals(c.getName()))
                                                .findAny();
        if (cookieOptional.isPresent()) {
            Cookie cookie = cookieOptional.get();
            cookie.setMaxAge(0);
            resp.addCookie(cookie);
            resp.sendRedirect(req.getContextPath() + "/currencySelection.html");
        } else {
            resp.setContentType("text/html");
            PrintWriter writer = resp.getWriter();
            writer.write("Currency pair Cookie does not exist on the browser");
        }
    }
}
