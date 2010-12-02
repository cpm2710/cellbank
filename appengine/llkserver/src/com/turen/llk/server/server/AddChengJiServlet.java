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
	private String userName;
	private String email;
	private String seconds;
	private String xiaoNeiId;
	private String headUrl;
	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException,ServletException {
		
		
	}
	public void doPost(HttpServletRequest req, HttpServletResponse resp)
	throws IOException,ServletException {
		req.setCharacterEncoding("UTF-8");
		resp.setCharacterEncoding("UTF-8");
	    resp.setContentType("text/plain");

		userName=req.getParameter("userName");
		email=req.getParameter("email");
		seconds=req.getParameter("miniSeconds");
		headUrl=req.getParameter("headUrl");
		if(req.getParameter("xiaoNeiId")!=null){
			xiaoNeiId=req.getParameter("xiaoNeiId");			
		}
		PersistenceManager pm = PMF.get().getPersistenceManager();
		ChengJi chengJi=new ChengJi();
		chengJi.setEmail(email);
		chengJi.setUsername(userName);
		chengJi.setMiniSeconds(Long.parseLong(seconds));
		chengJi.setXiaoNeiId(xiaoNeiId);
		chengJi.setHeadUrl(headUrl);
		pm.makePersistent(chengJi);
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
