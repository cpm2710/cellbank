package company;

import com.hp.hpl.jena.db.DBConnection;
import com.hp.hpl.jena.db.IDBConnection;
import com.hp.hpl.jena.ontology.OntClass;
import com.hp.hpl.jena.ontology.OntDocumentManager;
import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.ontology.OntModelSpec;
import com.hp.hpl.jena.ontology.OntResource;
import com.hp.hpl.jena.rdf.model.Model;
import com.hp.hpl.jena.rdf.model.ModelFactory;
import com.hp.hpl.jena.rdf.model.ModelMaker;
import com.hp.hpl.jena.util.iterator.ExtendedIterator;

public class CompanyOntologyTest {

	/**
	 * @param args
	 */
	public static final String DB_DRIVER = "com.mysql.jdbc.Driver";
	public static final String DB_URL = "jdbc:mysql://localhost:3306/jenatest";
	public static final String DB_USER = "root";
	public static final String DB_PASSWD = "root";
	public static final String DB_TYPE = "mySQL";
	public static final Boolean cleanDB = true;
	public static final String COMPANY_ONT = "urn:x-hp-jena:company";

	public static void main(String[] args) {

		// ensure the JDBC driver class is loaded
		/*try {
			Class.forName(DB_DRIVER);
		} catch (Exception e) {
			System.err.println("Failed to load the driver for the database: "
					+ e.getMessage());
			System.err.println("Have you got the CLASSPATH set correctly?");
		}*/
		OntDocumentManager.getInstance().addAltEntry( COMPANY_ONT, "file:D:\\WorkSpace\\Orips3WorkSpace\\GPaper\\owl\\companyrdf.owl" );
        
		ModelMaker maker=ModelFactory.createMemModelMaker();
		Model base = maker.createModel(COMPANY_ONT, false);
		OntModelSpec spec = new OntModelSpec(OntModelSpec.OWL_DL_MEM);
		spec.setImportModelMaker(maker);
		
		OntModel m = ModelFactory.createOntologyModel(spec, base);
		
		m.read(COMPANY_ONT);
		//OntClass cc2=m.getOntClass("http://www.ist.sjtu.edu.cn/ontologies/company.owl#ChairOrder");
		ExtendedIterator<OntClass> itereator=m.listClasses();
		while(itereator.hasNext()){
			OntClass oc=(OntClass)itereator.next();
			System.out.println(oc.getURI());
		}
		OntResource resource=m.getOntResource("http://www.ist.sjtu.edu.cn/ontologies/company.owl#ChairOrder");
		OntClass cc=m.getOntClass("http://www.ist.sjtu.edu.cn/ontologies/company.owl#BuyOrder");
		//cc2.listInstances();
		cc.listInstances();
		// Create a model maker object
		/*ModelMaker maker = getRDBMaker(DB_URL, DB_USER, DB_PASSWD, DB_TYPE);

		Model base = maker.createModel(COMPANY_ONT, false);

		OntModelSpec spec = new OntModelSpec(OntModelSpec.OWL_DL_MEM);
		spec.setImportModelMaker(maker);

		// now we plug that base model into an ontology model that also uses
		// the given model maker to create storage for imported models
		OntModel m = ModelFactory.createOntologyModel(spec, base);

		// now load the source document, which will also load any imports
		m.read(COMPANY_ONT);*/

		//maker = getRDBMaker(DB_URL, DB_USER, DB_PASSWD, DB_TYPE);
		
		//base = maker.createModel( COMPANY_ONT, false );
		
	}

	public static ModelMaker getRDBMaker(String dbURL, String dbUser,
			String dbPw, String dbType) {
		try {
			// Create database connection
			IDBConnection conn = new DBConnection(dbURL, dbUser, dbPw, dbType);

			// do we need to clean the database?
			if (cleanDB) {
				conn.cleanDB();
			}

			// Create a model maker object
			return ModelFactory.createModelRDBMaker(conn);
		} catch (Exception e) {
			e.printStackTrace();
			System.exit(1);
		}

		return null;
	}
}
