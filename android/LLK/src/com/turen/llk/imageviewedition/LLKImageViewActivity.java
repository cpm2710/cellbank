package com.turen.llk.imageviewedition;

import java.util.ArrayList;

import android.app.Activity;
import android.os.Bundle;
import android.view.Display;
import android.view.Window;
import android.view.WindowManager;

import com.turen.llk.cache.HeaderImageCacher;
import com.turen.llk.domain.NameHeaderUrlPair;

public class LLKImageViewActivity extends Activity{
	LLKImageViewActivity main;
	@SuppressWarnings("unchecked")
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
	          
	    LLKImageView imageView=new LLKImageView(this,new HeaderImageCacher(main),nameHeaderUrlList,screenWidth,screenHeight);
	    this.setContentView(imageView);
	    
	}
}
