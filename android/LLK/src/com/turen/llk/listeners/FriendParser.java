package com.turen.llk.listeners;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.renren.api.connect.android.Renren;
import com.turen.llk.domain.NameBitmapPair;
import com.turen.llk.domain.NameHeaderUrlPair;
import com.turen.llk.util.ImageRetriever;

import android.graphics.Bitmap;
import android.os.Bundle;
import android.util.Log;

public class FriendParser {
	private Renren renren;
	private String dataFormat="json";
	public FriendParser(Renren renren){
		this.renren=renren;
	}
	public ArrayList<NameHeaderUrlPair> getFriendNameHeaderUrl(int friendNumber){
		ArrayList<NameHeaderUrlPair> pairs=new ArrayList<NameHeaderUrlPair>();
		Bundle params = new Bundle();
		params.putString("method", "friends.getFriends");
		params.putString("page", "1");
		params.putString("count", "9999");
		String response=this.renren.request(params, dataFormat);
		List<Map<String, String>> datas = null;
      
        datas = this.parseFriendJson(response);
        Collections.shuffle(datas);
        
        for(int i=0;i<friendNumber;i++){
        	String headerUrl=datas.get(i).get("headurl");
			String name=datas.get(i).get("name");
			pairs.add(new NameHeaderUrlPair(name,headerUrl));
        }
       /* for(Map<String,String> friend : datas){
			String headerUrl=friend.get("headurl");
			String name=friend.get("name");
			pairs.add(new NameHeaderUrlPair(name,headerUrl));
			
		}*/
        
        return pairs;
	}
	public  ArrayList<NameBitmapPair> getFriendImageList(){
		HashMap<String,Bitmap> headerImages=new HashMap<String,Bitmap>();
		ArrayList<NameBitmapPair> imageList=new ArrayList<NameBitmapPair>();
		 Bundle params = new Bundle();
			params.putString("method", "friends.getFriends");
			params.putString("page", "1");
			params.putString("count", "10");
			//FriendRequestListener listener = new FriendRequestListener(this,
			//		dataFormat);
			String response=this.renren.request(params, dataFormat);//.request(params, listener, dataFormat);
			List<Map<String, String>> datas = null;
	      
	        datas = this.parseFriendJson(response);
	        int id=0;
	        for(Map<String,String> friend : datas){
				String headerUrl=friend.get("headurl");
				Bitmap headerImage=ImageRetriever.getImage(headerUrl);
				if(headerImage!=null){
				String name=friend.get("name");
				Log.v("llk",name);
				if(headerImages.get(name)==null){
				headerImages.put(name, headerImage);
				NameBitmapPair nbp=new NameBitmapPair();
				nbp.setName(name);
				nbp.setHeaderImage(headerImage);
				imageList.add(nbp);
				}else{
					headerImages.put(name+id, headerImage);
					NameBitmapPair nbp=new NameBitmapPair();
					nbp.setName(name+id);
					nbp.setHeaderImage(headerImage);
					imageList.add(nbp);
				}}
			}
	        
			return imageList;
	}
	public HashMap<String,Bitmap> getFriendImages(){
		HashMap<String,Bitmap> headerImages=new HashMap<String,Bitmap>();
		Bundle params = new Bundle();
		params.putString("method", "friends.getFriends");
		params.putString("page", "1");
		params.putString("count", "5");
		//FriendRequestListener listener = new FriendRequestListener(this,
		//		dataFormat);
		String response=this.renren.request(params, dataFormat);//.request(params, listener, dataFormat);
		List<Map<String, String>> datas = null;
      
        datas = this.parseFriendJson(response);
        int id=0;
        for(Map<String,String> friend : datas){
			String headerUrl=friend.get("headurl");
			Bitmap headerImage=ImageRetriever.getImage(headerUrl);
			if(headerImage!=null){
			String name=friend.get("name");
			Log.v("llk",name);
			if(headerImages.get(name)==null){
			headerImages.put(name, headerImage);
			}else{
				headerImages.put(name+id, headerImage);
			}}
		}
		return headerImages;
	}
	private List<Map<String, String>> parseFriendJson(String json) {
        List<Map<String, String>> datas = new ArrayList<Map<String, String>>();
        try {
            JSONArray friends = new JSONArray(json);
            for (int i = 0; i < friends.length(); i++) {
                Map<String, String> d = new HashMap<String, String>();
                Object obj = friends.get(i);
                JSONObject jobj = new JSONObject(obj.toString());
                d.put("headurl", jobj.getString("tinyurl"));
                d.put("name", jobj.getString("name"));
                datas.add(d);
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return datas;
    }
	public void getFriendResources(){
		
        

	}
}
