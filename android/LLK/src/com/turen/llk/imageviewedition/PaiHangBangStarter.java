package com.turen.llk.imageviewedition;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.util.Log;

import com.renren.api.connect.android.Util;
import com.turen.llk.Main;
import com.turen.llk.domain.ChengJi;
import com.turen.llk.listeners.StartGameListener;

public class PaiHangBangStarter {
	public void startPaiHangBang(final PaiHangBangListener listener) {
		new Thread() {
			@Override
			public void run() {
				String result=Util.openUrl("http://turenllm.appspot.com/friendllkserver/getPaiHangBangServlet", "GET", null);
				Log.v("result",result);
				ArrayList<ChengJi> chengJis=new ArrayList<ChengJi>();
				
				try {
		            JSONArray chengJiJSON = new JSONArray(result);
		            for (int i = 0; i < chengJiJSON.length(); i++) {
		            	ChengJi chengJi=new ChengJi();
		            	Object obj = chengJiJSON.get(i);
		                JSONObject jobj = new JSONObject(obj.toString());
		            	chengJi.setEmail(jobj.getString("email"));
		            	chengJi.setUsername(jobj.getString("userName"));
		            	chengJi.setSeconds(Integer.parseInt(jobj.getString("seconds")));
		            	chengJis.add(chengJi);
		            }
		        } catch (JSONException e) {
		            e.printStackTrace();
		        }
				
				listener.paiHangBangOnComplete(chengJis);
			}
		}.start();
	}
}
