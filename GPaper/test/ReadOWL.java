import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;

import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.rdf.model.ModelFactory;


public class ReadOWL {

	/**
	 * @param args
	 * @throws FileNotFoundException 
	 */
	public static void main(String[] args) throws FileNotFoundException {
		// TODO Auto-generated method stub
		OntModel m = ModelFactory.createOntologyModel();  
		File myFile = new File("D:/WorkSpace/Orips3WorkSpace/GPaper/owl/wsgp2.owl");
		System.out.println(myFile.getAbsolutePath());
		m.read(new FileInputStream(myFile), ""); 
		
	}

}
