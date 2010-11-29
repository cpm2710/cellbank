package com.turen.llk;

import java.util.ArrayList;
import java.util.HashMap;

import com.renren.api.connect.android.view.ConnectButton;
import com.turen.llk.util.ImageRetriever;


import android.app.Activity;
import android.app.AlertDialog.Builder;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.util.Log;
import android.view.Window;
import android.view.WindowManager;
import android.widget.GridView;
import android.widget.ListAdapter;
import android.widget.ListView;

public class LLKMainActivity extends Activity{
	LLKMainActivity main;
	ArrayList<NameBitmapPair> headerImageList=null;
	@Override
	public void onCreate(Bundle savedInstanceState) {
		main=this;
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE); // 隐藏标题
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,

		WindowManager.LayoutParams.FLAG_FULLSCREEN); // 设置全屏
		setContentView(R.layout.llkmain);
		retrieveHeaderImages();
		createGame();
	}
	public void retrieveHeaderImages(){
		if(headerImageList==null){
		headerImageList=new ArrayList<NameBitmapPair>();
		
		Bundle bundle=this.getIntent().getExtras();
		ArrayList<NameHeaderUrlPair> nameHeaderUrlList=(ArrayList<NameHeaderUrlPair>)bundle.get("nameHeaderUrlList");
		
		for(NameHeaderUrlPair pair : nameHeaderUrlList){
			NameBitmapPair p=new NameBitmapPair();
			p.setName(pair.getName());
			Bitmap map=ImageRetriever.getImage(pair.getHeaderUrl());
			p.setHeaderImage(map);
			headerImageList.add(p);
		}}
	}
	public void createGame(){		
		GridView gv= (GridView) findViewById(R.id.llkGrid);
		ListAdapter adapter = new ImageAdapter(this,headerImageList);
		gv.setAdapter(adapter);
		//gv.refreshDrawableState();
		/*
		
        final GridView lv = new GridView(this);
        lv.setAdapter(adapter);
        this.runOnUiThread(new Runnable() {

            @Override
            public void run() {
                Builder alertBuilder = new Builder(main);
                alertBuilder.setTitle("My Friends");
                alertBuilder.setView(lv);
                alertBuilder.setNeutralButton("确定", null);
                alertBuilder.create().show();
            }
        });*/
	}
}
