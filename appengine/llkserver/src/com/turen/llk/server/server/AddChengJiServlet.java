package com.turen.llk.server.server;

import java.io.IOException;
import java.util.logging.Logger;

import javax.jdo.PersistenceManager;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.turen.llk.server.domain.ChengJi;
import com.turen.llk.server.util.PMF;

public class AddChengJiServlet extends HttpServlet{

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private static final Logger log = Logger.getLogger(AddChengJiServlet.class.getName());
	private String userId;
	private String userName;
	private String email;
	private String seconds;

	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException,ServletException {
		
		
	}
	public void doPost(HttpServletRequest req, HttpServletResponse resp)
	throws IOException,ServletException {
		req.setCharacterEncoding("UTF-8");
		resp.setCharacterEncoding("UTF-8");
	    resp.setContentType("text/plain");

		userId=req.getParameter("userId");
		userName=req.getParameter("userName");
		email=req.getParameter("email");
		seconds=req.getParameter("seconds");
		PersistenceManager pm = PMF.get().getPersistenceManager();
		ChengJi chengJi=new ChengJi();
		chengJi.setEmail(email);
		chengJi.setUsername(userName);
		chengJi.setSeconds(Integer.parseInt(seconds));
		pm.makePersistent(chengJi);
		//this.doGet(req, resp);
	}
	public String getUserId() {
		return userId;
	}
	public void setUserId(String userId) {
		this.userId = userId;
	}
	public String getUserName() {
		return userName;
	}
	public void setUserName(String userName) {
		this.userName = userName;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public String getSeconds() {
		return seconds;
	}
	public void setSeconds(String seconds) {
		this.seconds = seconds;
	}
}
