import edu.stanford.smi.protegex.owl.swrl.bridge.SWRLRuleEngineBridge;
import edu.stanford.smi.protegex.owl.model.*;
import edu.stanford.smi.protegex.owl.swrl.bridge.BridgeFactory;
import edu.stanford.smi.protegex.owl.swrl.bridge.query.*;
import edu.stanford.smi.protege.model.*; 
import edu.stanford.smi.protegex.owl.ProtegeOWL;
import java.util.*;
import java.io.File;
import edu.stanford.smi.protegex.owl.swrl.model.SWRLFactory;
import com.hp.hpl.jena.util.FileUtils;


public class ObligationMonitoringService extends Thread {

	
	private SWRLFactory factory;
	private SWRLRuleEngineBridge bridge;
	private String ruleName;
	private int frequency;
	OWLModel owlModel;
	
	public ObligationMonitoringService(SWRLFactory fact, SWRLRuleEngineBridge brid, OWLModel owl, String name, int freq)
	{

		factory = fact;
		bridge = brid;
		ruleName = name;
		frequency = freq;
		owlModel=owl;
	}
	
	private void evaluatePredicate()
	{
		   
		   try{
			factory.disableAll();
			MonitoringMain.display("[SLAont] applying "+ ruleName +" ...");
			factory.getImp(ruleName).enable();
			bridge.infer();
			MonitoringMain.display("_____________________________________________________________________________________");
			
			}
			catch(Throwable e){	e.printStackTrace();}	
	}
	
	
	public void run() 
	{
		try {
							while(true){
							Thread.sleep(frequency);
							synchronized(factory){
							synchronized(owlModel){
                         evaluatePredicate();
							}
							}}
                     } catch ( Exception x ) {
                          x.printStackTrace();
                      }
	}


}