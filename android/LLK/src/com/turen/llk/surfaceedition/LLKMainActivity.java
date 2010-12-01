package com.turen.llk.surfaceedition;

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
	private LLKView myView;
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		main=this;
		
		requestWindowFeature(Window.FEATURE_NO_TITLE); // 隐藏标题
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,

		WindowManager.LayoutParams.FLAG_FULLSCREEN); // 设置全屏
		int screenWidth;
	    int screenHeight;
	    	
	    //获得手机屏幕对象
	    WindowManager windowManager = getWindowManager();
	        
	    //获得手机屏幕显示对象
	    Display display = windowManager.getDefaultDisplay();
	        
	    //获得有效像素
	    screenWidth = display.getWidth();
	    screenHeight = display.getHeight();
	    Bundle bundle=this.getIntent().getExtras();
	    ArrayList<NameHeaderUrlPair> nameHeaderUrlList=(ArrayList<NameHeaderUrlPair>)bundle.get("nameHeaderUrlList");
	    myView = new LLKView(this,new HeaderImageCacher(main),nameHeaderUrlList,screenWidth,screenHeight);
        myView.setScreenWidth(screenWidth);
        myView.setScreenHeight(screenHeight);
        setContentView(myView);
	}
	public void initialGame(){
		
	}
}
