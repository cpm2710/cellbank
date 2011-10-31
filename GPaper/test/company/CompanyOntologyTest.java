package company;

import java.io.File;

import org.semanticweb.owlapi.apibinding.OWLManager;
import org.semanticweb.owlapi.model.OWLOntology;
import org.semanticweb.owlapi.model.OWLOntologyCreationException;
import org.semanticweb.owlapi.model.OWLOntologyManager;

import com.hp.hpl.jena.db.DBConnection;
import com.hp.hpl.jena.db.IDBConnection;
import com.hp.hpl.jena.ontology.OntDocumentManager;
import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.ontology.OntModelSpec;
import com.hp.hpl.jena.rdf.model.Model;
import com.hp.hpl.jena.rdf.model.ModelFactory;
import com.hp.hpl.jena.rdf.model.ModelMaker;

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

	public static void main(String[] args) throws OWLOntologyCreationException {

		// ensure the JDBC driver class is loaded
		/*try {
			Class.forName(DB_DRIVER);
		} catch (Exception e) {
			System.err.println("Failed to load the driver for the database: "
					+ e.getMessage());
			System.err.println("Have you got the CLASSPATH set correctly?");
		}*/
		//OntDocumentManager.getInstance().addAltEntry( COMPANY_ONT, "file:D:\\WorkSpace\\Orips3WorkSpace\\GPaper\\owl\\company.owl" );
        
		/*ModelMaker maker=
		Model base = maker.createModel(COMPANY_ONT, false);
		OntModelSpec spec = new OntModelSpec(OntModelSpec.OWL_DL_MEM);
		spec.setImportModelMaker(maker);*/
		
		//OntModel m = ModelFactory.createOntologyModel();
		
		//m.read(COMPANY_ONT);
		
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
		OWLOntologyManager manager = OWLManager.createOWLOntologyManager();
		File file = new File("D:\\WorkSpace\\Orips3WorkSpace\\GPaper\\owl\\company.owl");
		OWLOntology companyOntology = manager.loadOntologyFromOntologyDocument(file);
		System.out.println("Loaded ontology: " + companyOntology);
		
	}

	/*public static ModelMaker getRDBMaker(String dbURL, String dbUser,
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
	}*/
}
