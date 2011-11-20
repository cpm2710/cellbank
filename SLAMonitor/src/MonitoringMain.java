import edu.stanford.smi.protegex.owl.swrl.bridge.SWRLRuleEngineBridge;
import edu.stanford.smi.protegex.owl.model.*;
import edu.stanford.smi.protegex.owl.swrl.bridge.BridgeFactory;
import edu.stanford.smi.protegex.owl.swrl.bridge.query.*;
import edu.stanford.smi.protege.model.*; 
import edu.stanford.smi.protegex.owl.ProtegeOWL;
import edu.stanford.smi.protegex.owl.swrl.parser.SWRLParser;
import java.util.*;
import java.io.File;
import edu.stanford.smi.protegex.owl.swrl.model.SWRLFactory;
import com.hp.hpl.jena.util.FileUtils;
import edu.stanford.smi.protegex.owl.jena.JenaOWLModel;
import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

 
// cette classe exécutre la régle "Rule-2" dans le projet local dans protégé "ontologieK.pprj"
public class MonitoringMain extends JPanel    implements ActionListener { 

	
	private static String OWL_FILE = "SLAOnt.owl"; 
	OWLModel owlModel;
	SWRLFactory factory;
	SWRLRuleEngineBridge bridge;
	static private final String newline = "\n";
    JButton openOwlFile, startMonitoring;
    public static JTextArea log;
    JFileChooser fc;
	String slaOntPrefix = "slaont:";
	
	// Le constructeur initialise les composants graphiques nécessaires pour l'affichage
	public MonitoringMain() {
        super(new BorderLayout());

        //Create the log first, because the action listeners
        //need to refer to it.
        log = new JTextArea(40,150);
        log.setMargin(new Insets(5,5,5,5));
        log.setEditable(false);
        JScrollPane logScrollPane = new JScrollPane(log);

        //Create a file chooser
        fc = new JFileChooser(".");

        //Uncomment one of the following lines to try a different
        //file selection mode.  The first allows just directories
        //to be selected (and, at least in the Java look and feel,
        //shown).  The second allows both files and directories
        //to be selected.  If you leave these lines commented out,
        //then the default mode (FILES_ONLY) will be used.
        //
        //fc.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
        //fc.setFileSelectionMode(JFileChooser.FILES_AND_DIRECTORIES);

        //Create the open button.  We use the image from the JLF
        //Graphics Repository (but we extracted it from the jar).
        openOwlFile = new JButton("Open SLAont File...");
        openOwlFile.addActionListener(this);

        //Create the save button.  We use the image from the JLF
        //Graphics Repository (but we extracted it from the jar).
        startMonitoring = new JButton("Start Monitoring...");
        startMonitoring.addActionListener(this);

        //For layout purposes, put the buttons in a separate panel
        JPanel buttonPanel = new JPanel(); //use FlowLayout
        buttonPanel.add(openOwlFile);
        buttonPanel.add(startMonitoring);

        //Add the buttons and the log to this panel.
        add(buttonPanel, BorderLayout.PAGE_START);
        add(logScrollPane, BorderLayout.CENTER);
		startMonitoring.setEnabled(false);
    }
	
