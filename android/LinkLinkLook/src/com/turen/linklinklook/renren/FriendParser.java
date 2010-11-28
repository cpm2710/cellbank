package com.turen.linklinklook.renren;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.turen.linklinklook.imageutil.ImageRetriever;

import android.graphics.Bitmap;
import android.os.Bundle;
import android.util.Log;

public class FriendParser {
	public static List<Map<String, String>> parseFriendJson(String json){
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
	public static HashMap<String,Bitmap> parseHeaderImages(RenRen renren){
		HashMap<String,Bitmap> headerImages=new HashMap<String,Bitmap>();
		Bundle params = new Bundle();
        params.putString("method", "friends.getFriends");
        params.putString("page", "1");
        params.putString("count", "10");
        String dataFormat = "json";
        String resp = renren.request(params, dataFormat);
        Log.v("a",resp);
		RenrenError rrError = Util.parseRenrenError(resp, dataFormat);
		List<Map<String, String>> datas = null;
		datas = FriendParser.parseFriendJson(resp);
		for(Map<String,String> friend : datas){
			String headerUrl=friend.get("headurl");
			Bitmap headerImage=ImageRetriever.getImage(headerUrl);
			headerImages.put(headerUrl, headerImage);
		}
		return headerImages;
	}
}
