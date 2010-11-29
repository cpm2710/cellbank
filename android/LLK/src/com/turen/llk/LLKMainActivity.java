package com.turen.llk;

import java.util.ArrayList;
import java.util.HashMap;

import com.renren.api.connect.android.view.ConnectButton;
import com.turen.llk.cache.GraphicsUtil;
import com.turen.llk.cache.HeaderImageCacher;
import com.turen.llk.domain.NameBitmapPair;
import com.turen.llk.domain.NameHeaderUrlPair;
import com.turen.llk.util.ImageRetriever;


import android.app.Activity;
import android.app.AlertDialog.Builder;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.util.Log;
import android.view.Display;
import android.view.Window;
import android.view.WindowManager;
import android.widget.GridView;
import android.widget.ListAdapter;
import android.widget.ListView;

public class LLKMainActivity extends Activity{
	LLKMainActivity main;
	ArrayList<NameBitmapPair> headerImageList=null;
	HeaderImageCacher cacher;
	@Override
	public void onCreate(Bundle savedInstanceState) {
		main=this;
		this.cacher=new HeaderImageCacher(main);
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
		Bundle bundle=this.getIntent().getExtras();
		ArrayList<NameHeaderUrlPair> nameHeaderUrlList=(ArrayList<NameHeaderUrlPair>)bundle.get("nameHeaderUrlList");
		headerImageList=cacher.getNameBitmap(nameHeaderUrlList);
		}
	}
	public void createGame(){		
		GridView gv= (GridView) findViewById(R.id.llkGrid);
		
		WindowManager windowManager = getWindowManager();
		Display display = windowManager.getDefaultDisplay();
		int width = display.getWidth();
		int height = display.getHeight();
		int division=(int)Math.sqrt(headerImageList.size());
		gv.setNumColumns(division+1);
		width/=division;
		height/=division;
		
		ImageAdapter adapter = new ImageAdapter(this,headerImageList);
		adapter.setWidth(width);
		adapter.setHeight(height);
		gv.setAdapter(adapter);		
	}
}
