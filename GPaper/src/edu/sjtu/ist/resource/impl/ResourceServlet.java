package edu.sjtu.ist.resource.impl;

import javax.servlet.http.HttpServlet;

import com.hp.hpl.jena.ontology.OntModel;

import edu.sjtu.ist.ontology.OntologyModelFactory;

public class ResourceServlet extends HttpServlet {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	@Override
	public void doGet(javax.servlet.http.HttpServletRequest req,
			javax.servlet.http.HttpServletResponse resp) {
		/*System.out.println(req.getRequestURI());
		System.out.println(req.getContextPath());
		System.out.println(req.getLocalName());//这个可以拿到域名
		System.out.println(req.getLocalAddr());
		System.out.println(req.getPathInfo());
		System.out.println(req.getServletPath());
		21:01:09,775 INFO  [STDOUT] /gpaper/shit
21:01:09,778 INFO  [STDOUT] /gpaper
21:01:09,790 INFO  [STDOUT] www.ist.sjtu.edu.cn
21:01:09,791 INFO  [STDOUT] 127.0.0.1
21:01:09,798 INFO  [STDOUT] /shit
21:01:09,798 INFO  [STDOUT] doGET
21:01:18,456 INFO  [STDOUT] /gpaper/shit
21:01:18,457 INFO  [STDOUT] /gpaper
21:01:18,457 INFO  [STDOUT] www.ist.sjtu.edu.cn
21:01:18,457 INFO  [STDOUT] 127.0.0.1
21:01:18,457 INFO  [STDOUT] /shit
21:01:18,457 INFO  [STDOUT] doGET
21:52:47,346 INFO  [STDOUT] /gpaper/shit
21:52:47,347 INFO  [STDOUT] /gpaper
21:52:47,349 INFO  [STDOUT] www.ist.sjtu.edu.cn
21:52:47,350 INFO  [STDOUT] 127.0.0.1
21:52:47,350 INFO  [STDOUT] /shit
21:52:47,350 INFO  [STDOUT] doGET
		*/
		String nameSpace=req.getLocalName();
		String requestURI=req.getRequestURI();
		if(requestURI.contains("?_wadl")){
			requestURI.lastIndexOf("/");
		}else{
			OntModel om=OntologyModelFactory.getOntologyModel(nameSpace);
			String uri="http://"+nameSpace+requestURI;
			om.getResource(uri);
			/*int lastSlashIndex=requestURI.lastIndexOf("/");
			String resourceId=requestURI.substring(lastSlashIndex);
			System.out.println(resourceId);*/
		}
		
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
