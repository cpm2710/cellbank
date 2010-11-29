package com.turen.llk;

import java.util.ArrayList;

import android.content.Context;
import android.graphics.Canvas;
import android.os.Handler;
import android.view.KeyEvent;
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
		
		public LLKMainThread(SurfaceHolder surfaceHolder, Context context,
				Handler handler,LLKMainGame game) {
			mSurfaceHolder = surfaceHolder;
			mHandler = handler;
			mContext = context;
			mGame=game;
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
		/**
		 * 画面处理
		 * 
		 * @param canvas
		 */
		private void doDraw(Canvas canvas) {			
			ArrayList<HeaderPictureGrid> pictureGrids=this.mGame.getHeaderPictureGrids();
			for(int i=0;i<pictureGrids.size();i++){
				HeaderPictureGrid grid=pictureGrids.get(i);
				canvas.drawBitmap(grid.getHeaderImage(), grid.getX()*40, grid.getY()*40, null);
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