	// gestion des évènements graphiques (openOWLFile et startMonitoring)
	public void actionPerformed(ActionEvent e) {

        //Handle open button action.
        if (e.getSource() == openOwlFile) {
			log.append("Please wait while loading SLAont ontology..." + newline);
            int returnVal = fc.showOpenDialog(this);
			
            if (returnVal == JFileChooser.APPROVE_OPTION) {
                
				try{
					File file = fc.getSelectedFile();
					owlModel = ProtegeOWL.createJenaOWLModelFromURI(file.toURI().toString());
					slaOntPrefix = owlModel.getResourceNameForURI("http://www.laas.fr/~kchaari/slaOnt/SLAont.owl");
					if ((slaOntPrefix==null) || (slaOntPrefix.equals("")))
					{
						log.append("[FATAL ERROR] You have to import SLAOnt ontology from http://www.laas.fr/~kchaari/slaOnt/SLAont.owl !");
					}
					else
					{
						edu.stanford.smi.protegex.owl.swrl.bridge.builtins.slaActions.SWRLBuiltInLibraryImpl.setOWLModel(owlModel, OWL_FILE, slaOntPrefix);
						// créer une instance de SWRLFactory qui sera responsable de la gestion des règles SWRL (activation, désactivation, création, suppression...)
						factory = new SWRLFactory(owlModel);
						// créer une instance de SWRLRuleEngineBridge qui sera responsable de l'exécution des règles et de la récupération du résultat de cette exécution
						bridge = BridgeFactory.createBridge(owlModel);
						
		                //This is where a real application would open the file.
		                log.append(file.getName() + " opened successfully." + newline);
						startMonitoring.setEnabled(true);
					}
				}
				catch(Throwable err)
				{
					log.append("Error occured when opening OWL file: " + err.getMessage()+ newline);
				}
            } else {
                log.append("Open command cancelled by user." + newline);
            }
            log.setCaretPosition(log.getDocument().getLength());

        //Handle save button action.
        } else if (e.getSource() == startMonitoring) {
          startMonitoring();
        }
		log.setCaretPosition(log.getDocument().getLength());
    }
	
	private void save(OWLModel owl, String fileName)
	{
		Collection errors = new ArrayList();
		((JenaOWLModel) owl).save(new File(fileName).toURI(), FileUtils.langXMLAbbrev, errors);
		//System.out.println("File saved with " + errors.size() + " errors.");
	}
	/*
	Cette métode récupère les détails (le nom de la métrique et sa fréquence de mesure) de toutes les métriques définies dans l'ontologie. 
	Pour chaque métrique, on créé une règle SWRL qui permet de récupérer sa mesure et nous instancions un MetricMeasurementService.
	*/
	private void createMetricMeasurementServices()
	{
	
			try{
			factory.disableAll();
			factory.getImp(slaOntPrefix+"getMetricsDetails").enable();
			//display("[SLAont] applying getSLAParameterDetails in "+ OWL_FILE +" ...");
			bridge.infer();
			Result result = bridge.getQueryResult(slaOntPrefix+"getMetricsDetails");
			while (result.hasNext()) {
				String metricName = result.getObjectValue("?"+slaOntPrefix+"metric").getIndividualName();
				int frequency = result.getDatatypeValue("?"+slaOntPrefix+"frequency").getInt();
				try{
				factory.createImp(metricName+"MeasurementRule", "slaActions:getMetric("+metricName+",?value)"+SWRLParser.IMP_CHAR+"query:select(?value)");
				}
				catch(Exception e){/*ici ça veut dire que la règle existe déja*/log.append(e.getMessage());}
				
				Thread measurementThread = new MetricMeasurementService(factory, bridge, metricName, frequency);
				measurementThread.start();
				result.next();
			} // while
			//display("[SLAont] Measurement Threads created" + FREQUENCY);
			save(owlModel, OWL_FILE);
			//display("_____________________________________________________________________________________");
			
			}
			catch(Throwable e){	e.printStackTrace();log.append(e.getMessage());}	
	}
	
