package com.turen.llk.imageviewedition;

import java.util.Date;
import java.util.UUID;

import org.json.JSONObject;

import com.turen.llk.HeaderPictureGrid;
import com.turen.llk.LLKMainGame;
import com.turen.llk.Main;
import com.turen.llk.R;
import com.turen.llk.listeners.ChengJiUploaderListener;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.view.animation.TranslateAnimation;

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
			return;
		}
		if(current.getGrid().isRemoved()){
			pre=null;
			current=null;
			return;
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
				if (this.mGame.findPath2(pre.getGrid(), current.getGrid())) {
					pre.getGrid().setRemoved(true);
					current.getGrid().setRemoved(true);
					mGame.setGridRemoved(mGame.getGridRemoved()+2);
					Log.v("remove","removed "+pre.getGrid().getName()+" "+pre.getGrid().getName());
					//pre.setVisibility(pre.INVISIBLE);
					//current.setVisibility(current.INVISIBLE);
					/*Animation Anim_Alpha = AnimationUtils.loadAnimation(mContext, R.anim.alpha_action);  
					pre.startAnimation(Anim_Alpha); 
					Animation Anim_Alpha2 = AnimationUtils.loadAnimation(mContext, R.anim.alpha_action);  
					current.startAnimation(Anim_Alpha2);*/
					
					int fromloc[] = new int[2];
					int toloc[]=new int[2];
			        pre.getLocationOnScreen(fromloc);
			        // Just used to print out the images position
			        //Toast.makeText(ctx, "Position on screen: x = " + loc[0] + " y = " + loc[1], 5000).show();
			        int fromXDelta =fromloc[0];
			        int fromYDelta = fromloc[1];
			        current.getLocationOnScreen(toloc);
			        int toXDelta = toloc[0];
			        int toYDelta = toloc[1];

			        TranslateAnimation translateAnimation = new TranslateAnimation(
			                -fromXDelta,- toXDelta, -fromYDelta,-toYDelta);
			        translateAnimation.setDuration(800);
			        translateAnimation.setFillEnabled(true);

			        pre.startAnimation(translateAnimation);
			        pre.setVisibility(View.INVISIBLE);
			        current.setVisibility(View.INVISIBLE);
					
					
					Log.v("removed",""+mGame.getGridRemoved()+" "+(mGame.getGridSize()-10));
					if(mGame.getGridRemoved()==(mGame.getGridSize())){
						ChengJiUploaderListener listener=new ChengJiUploaderListener();
						listener.showProgress(mContext, "上传您的成绩", "请耐心等待");
						ChengJiUploader uploader=new ChengJiUploader();
						Bundle params=new Bundle();
						LLKImageViewActivity m=(LLKImageViewActivity)mContext;
						
						
						params.putString("userName", m.getCurrentUser().getUsername());
						params.putString("headUrl", m.getCurrentUser().getHeadurl());
						params.putString("email", "N/A");
						
						params.putString("xiaoNeiId", m.getCurrentUser().getXiaoNeiId());
						long now=new Date().getTime();
						long miniSeconds=(now-m.getgStartTime());
						params.putString("miniSeconds", String.valueOf(miniSeconds));
						
						
						uploader.uploadChengJi(listener,params);
					}
				}
			}
			pre = null;
			current = null;
		}
	}

}
