package edu.stanford.smi.protegex.owl.swrl.bridge.builtins.slaActions;
import edu.stanford.smi.protegex.owl.swrl.bridge.LiteralInfo;
import java.util.Hashtable;

public interface Function
{
	public LiteralInfo call(Hashtable metrics);
}