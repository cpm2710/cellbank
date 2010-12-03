package com.turen.llk.server.server;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;
import java.util.logging.Logger;

import javax.jdo.PersistenceManager;
import javax.jdo.Query;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.turen.llk.server.domain.ChengJi;
import com.turen.llk.server.util.JSONGenerator;
import com.turen.llk.server.util.PMF;



public class GetPaiHangBangServlet extends HttpServlet {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private static final Logger log = Logger.getLogger(GetPaiHangBangServlet.class.getName());
	private String rating;
	private int level;

	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException,ServletException {
		
		req.setCharacterEncoding("UTF-8");
		resp.setCharacterEncoding("UTF-8");
	    resp.setContentType("text/plain");
	    rating=req.getParameter("level");
		level=Integer.parseInt(rating);
	    PersistenceManager pm = PMF.get().getPersistenceManager();
		try {
			Query query = pm.newQuery("select from com.turen.llk.server.domain.ChengJi where level="+level +
                              " order by miniSeconds asc");
			query.setRange(0, 10);
			List<ChengJi> results = (List<ChengJi>) query.execute();
			String json=JSONGenerator.generate(results);
			PrintWriter writer=resp.getWriter();
			writer.write(json);
			writer.flush();
			writer.close();
		} finally {
			pm.close();
		}		
		
	}
	public void doPost(HttpServletRequest req, HttpServletResponse resp)
	throws IOException,ServletException {
		/*
		 * String username = req.getParameter("username");
		String password = req.getParameter("password");
		User user = new User();
		user.setUsername(username);
		user.setPassword(password);
		log.log(Level.WARNING, "user: "+username+" trying to login");
		PersistenceManager pm = PMF.get().getPersistenceManager();
		try {
			Query query = pm.newQuery(User.class);
			query.setFilter("username == lastNameParam");
			query.declareParameters("String lastNameParam");
			List<User> results = (List<User>) query.execute(username);
			if (results.iterator().hasNext()) {
				user = results.get(0);
			} else {
				user = pm.makePersistent(user);
			}
		} finally {
			pm.close();
		}		
		req.getSession().setAttribute("username", username);
		req.getRequestDispatcher("/index.jsp").forward(req, resp);*/
		
		this.doGet(req, resp);
	}
}