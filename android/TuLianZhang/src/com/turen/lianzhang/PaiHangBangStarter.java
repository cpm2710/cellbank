package com.turen.lianzhang;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
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
		        }catch(Exception e){
		        	e.printStackTrace();
					Dialog dialog = new AlertDialog.Builder(Main.m).setTitle("出现了网络故障!")
					 .setIcon(R.drawable.lianzhangicon)
	               .setMessage("这有可能是google appspot 无法连接造成的,请先连接网络并确保您的网络通畅!")  
	               // .setItems(str, Test_Dialog.this)// 设置对话框要显示的一个list  
	               // .setSingleChoiceItems(str, 0, Test_Dialog.this)//  
	               // 设置对话框显示一个单选的list  
	               .setPositiveButton("确定", new android.content.DialogInterface.OnClickListener (){

						@Override
						public void onClick(DialogInterface arg0, int arg1) {
							// TODO Auto-generated method stub
							arg0.dismiss();
						}
	              	 
	               })
	               .create();
					 dialog.show(); 
		        }
				
				listener.paiHangBangOnComplete(level,chengJis);
			}
		}.start();
	}
}
