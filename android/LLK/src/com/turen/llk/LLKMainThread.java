package com.turen.llk;

import java.util.ArrayList;

import com.turen.llk.cache.GraphicsUtil;
import com.turen.llk.domain.LevelInfo;

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
	HeaderPictureGrid pre = null;
	HeaderPictureGrid current = null;

	public LLKMainThread(SurfaceHolder surfaceHolder, Context context,
			Handler handler, LLKMainGame game, Bitmap mBackgroundImage) {
		mSurfaceHolder = surfaceHolder;
		mHandler = handler;
		mContext = context;
		mGame = game;
		this.mBackgroundImage = mBackgroundImage;
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

	public void onTouchEvent(MotionEvent event) {
		int x = ((int) event.getX()) / this.mGame.getGridWidth();
		int y = ((int) event.getY()) / this.mGame.getGridHeight();
		Log.v("llktouch",""+x+" "+y);
		HeaderPictureGrid[][] grid = this.mGame.getGrid();
		// LevelInfo levelInfo=this.mGame.getLevelInfo();
		// int gridSize=levelInfo.x*levelInfo.y;
		// int j=0;
		// grid[x][y].setRemoved(true);
		current = grid[x][y];
		if(current==null){
			pre=null;
		}
		if (pre == null) {
			pre = current;// grid[x][y];
			current = null;
		} else {
			//Log.v("myllk", "pre name:"+pre.getName());
			//Log.v("myllk", "current name:"+current.getName());
			if (pre.getName().equals(current.getName())) {
				//Log.v("myllk"," match "+pre.getName());
				if (this.mGame.findPath(pre, current)) {
					pre.setRemoved(true);
					current.setRemoved(true);
				}
			}
			pre = null;
			current = null;
		}
	}

	private void drawBackground(Canvas canvas) {
		canvas.drawBitmap(mBackgroundImage, 0, 0, null);
	}

	/**
	 * 画面处理
	 * 
	 * @param canvas
	 */
	private void doDraw(Canvas canvas) {
		drawBackground(canvas);

		HeaderPictureGrid[][] grid = this.mGame.getGrid();
		LevelInfo levelInfo = this.mGame.getLevelInfo();
		for (int i = 1; i < levelInfo.x+1; i++) {
			for (int j = 1; j < levelInfo.y+1; j++) {
				if (!grid[i][j].isRemoved()) {
					canvas.drawBitmap(grid[i][j].getHeaderImage(), i
							* this.mGame.getGridWidth(), j
							* this.mGame.getGridHeight(), null);
				}
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