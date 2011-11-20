import edu.stanford.smi.protegex.owl.swrl.bridge.SWRLRuleEngineBridge;
import edu.stanford.smi.protegex.owl.model.OWLModel;
import edu.stanford.smi.protegex.owl.swrl.bridge.BridgeFactory;
import edu.stanford.smi.protegex.owl.swrl.bridge.query.*;
import edu.stanford.smi.protege.model.*; 
import edu.stanford.smi.protegex.owl.ProtegeOWL;
import java.util.*;
import java.io.File;
import edu.stanford.smi.protegex.owl.swrl.model.SWRLFactory;
 
// cette classe exécutre la régle "Rule-2" dans le projet local dans protégé "ontologieK.pprj"
public class TestSLAontRules { 

	private static String OWL_FILE = "SLAOnt.owl"; 

	
	public static void main(String[] args)
	{
		
	try{
			File file = new File(OWL_FILE);
			OWLModel owlModel = ProtegeOWL.createJenaOWLModelFromURI(file.toURI().toString());
			/*
			System.out.println("[TARAK] executing query 'testRule1' in "+ OWL_FILE +" ...");
			executeRule1(owlModel);
			System.out.println();
			System.out.println("[TARAK] executing query 'testRule2' in "+ OWL_FILE +" ...");
			executeRule2(owlModel);
			*/
			SWRLFactory factory = new SWRLFactory(owlModel);
			System.out.println("[TARAK] getting enabled rules in "+ OWL_FILE +" ...");
			Collection enabledRules = factory.getEnabledImps();
			Iterator i = enabledRules.iterator();
            while (i.hasNext()) {
                System.out.println("Rule: " + i.next().toString());
			}
			
			System.out.println("[TARAK] applying rules in "+ OWL_FILE +" ...");
			
			
			SWRLRuleEngineBridge bridge = BridgeFactory.createBridge(owlModel);
			bridge.infer(); // Run all the rules and queries
			}
		catch(Throwable e){	System.out.println("An error occured"); e.printStackTrace();}	

	
	
		
	}
}