	/*
	Cette métode récupère les détails (le nom du SLaparameter et sa fréquence d'agrégation)  de tous les SLAparameter définis dans l'ontologie. 
	Pour chaque SLAparameter, on créé une règle SWRL qui permet de calculer sa valeur et nous instancions un SLAParameterMeasurementService.
	*/
	private void createSLAParameterMeasurementServices()
	{
			try{
			factory.disableAll();
			factory.getImp(slaOntPrefix+"getSLAParametersDetails").enable();
			//display("[SLAont] applying getSLAParameterDetails in "+ OWL_FILE +" ...");
			bridge.infer();
			Result result = bridge.getQueryResult(slaOntPrefix+"getSLAParametersDetails");
			while (result.hasNext()) {
				String parameterName = result.getObjectValue("?"+slaOntPrefix+"p").getIndividualName();
				int frequency = result.getDatatypeValue("?"+slaOntPrefix+"frequency").getInt();
				try{
				System.out.println("Creating rule: Calculate"+parameterName);
				//System.out.println(slaOntPrefix+"definedByMetric("+parameterName+", ?metric)  "+SWRLParser.AND_CHAR+" "+slaOntPrefix+"hasFunction("+parameterName+", ?f)  "+SWRLParser.AND_CHAR+"  "+slaOntPrefix+"hasImplementingClass(?f, ?impClass)  "+SWRLParser.AND_CHAR+" slaActions:agregateMetric(?metric, ?impClass, ?x)"+SWRLParser.IMP_CHAR+"slaActions:setSLAParameterValue("+parameterName+", ?x)"+SWRLParser.AND_CHAR+"query:select(?x)");
				factory.createImp("Calculate"+parameterName, slaOntPrefix+"definedByMetric("+parameterName+", ?metric)  "+SWRLParser.AND_CHAR+" "+slaOntPrefix+"hasFunction("+parameterName+", ?f)  "+SWRLParser.AND_CHAR+"  "+slaOntPrefix+"hasImplementingClass(?f, ?impClass)  "+SWRLParser.AND_CHAR+" slaActions:agregateMetric(?metric, ?impClass, ?x)"+SWRLParser.IMP_CHAR+"slaActions:setSLAParameterValue("+parameterName+", ?x)"+SWRLParser.AND_CHAR+"query:select(?x)");
				}
				catch(Exception e){/*ici ça veut dire que la règle existe déja*/}
				
				Thread aggregationThread = new SLAParameterMeasurementService(factory, bridge, owlModel, parameterName, frequency);
				aggregationThread.start();
				result.next();
			} // while
			//display("[SLAont] Measurement Threads created" + FREQUENCY);
			save(owlModel, OWL_FILE);
			//display("_____________________________________________________________________________________");
			
			}
			catch(Throwable e){	e.printStackTrace();log.append(e.getMessage());}	
	}
	
	/*
	Cette métode récupère les détails (le nom de la règle SWRL et sa fréquence de vérification)  de tous les prédicats définis dans l'ontologie. 
	Pour chaque prédicat, nous instancions un ObligationMonitoringService.
	*/
	private void createObligationMonitoringServices()
	{
		try{
			factory.disableAll();
			factory.getImp(slaOntPrefix+"getObligationDetails").enable();
			//display("[SLAont] applying getSLAParameterDetails in "+ OWL_FILE +" ...");
			bridge.infer();
			Result result = bridge.getQueryResult(slaOntPrefix+"getObligationDetails");
			while (result.hasNext()) {
				//String predicateName = result.getObjectValue("?p").getIndividualName();
				String ruleName = result.getObjectValue("?"+slaOntPrefix+"r").getIndividualName();
				int frequency = result.getDatatypeValue("?"+slaOntPrefix+"frequency").getInt();
				Thread aggregationThread = new ObligationMonitoringService(factory, bridge, owlModel, ruleName, frequency);
				aggregationThread.start();
				result.next();
			} // while
			//display("[SLAont] Measurement Threads created" + FREQUENCY);
			save(owlModel, OWL_FILE);
			//display("_____________________________________________________________________________________");
			
			}
			catch(Throwable e){	e.printStackTrace();log.append(e.getMessage());}	
	}
	
	private void startMonitoring()
	{
		display("[SLAont] Creating Metric Measurement, SLAParameter Measurement and Obligation monitoring Services...");
		createMetricMeasurementServices();
		createSLAParameterMeasurementServices();
		createObligationMonitoringServices();
	}
	
	public static void main(String[] args)
	{
		UIManager.put("swing.boldMetal", Boolean.FALSE); 
        createAndShowGUI();
	}
	
	public static void display(String message)
	{
		synchronized(log){
			log.append(message + newline);
			log.setCaretPosition(log.getDocument().getLength());
		}
	}
	
	private static void createAndShowGUI() {
        //Create and set up the window.
        JFrame frame = new JFrame("SLAOnt: SLA Obligation monitoring");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        //Add content to the window.
        frame.add(new MonitoringMain());

        //Display the window.
        frame.pack();
        frame.setVisible(true);
    }
}