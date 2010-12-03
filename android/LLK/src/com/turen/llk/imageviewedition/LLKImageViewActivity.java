package com.turen.llk.imageviewedition;

import java.util.ArrayList;
import java.util.Date;

import android.app.Activity;
import android.os.Bundle;
import android.os.SystemClock;
import android.util.Log;
import android.view.Display;
import android.view.Window;
import android.view.WindowManager;
import android.view.ViewGroup.LayoutParams;
import android.widget.Chronometer;
import android.widget.LinearLayout;

import com.turen.llk.R;
import com.turen.llk.cache.HeaderImageCacher;
import com.turen.llk.domain.CurrentUser;
import com.turen.llk.domain.LevelInfo;
import com.turen.llk.domain.NameHeaderUrlPair;

public class LLKImageViewActivity extends Activity{
	LLKImageViewActivity main;
	LLKImageView imageView;
	CurrentUser currentUser;
	public CurrentUser getCurrentUser() {
		return currentUser;
	}
	public void setCurrentUser(CurrentUser currentUser) {
		this.currentUser = currentUser;
	}
	long gStartTime;
	public long getgStartTime() {
		return gStartTime;
	}
	public void setgStartTime(long gStartTime) {
		this.gStartTime = gStartTime;
	}
	public LLKImageView getImageView() {
		return imageView;
	}
	public void setImageView(LLKImageView imageView) {
		this.imageView = imageView;
	}
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
	    screenHeight=screenHeight-50;
	    Bundle bundle=this.getIntent().getExtras();
	    ArrayList<NameHeaderUrlPair> nameHeaderUrlList=(ArrayList<NameHeaderUrlPair>)bundle.get("nameHeaderUrlList");
	    Log.v("nameHeaderListSize:",""+nameHeaderUrlList.size());
	    LevelInfo levelInfo=(LevelInfo)bundle.get("levelInfo");
	    currentUser=(CurrentUser)bundle.get("currentUser");
	    
	    LinearLayout layout=new LinearLayout(this);
	    layout.setBackgroundResource(R.drawable.guilie);
	    layout.setPadding(0, 0, 0, 0);
	    layout.setOrientation(LinearLayout.VERTICAL);
	    imageView=new LLKImageView(this,new HeaderImageCacher(main),nameHeaderUrlList,screenWidth,screenHeight,levelInfo);
	    layout.addView(imageView);
	    
	    Chronometer meter=new Chronometer(this);
	    meter.setBase(SystemClock.elapsedRealtime());
	    meter.start();
	    meter.bringToFront();
	    layout.addView(meter);
	    this.setContentView(layout);
	    gStartTime=new Date().getTime();
	}
}
