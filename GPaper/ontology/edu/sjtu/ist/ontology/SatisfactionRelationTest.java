package edu.sjtu.ist.ontology;

import org.mindswap.pellet.jena.PelletReasonerFactory;

import com.hp.hpl.jena.ontology.ObjectProperty;
import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.rdf.model.ModelFactory;

public class SatisfactionRelationTest {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		String ont = "D:\\JavaWorld\\WorkSpaces\\GPaper\\GPaper\\owl\\ist\\companyrdf.owl.xml";

		OntModel model = ModelFactory.createOntologyModel( PelletReasonerFactory.THE_SPEC, null );
		model.read( ont );
		//ObjectProperty sibling = model.getObjectProperty( "http://www.ist.sjtu.edu.cn/ontologies/company.owl" + "#sibling" );
	}

}
