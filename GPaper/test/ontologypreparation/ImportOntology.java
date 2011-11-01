package ontologypreparation;

import java.io.File;

import edu.sjtu.ist.ontology.OntologyModelFactory;

public class ImportOntology {
	public static void main(String args[]){
		String COMPANY_ONT="";
		File dot=new File(".");
		System.out.println(dot.getAbsolutePath());
		
		String filePath="file:.\\owl\\companyrdf.owl";
		OntologyModelFactory.loadOntologyIntoDBFromFile("http://www.ist.sjtu.edu.cn", filePath);
	}
}
