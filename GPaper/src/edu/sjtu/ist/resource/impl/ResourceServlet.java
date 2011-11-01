package edu.sjtu.ist.resource.impl;

import javax.servlet.http.HttpServlet;

public class ResourceServlet extends HttpServlet {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	@Override
	public void doGet(javax.servlet.http.HttpServletRequest req,
			javax.servlet.http.HttpServletResponse resp) {
		System.out.println(req.getRequestURI());
		System.out.println(req.getContextPath());
		System.out.println(req.getLocalName());//这个可以拿到域名
		System.out.println(req.getLocalAddr());
		System.out.println(req.getPathInfo());
		System.out.println(req.getServletPath());
		
		System.out.println("doGET");
	}
	@Override
	public void doPut(javax.servlet.http.HttpServletRequest req,
			javax.servlet.http.HttpServletResponse resp) {
		System.out.println("doput");
	}
	@Override
	public void doPost(javax.servlet.http.HttpServletRequest req,
			javax.servlet.http.HttpServletResponse resp) {
		System.out.println("dopost");
	}
	@Override
	public void doDelete(javax.servlet.http.HttpServletRequest req,
			javax.servlet.http.HttpServletResponse resp) {
		System.out.println("dodELETE");
	}
}
