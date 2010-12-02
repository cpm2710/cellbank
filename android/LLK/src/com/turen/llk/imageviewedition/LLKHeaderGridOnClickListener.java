package com.turen.llk.imageviewedition;

import com.turen.llk.HeaderPictureGrid;
import com.turen.llk.LLKMainGame;
import com.turen.llk.R;
import com.turen.llk.listeners.ChengJiUploaderListener;

import android.content.Context;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;

public class LLKHeaderGridOnClickListener implements OnClickListener {
	Context mContext;
	HeaderGridImageView pre=null;
	//private HeaderPictureGrid pre=null;
	HeaderGridImageView current=null;
	//private HeaderPictureGrid current=null;
	
	LLKMainGame mGame=null;
	public LLKHeaderGridOnClickListener(Context c,LLKMainGame game){
		this.mContext=c;
		this.mGame=game;
	}
	@Override
	public void onClick(View v) {
		HeaderGridImageView view=(HeaderGridImageView)v;
		Log.v("shit",view.getGrid().getName());
		
		current = view;
		if(current==null){
			pre=null;
		}
		if (pre == null) {
			Log.v("pre null","pre is null");
			pre = current;
			current = null;
		} else {
			Log.v("pre is not null","pre is not null");
			if(pre==current){
				Log.v("pre equals current","pre equals current");
				pre = null;
				current = null;
				return;
			}
			if (pre.getGrid().getName().equals(current.getGrid().getName())) {
				if (this.mGame.findPath(pre.getGrid(), current.getGrid())) {
					pre.getGrid().setRemoved(true);
					current.getGrid().setRemoved(true);
					mGame.setGridRemoved(mGame.getGridRemoved()+2);
					Log.v("remove","removed "+pre.getGrid().getName()+" "+pre.getGrid().getName());
					pre.setVisibility(pre.INVISIBLE);
					current.setVisibility(current.INVISIBLE);
					Animation Anim_Alpha = AnimationUtils.loadAnimation(mContext, R.anim.alpha_action);  
					pre.startAnimation(Anim_Alpha); 
					Animation Anim_Alpha2 = AnimationUtils.loadAnimation(mContext, R.anim.alpha_action);  
					current.startAnimation(Anim_Alpha);
					Log.v("removed",""+mGame.getGridRemoved()+" "+(mGame.getGridSize()-10));
					if(mGame.getGridRemoved()==(mGame.getGridSize()-10)){
						ChengJiUploaderListener listener=new ChengJiUploaderListener();
						listener.showProgress(mContext, "上传您的成绩", "请耐心等待");
						ChengJiUploader uploader=new ChengJiUploader();
						uploader.uploadChengJi(listener);
					}
				}
			}
			pre = null;
			current = null;
		}
	}

}
