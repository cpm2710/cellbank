package com.turen.lianzhang;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.os.Bundle;
import android.util.Log;

import com.renren.api.connect.android.Util;
import com.turen.lianzhang.Main;
import com.turen.lianzhang.domain.ChengJi;
import com.turen.lianzhang.listeners.StartGameListener;

public class PaiHangBangStarter {
	public void startPaiHangBang(final PaiHangBangListener listener,final int level) {
		new Thread() {
			@Override
			public void run() {
				Bundle bundle=new Bundle();
				bundle.putString("level", String.valueOf(level));
				String result=Util.openUrl("http://turenllm.appspot.com/friendllkserver/getPaiHangBangServlet", "GET", bundle);
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
		            	chengJi.setHeaderUrl(jobj.getString("headUrl"));
		            	chengJi.setSeconds(Integer.parseInt(jobj.getString("miniSeconds")));
		            	chengJis.add(chengJi);
		            }
		        } catch (JSONException e) {
		            e.printStackTrace();
		        }
				
				listener.paiHangBangOnComplete(level,chengJis);
			}
		}.start();
	}
}
