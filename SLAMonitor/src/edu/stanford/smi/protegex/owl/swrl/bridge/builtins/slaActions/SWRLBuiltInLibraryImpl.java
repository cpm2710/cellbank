    /*
     * This is my Fun
     */

    
    package edu.stanford.smi.protegex.owl.swrl.bridge.builtins.slaActions;
    
    import edu.stanford.smi.protegex.owl.swrl.bridge.*;
	import edu.stanford.smi.protegex.owl.swrl.bridge.builtins.*;
    import edu.stanford.smi.protegex.owl.swrl.bridge.exceptions.*;
	import com.hp.hpl.jena.util.FileUtils;
	import edu.stanford.smi.protegex.owl.jena.JenaOWLModel;
	import edu.stanford.smi.protegex.owl.ProtegeOWL;
	import javax.swing.JOptionPane;
	import javax.swing.JFileChooser;
	import java.lang.reflect.Method;
  
    import org.apache.commons.lang.StringUtils;
	import java.io.File;
   
    import java.util.*;
//    import java.util.regex.*;
	import edu.stanford.smi.protegex.owl.model.*;
    

    public class SWRLBuiltInLibraryImpl extends SWRLBuiltInLibrary
    {
      private static String SWRLBLibraryName = "SLAOntActions";
    
      private static String SWRLBPrefix = "slaActions:";
	  private static String SLAONT_PREFIX = "slaont:";
	  private static OWLModel owlModel = null;
	  private static String OWL_File = "slaOnt.owl";
    
      private static String SWRLB_DISSEMINATE_VIOLATION = SWRLBPrefix + "disseminateViolation";
      private static String SWRLB_GET_METRIC = SWRLBPrefix + "getMetric";
      private static String SWRLB_AGREGATE_METRIC = SWRLBPrefix + "agregateMetric";
	  private static String SWRLB_SET_SLAPARAMETER_VALUE = SWRLBPrefix + "setSLAParameterValue";
	  
	  private static String MESSAGE="";


      private static Hashtable metrics = new Hashtable();

	  
    
   public SWRLBuiltInLibraryImpl() 
      { 
        super(SWRLBLibraryName); 
    
        
      } // SWRLBuiltInLibraryImpl
    
      public void reset() {}
	  
	  public static void setOWLModel(OWLModel owl, String owlFile, String slaontPrefix)
	  {
		owlModel = owl;
		OWL_File = owlFile;
		SLAONT_PREFIX = slaontPrefix;
		System.out.println("[SWRLBuiltInLibraryImpl] OWL Model Successfully set in SWRLBuiltInLibraryImpl");
	  }
     

	public boolean getMetric(List<Argument> arguments) throws BuiltInException
      {
        
    
        //SWRLBuiltInUtil.checkNumberOfArgumentsEqualTo(1, arguments.size());
        String metricName = SWRLBuiltInUtil.getArgumentAsAnIndividualName(0, arguments);

	if (!metrics.containsKey(metricName))
	{
		metrics.put(metricName, new Vector());
	}
	Vector metricValues = (Vector) metrics.get(metricName);
	// ici la mesure de la métrique est simulée par une génération d'un float entre 0 et 170. Ceci doit être remplac?par l'appel de la directive de mesure
	float value = new Float(Math.random()*170);
	System.out.println("[SWRLBuiltInLibraryImpl] generated metric "+metricName+" value = "+value);
	metricValues.add(value);
	
	//int resultIndex = SWRLBuiltInUtil.getFirstUnboundArgument(arguments);
	// affecter au deuxième argument la valeur du résultat
	if (arguments.size()>1)
		arguments.set(1, new LiteralInfo(value)); // Bind the result to the given variable
	return true;
	} //getMetric
	
	
	public boolean agregateMetric(List<Argument> arguments) throws BuiltInException
      	{
        
    	//System.out.println("______________________________ agregateMetric start__________");
        //SWRLBuiltInUtil.checkNumberOfArgumentsEqualTo(1, arguments.size());
        String metricName = SWRLBuiltInUtil.getArgumentAsAnIndividualName(0, arguments);
		String agregationFunction = "";
		try{
	          agregationFunction = SWRLBuiltInUtil.getArgumentAsALiteral(1, arguments).toString();
			}
			catch(BuiltInException e)
			{
				
				agregationFunction = SWRLBuiltInUtil.getArgumentAsAnIndividualName(1, arguments);

			}
			
	
	// check if the referenced metric is already measured
	if (!metrics.containsKey(metricName))
	{
		System.out.println("[SWRLBuiltInLibraryImpl] undefined metric "+metricName); 
		
		return false;
	}
	
	try{
		Function function = (Function) Class.forName(agregationFunction).newInstance();
			// affecter au troisième argument la valeur du résultat
		if (arguments.size()>2)
			arguments.set(2, function.call(metrics)); // Bind the result to the third parameter
		  // 	System.out.println("______________________________ agregateMetric end__________");
		return true;
	}catch(Exception e)
	{
		e.printStackTrace();
		return false;
	}
	
	
	} //agregateMetric
      
    
	 public boolean disseminateViolation(List<Argument> arguments) throws BuiltInException
      {
        
    
        SWRLBuiltInUtil.checkNumberOfArgumentsAtLeast(1, arguments.size());
		MESSAGE = "--- ViolationDetected in SLAOnt caused by values: ";
		for (int argumentNumber = 0; argumentNumber < arguments.size()-1; argumentNumber++) {// Exception thrown if argument is not a literal.
			try{
	          MESSAGE += SWRLBuiltInUtil.getArgumentAsALiteral(argumentNumber, arguments).toString()+" , ";
			}
			catch(BuiltInException e)
			{
				
				MESSAGE += SWRLBuiltInUtil.getArgumentAsAnIndividualName(argumentNumber, arguments)+" = ";

			}
        	 }
		try{
	          MESSAGE += SWRLBuiltInUtil.getArgumentAsALiteral(arguments.size()-1, arguments).toString();
			}
			catch(BuiltInException e)
			{
				MESSAGE += SWRLBuiltInUtil.getArgumentAsAnIndividualName(arguments.size()-1, arguments);

			}
        System.out.println(MESSAGE+"---");
		Runnable r3 = new Runnable() {
                 public void run() {
		JOptionPane.showMessageDialog(null,MESSAGE+"---");
			}
              };
			Thread rr3 = new Thread(r3, "PredicateEvaluationThread");
			rr3.start();		// cette méthode peut être étendue ?d'autres actions plus élaborées qu'un simple affichage sur la console
        return true;
      } // disseminateViolation
	  
	  public boolean setSLAParameterValue(List<Argument> arguments) throws BuiltInException
      {
        
		  SWRLBuiltInUtil.checkNumberOfArgumentsEqualTo(2, arguments.size());
		  String slaParameterName = SWRLBuiltInUtil.getArgumentAsAnIndividualName(0, arguments);
		  try{
			if (owlModel == null)
				loadOwlFileChooser();
			OWLIndividual slaParameter = owlModel.getOWLIndividual(slaParameterName);
			OWLDatatypeProperty hasEvaluationProperty = owlModel.getOWLDatatypeProperty(SLAONT_PREFIX+"hasEvaluation"); 
			if (SWRLBuiltInUtil.isArgumentAFloat(1, arguments))
				slaParameter.setPropertyValue(hasEvaluationProperty, SWRLBuiltInUtil.getArgumentAsAFloat(1, arguments));
			else if (SWRLBuiltInUtil.isArgumentAnInteger(1, arguments))
				slaParameter.setPropertyValue(hasEvaluationProperty, SWRLBuiltInUtil.getArgumentAsAnInteger(1, arguments));
			else if (SWRLBuiltInUtil.isArgumentAString(1, arguments))
				slaParameter.setPropertyValue(hasEvaluationProperty, SWRLBuiltInUtil.getArgumentAsAString(1, arguments));
			else if (SWRLBuiltInUtil.isArgumentABoolean(1, arguments))
				slaParameter.setPropertyValue(hasEvaluationProperty, SWRLBuiltInUtil.getArgumentAsABoolean(1, arguments));
			else if (SWRLBuiltInUtil.isArgumentADouble(1, arguments))
				slaParameter.setPropertyValue(hasEvaluationProperty, SWRLBuiltInUtil.getArgumentAsADouble(1, arguments));
			else if (SWRLBuiltInUtil.isArgumentALong(1, arguments))
				slaParameter.setPropertyValue(hasEvaluationProperty, SWRLBuiltInUtil.getArgumentAsALong(1, arguments));
			else if (SWRLBuiltInUtil.isArgumentAShort(1, arguments))
				slaParameter.setPropertyValue(hasEvaluationProperty, SWRLBuiltInUtil.getArgumentAsAShort(1, arguments));
			
			save(owlModel, OWL_File);
			   return true;
			}
			catch(Exception e)
			{
				e.printStackTrace();
				return false;
			}
		
       
      } // disseminateViolation


	private void loadOwlFile()
	{
		// ouvrir le fichier owl (slaOnt.owl)
		try{
			System.out.println("OWL FILE NOT SPECIFIED");
			OWL_File = JOptionPane.showInputDialog("Please enter the complet Path to the SLAont OWL File","./javaTestOnto/SLAont.owl");
			File file = new File(OWL_File);
			// récupérer le modèle owl de l'ontologie spécifiée dans le fichier owl
			owlModel = ProtegeOWL.createJenaOWLModelFromURI(file.toURI().toString());
			}
			catch(Exception e)
			{
			e.printStackTrace();
			JOptionPane.showMessageDialog(null,"Failed to load OWL File");
			}
	}

	private void loadOwlFileChooser()
	{
		// ouvrir le fichier owl (slaOnt.owl)
		try{
			JOptionPane.showMessageDialog(null,"OWL FILE NOT SPECIFIED! \n please choose the SLAOnt OWL file");
			JFileChooser fc = new JFileChooser("./javaTestOnto/SLAont.owl");
			int returnVal=-1;
			do{
			returnVal = fc.showOpenDialog(null);
			}while (returnVal!=JFileChooser.APPROVE_OPTION);
         	        File file = fc.getSelectedFile();
			OWL_File = file.getAbsolutePath();
			// récupérer le modèle owl de l'ontologie spécifiée dans le fichier owl
			owlModel = ProtegeOWL.createJenaOWLModelFromURI(file.toURI().toString());
			}
			catch(Exception e)
			{
			e.printStackTrace();
			JOptionPane.showMessageDialog(null,"Failed to load OWL File");
			}
	}

	private void save(OWLModel owl, String fileName)
	{
		Collection errors = new ArrayList();
		((JenaOWLModel) owl).save(new File(fileName).toURI(), FileUtils.langXMLAbbrev, errors);
		System.out.println(fileName+" File saved with " + errors.size() + " errors.");
	}

  
} // SWRLBuiltInLibraryImpl