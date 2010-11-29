package com.turen.llk;

import java.util.ArrayList;


import android.content.Context;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.os.Handler;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.SurfaceHolder;

public class LLKMainThread extends Thread {
		private LLKMainGame mGame;
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
		private Bitmap mBackgroundImage;
		
		public LLKMainThread(SurfaceHolder surfaceHolder, Context context,
				Handler handler,LLKMainGame game) {
			mSurfaceHolder = surfaceHolder;
			mHandler = handler;
			mContext = context;
			mGame=game;
			Resources res = mContext.getResources();
			mBackgroundImage = BitmapFactory.decodeResource(res,
					R.drawable.blackground);
		}
		public boolean onKeyDown(int keyCode, KeyEvent msg) {
			synchronized (mSurfaceHolder) {
				
			}
			return false;
		}
		/**
		 * 线程的开启以及关闭 true:开启 false:关闭
		 */
		public void setRunning(boolean b) {
			mRun = b;
		}
		public void onTouchEvent(MotionEvent event){
			ArrayList<HeaderPictureGrid> pictureGrids=this.mGame.getHeaderPictureGrids();
			int x=((int)event.getX())/pictureGrids.get(0).getHeaderImage().getWidth();
			int y=((int)event.getY())/pictureGrids.get(0).getHeaderImage().getHeight();
			//pictureGrids.get(0).getHeaderImage().getWidth();
			for(HeaderPictureGrid g:pictureGrids){
				if(g.getX()==x&&g.getY()==y){
					Log.v("llkremove","removed "+g.getName());
					g.setRemoved(true);
					break;
				}
			}
		}
		private void drawBackground(Canvas canvas){
			canvas.drawBitmap(mBackgroundImage, 0, 0, null);
		}
		/**
		 * 画面处理
		 * 
		 * @param canvas
		 */
		private void doDraw(Canvas canvas) {
			drawBackground(canvas);
			ArrayList<HeaderPictureGrid> pictureGrids=this.mGame.getHeaderPictureGrids();
			for(int i=0;i<pictureGrids.size();i++){
				HeaderPictureGrid grid=pictureGrids.get(i);
				if(!grid.isRemoved()){
				int width=grid.getHeaderImage().getWidth();
				int height=grid.getHeaderImage().getHeight();
				canvas.drawBitmap(grid.getHeaderImage(), grid.getX()*width, grid.getY()*height, null);
				}else{
					//canvas.draw
				}
			}
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