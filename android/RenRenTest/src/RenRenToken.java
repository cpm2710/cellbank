import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;


public class RenRenToken {
	public static void main(String args[]){
		List<NameValuePair> nvps = new  ArrayList<NameValuePair>();  
        nvps.add(new  BasicNameValuePair( "api_key" , "85ede30cd7a0401f859de10dded7c62b"));  
        nvps.add(new  BasicNameValuePair( "format" ,  "JSON" ));  
        nvps.add(new  BasicNameValuePair( "method" ,  "xiaonei.auth.createToken" ));  
        nvps.add(new  BasicNameValuePair( "v" ,  "1.0" ));
        nvps.add(new BasicNameValuePair("sig", getSig(data));
        

		PostMethod postMethod = new PostMethod(REST_URL);
		postMethod.addParameters(data);
		client.executeMethod(postMethod);
		String body = postMethod.getResponseBodyAsString();
	}
}
