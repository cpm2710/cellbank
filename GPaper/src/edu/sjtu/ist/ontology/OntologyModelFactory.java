package edu.sjtu.ist.ontology;

import com.hp.hpl.jena.db.DBConnection;
import com.hp.hpl.jena.db.IDBConnection;
import com.hp.hpl.jena.ontology.OntDocumentManager;
import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.ontology.OntModelSpec;
import com.hp.hpl.jena.rdf.model.Model;
import com.hp.hpl.jena.rdf.model.ModelFactory;
import com.hp.hpl.jena.rdf.model.ModelMaker;

public class OntologyModelFactory {
	public static final String DB_DRIVER = "com.mysql.jdbc.Driver";
	public static final String DB_URL = "jdbc:mysql://localhost:3306/gpaper_ont";
	public static final String DB_USER = "root";
	public static final String DB_PASSWD = "root";
	public static final String DB_TYPE = "mySQL";
	public static final Boolean cleanDB = true;
	public static final String COMPANY_ONT = "urn:x-hp-jena:company";
	static{
		try {
			Class.forName(DB_DRIVER);
		} catch (Exception e) {
			System.err.println("Failed to load the driver for the database: "
					+ e.getMessage());
			System.err.println("Have you got the CLASSPATH set correctly?");
		}
		
	}
	public static OntModel getOntologyModel(String nameSpace){
		ModelMaker maker = getRDBMaker(DB_URL, DB_USER, DB_PASSWD, DB_TYPE);
		Model base=maker.getModel(nameSpace);
		OntModelSpec spec = new OntModelSpec(OntModelSpec.OWL_DL_MEM);
		spec.setImportModelMaker(maker);
		OntModel m = ModelFactory.createOntologyModel(spec, base);
		return m;
	}
	public static void loadOntologyIntoDBFromFile(String nameSpace,String filePath){
		OntDocumentManager.getInstance().addAltEntry( nameSpace, filePath );
		ModelMaker maker = getRDBMaker(DB_URL, DB_USER, DB_PASSWD, DB_TYPE);

		Model base = maker.createModel(nameSpace, false);

		OntModelSpec spec = new OntModelSpec(OntModelSpec.OWL_DL_MEM);
		spec.setImportModelMaker(maker);
		OntModel m = ModelFactory.createOntologyModel(spec, base);

		// now load the source document, which will also load any imports
		m.read(nameSpace);
	}
	private static ModelMaker getRDBMaker(String dbURL, String dbUser,
			String dbPw, String dbType) {
		try {
			// Create database connection
			IDBConnection conn = new DBConnection(dbURL, dbUser, dbPw, dbType);

			// do we need to clean the database?
			/*if (cleanDB) {
				conn.cleanDB();
			}*/

			// Create a model maker object
			return ModelFactory.createModelRDBMaker(conn);
		} catch (Exception e) {
			e.printStackTrace();
			System.exit(1);
		}

		return null;
	}
}
