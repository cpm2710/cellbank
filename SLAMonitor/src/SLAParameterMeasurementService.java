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


public class SLAParameterMeasurementService extends Thread {

	
	private SWRLFactory factory;
	private SWRLRuleEngineBridge bridge;
	private String parameterName;
	private int frequency;
	OWLModel owlModel;
	
	public SLAParameterMeasurementService(SWRLFactory fact, SWRLRuleEngineBridge brid, OWLModel owl, String name, int freq)
	{

		factory = fact;
		bridge = brid;
		parameterName = name;
		frequency = freq;
		owlModel=owl;
	}
	
	private Object calculateSLAParameter()
	{
		   float res=-1;
		   try{
			factory.disableAll();
			factory.getImp("Calculate"+parameterName).enable();
			MonitoringMain.display("[SLAont] applying rule Calculate"+parameterName+ "...");
			bridge.infer();
			Result result=bridge.getQueryResult("Calculate"+parameterName);
			return result.getDatatypeValue("?x").toString();
			}
			catch(Throwable e){	e.printStackTrace();return -1;}	
	}
	
	
	public void run() 
	{
		try {
						while(true){
						
						Thread.sleep(frequency);
						synchronized(factory){
						synchronized(owlModel){
                        MonitoringMain.display("\t"+calculateSLAParameter());

							}
							}}
                     } catch ( Exception x ) {
                         x.printStackTrace();
                     }
	}


}