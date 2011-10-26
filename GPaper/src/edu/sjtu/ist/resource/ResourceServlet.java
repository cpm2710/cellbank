package edu.sjtu.ist.resource;

import javax.servlet.http.HttpServlet;

public class ResourceServlet extends HttpServlet {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	@Override
	public void doGet(javax.servlet.http.HttpServletRequest req,
			javax.servlet.http.HttpServletResponse resp) {
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
