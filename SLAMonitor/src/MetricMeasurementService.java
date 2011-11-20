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


public class MetricMeasurementService extends Thread {

	
	private SWRLFactory factory;
	private SWRLRuleEngineBridge bridge;
	private String metricName;
	private int frequency;
	
	public MetricMeasurementService(SWRLFactory fact, SWRLRuleEngineBridge brid, String name, int freq)
	{

		factory = fact;
		bridge = brid;
		metricName = name;
		frequency = freq;
	}
	
	private Object metricMeasurement()
	{
		   float res=-1;
		   try{
			factory.disableAll();
			factory.getImp(metricName+"MeasurementRule").enable();
			MonitoringMain.display("[SLAont] applying "+ metricName+"MeasurementRule ...");
			bridge.infer();
			Result result=bridge.getQueryResult(metricName+"MeasurementRule");
			return result.getDatatypeValue("?value").toString();
			}
			catch(Throwable e){	e.printStackTrace();return -1;}	
	}
	
	public void run() 
	{
		try {
							while(true){
							synchronized(factory){
							MonitoringMain.display(""+metricMeasurement());
                          }
							Thread.sleep(frequency);
							}
                      } catch ( Exception x ) {
                         x.printStackTrace();
                     }
	}


}