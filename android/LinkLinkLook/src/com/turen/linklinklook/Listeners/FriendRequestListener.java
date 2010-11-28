package com.turen.linklinklook.Listeners;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.ProgressDialog;
import android.app.AlertDialog.Builder;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.RequestListenerHelper.DefaultRequestListener;
import com.turen.linklinklook.Main;
import com.turen.linklinklook.imageutil.ImageRetriever;
import com.turen.linklinklook.xml.FriendHandler;
import com.renren.api.connect.android.exception.RenrenError;

/**
 * @author 李勇(yong.li@opi-corp.com) 2010-9-3
 */
public class FriendRequestListener extends DefaultRequestListener {

    private Main main;

    private ProgressDialog progress;

    private String dataFormate;

    public FriendRequestListener(Main main, String dataFormate) {
        this.main = main;
        this.dataFormate = dataFormate;
        progress = ProgressDialog.show(main, "Get friends", "Loading...");
        progress.show();
    }

    @Override
    public void onFault(final Throwable fault) {
        showError("Fault", fault.toString());
    }

    @Override
    public void onRenrenError(final RenrenError e) {
        showError("RenrenError", e.toString());
    }

    private void showError(final String title, final String text) {
        main.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (progress != null) progress.dismiss();
                Util.showAlert(main, title, text);
            }
        });
    }

    @SuppressWarnings("unchecked")
    @Override
    public void onComplete(String response) {
    	List<Map<String, String>> datas = null;
        
        datas = this.parseFriendJson(response);
        int length=datas.size();
        for(final Map<String,String> friend : datas){
        	/*main.runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    if (progress != null) progress.dismiss();
                    Util.showAlert(main, "", friend.values().toString());
                }
            });*/
        	String headerUrl=friend.get("headurl");
        	Bitmap headerImage=ImageRetriever.getImage(headerUrl);
        	main.getHeaderImages().put(headerUrl, headerImage);
        }
        
        
        /*List<Map<String, String>> datas = null;
       
            datas = this.parseFriendJson(response);
        

        ListAdapter adapter = new ImageAdapter(this.main, datas);
        ListView lv = new ListView(this.main);
        lv.setAdapter(adapter);*/

        this.progress.dismiss();
        /*this.showResult(lv);*/
    }

    private void showResult(final ListView lv) {
        this.main.runOnUiThread(new Runnable() {

            @Override
            public void run() {
                /*Builder alertBuilder = new Builder(main);
                alertBuilder.setTitle("My Friends");
                alertBuilder.setView(lv);
                alertBuilder.setNeutralButton("确定", null);
                alertBuilder.create().show();*/
            }
        });
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
}
