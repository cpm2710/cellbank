package edu.sjtu.owl.swrl.bridge.builtins.slaActions;
import edu.stanford.smi.protegex.owl.swrl.bridge.LiteralInfo;
import edu.stanford.smi.protegex.owl.swrl.bridge.builtins.slaActions.Function;

import java.util.Vector;
import java.util.Hashtable;

public class AverageFunction implements Function
{
	
	public LiteralInfo call(Hashtable metrics)
	{
		Vector values = (Vector) metrics.get("responseTime");
		float sum=0;
		for(int i=0; i<values.size();i++)
			sum += ((Float) values.get(i)).floatValue();
		float res = sum/values.size();
		System.out.println("[SWRLBuiltInLibraryImpl] slaParameterValue = "+res); 
		values.removeAllElements();
		return new LiteralInfo(res);
	}
}