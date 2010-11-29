package com.turen.llk.view;


import java.util.ArrayList;

import com.turen.llk.cache.HeaderImageCacher;
import com.turen.llk.domain.NameBitmapPair;
import com.turen.llk.domain.NameHeaderUrlPair;

import android.content.Context;
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
	ArrayList<NameBitmapPair> headerImageList=null;
	HeaderImageCacher cacher;
	public LLKView(Context context,HeaderImageCacher cacher,ArrayList<NameHeaderUrlPair> nameHeaderUrlList) {
		super(context);
		this.cacher=cacher;
		headerImageList=cacher.getNameBitmapList(nameHeaderUrlList);
		SurfaceHolder holder = this.getHolder();// 获取holder
		holder.addCallback(this);
		/*thread = new LLKMainThread(holder, context,new Handler() {
			@Override
			public void handleMessage(Message m) {
				//mStatusText.setVisibility(m.getData().getInt("viz"));
				//mStatusText.setText(m.getData().getString("text"));
			}
		});*/
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
		thread.start();}
	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		// TODO Auto-generated method stub
		
	}
	public boolean onTouchEvent(MotionEvent event) {
		int eventaction = event.getAction();
		int dx = (int) event.getX();
		int dy = (int) event.getY();
		Log.v("myllk", ""+dx+" "+dy);
		switch (eventaction) {
		case MotionEvent.ACTION_DOWN:
			//thread.updatePhysics(dx, dy);
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
	class LLKMainThread extends Thread {
		private SurfaceHolder mSurfaceHolder;
		private Handler mHandler;
		private Context mContext;
		/**
		 * 游戏状态：开始，进行中，暂停，结束
		 */
		public static final int STATE_START = 1;
		public static final int STATE_RUNNING = 2;
		public static final int STATE_PAUSE = 3;
		public static final int STATE_OVER = 4;
		/** 线程启动 开关 */
		private boolean mRun = false;
		private int mMode;
		
		public LLKMainThread(SurfaceHolder surfaceHolder, Context context,
				Handler handler) {
			mSurfaceHolder = surfaceHolder;
			mHandler = handler;
			mContext = context;
		}
		public boolean onKeyDown(int keyCode, KeyEvent msg) {
			synchronized (mSurfaceHolder) {
				if (keyCode == KeyEvent.KEYCODE_BACK) {
					setRunning(false);
					stop();
					return true;
				}else if(mMode == STATE_OVER && keyCode == KeyEvent.KEYCODE_CALL){
					mMode = STATE_START;
					return true;
				}
			}
			return false;
		}
		/**
		 * 线程的开启以及关闭 true:开启 false:关闭
		 */
		public void setRunning(boolean b) {
			mRun = b;
		}
		/**
		 * 五子棋画面处理
		 * 
		 * @param canvas
		 */
		private void doDraw(Canvas canvas) {
			
			/** 游戏状态变更 */
			if(mMode == STATE_START){
				//getBackground();
				mMode = STATE_RUNNING;
			}

			/** 画棋盘 */
			//drawBackground(canvas);
		}
		/**
		 * 执行线程
		 */
		public void run() {
			while (mRun) {
				Canvas c = null;
				try {
					c = mSurfaceHolder.lockCanvas(null);
					synchronized (mSurfaceHolder) {
						doDraw(c);
					}
				} finally {
					if (c != null) {
						mSurfaceHolder.unlockCanvasAndPost(c);
					}
				}
			}
		}
	}
}
