package com.turen.llk.server.server;

import java.io.IOException;
import java.util.logging.Logger;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class AddChengJiServlet extends HttpServlet{

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private static final Logger log = Logger.getLogger(AddChengJiServlet.class.getName());

	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException,ServletException {
		
		
	}
	public void doPost(HttpServletRequest req, HttpServletResponse resp)
	throws IOException,ServletException {
		
		this.doGet(req, resp);
	}
}
