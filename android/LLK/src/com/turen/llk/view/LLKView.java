package com.turen.llk.view;


import java.util.ArrayList;

import com.turen.llk.LLKMainGame;
import com.turen.llk.LLKMainThread;
import com.turen.llk.R;
import com.turen.llk.cache.GraphicsUtil;
import com.turen.llk.cache.HeaderImageCacher;
import com.turen.llk.domain.LevelInfo;
import com.turen.llk.domain.NameBitmapPair;
import com.turen.llk.domain.NameHeaderUrlPair;

import android.content.Context;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

public class LLKView extends SurfaceView implements SurfaceHolder.Callback{

	private int screenWidth;
	private int screenHeight;
	private LLKMainThread thread;
	//private ArrayList<NameBitmapPair> headerImageList=null;
	private LLKMainGame llkGame=null;
	private HeaderImageCacher cacher=null;
	public LLKView(Context context,
			HeaderImageCacher cacher,
			ArrayList<NameHeaderUrlPair> nameHeaderUrlList,
			int screenWidth,
			int screenHeight) {
		super(context);
		this.cacher=cacher;
		ArrayList<NameBitmapPair> headerImageList=this.cacher.getNameBitmapList(nameHeaderUrlList);
		LevelInfo levelInfo=new LevelInfo();
		levelInfo.x=5;
		levelInfo.y=5;
		this.llkGame=new LLKMainGame(headerImageList,screenWidth,screenHeight,levelInfo);
		
		SurfaceHolder holder = this.getHolder();// 获取holder
		holder.addCallback(this);
		Resources res = context.getResources();
		Bitmap mBackgroundImage=BitmapFactory.decodeResource(res,
				R.drawable.blackground);
		mBackgroundImage=GraphicsUtil.getResize(mBackgroundImage, screenWidth, screenHeight);
		thread = new LLKMainThread(holder, context,new Handler() {
			@Override
			public void handleMessage(Message m) {
				//mStatusText.setVisibility(m.getData().getInt("viz"));
				//mStatusText.setText(m.getData().getString("text"));
			}
		},llkGame,mBackgroundImage);
		setFocusable(true);
	}
	
	// 获取屏幕宽度
	public void setScreenWidth(int screenWidthIn) {
		screenWidth = screenWidthIn;
	}

	// 返回屏幕宽度
	public int getScreenWidth() {
		return screenWidth;
	}

	// 获取屏幕高度
	public void setScreenHeight(int screenHeightIn) {
		screenHeight = screenHeightIn;
	}

	// 返回屏幕高度
	public int getScreenHeight() {
		return screenHeight;
	}
	@Override
	public void surfaceChanged(SurfaceHolder arg0, int arg1, int arg2, int arg3) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void surfaceCreated(SurfaceHolder holder) {
		if(thread!=null){
			thread.setRunning(true);
		thread.start();
		}
	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		// TODO Auto-generated method stub
		
	}
	public boolean onTouchEvent(MotionEvent event) {
		int eventaction = event.getAction();
		int dx = (int) event.getX();
		int dy = (int) event.getY();
		Log.v("myllk2", ""+dx+" "+dy);
		switch (eventaction) {
		case MotionEvent.ACTION_DOWN:
			thread.onTouchEvent(event);
			break;
		case MotionEvent.ACTION_MOVE:
			break;
		case MotionEvent.ACTION_UP:
			break;
		default:
			break;
		}
		return true;
	}

	public boolean onKeyDown(int keyCode, KeyEvent msg) {
		return thread.onKeyDown(keyCode, msg);
	}
	
}
