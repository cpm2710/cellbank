package ontologypreparation;

import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.rdf.model.AnonId;
import com.hp.hpl.jena.rdf.model.RDFNode;
import com.hp.hpl.jena.rdf.model.Resource;
import com.hp.hpl.jena.rdf.model.Statement;
import com.hp.hpl.jena.rdf.model.StmtIterator;

import edu.sjtu.ist.ontology.OntologyModelFactory;

public class GetOntology {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		OntModel om=OntologyModelFactory.getOntologyModel("http://www.ist.sjtu.edu.cn");
		Resource r=om.getResource("http://www.ist.sjtu.edu.cn/ontologies/company.owl#ChairOrder");
		//r.listProperties();
		StmtIterator si=r.listProperties();
		while(si.hasNext()){
			Statement s=si.next();
			RDFNode node=s.getObject();
			Resource name=s.getObject().asResource();
			System.out.println(name.getLocalName());
			//AnonId ai=name.getId();
			String value=node.asLiteral().toString();
			System.out.println("");
		}
	}

}
