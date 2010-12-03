package com.turen.lianzhang;

import java.util.Date;
import java.util.UUID;

import org.json.JSONObject;

import com.turen.lianzhang.HeaderPictureGrid;
import com.turen.lianzhang.LLKMainGame;
import com.turen.lianzhang.Main;
import com.turen.lianzhang.R;
import com.turen.lianzhang.domain.LevelInfo;
import com.turen.lianzhang.listeners.ChengJiUploaderListener;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.view.animation.TranslateAnimation;
import android.view.animation.Animation.AnimationListener;
import android.widget.ImageView;
import android.widget.Toast;

public class LLKHeaderGridOnClickListener implements OnClickListener {
	Context mContext;
	HeaderGridImageView pre=null;
	//private HeaderPictureGrid pre=null;
	HeaderGridImageView current=null;
	//private HeaderPictureGrid current=null;
	private android.content.DialogInterface.OnClickListener yesListener=new android.content.DialogInterface.OnClickListener(){

		@Override
		public void onClick(DialogInterface arg0, int arg1) {
			// TODO Auto-generated method stub
			ChengJiUploaderListener listener=new ChengJiUploaderListener();
			listener.showProgress(mContext, "上传您的成绩", "请耐心等待");
			ChengJiUploader uploader=new ChengJiUploader();
			Bundle params=new Bundle();
			LLKImageViewActivity m=(LLKImageViewActivity)mContext;
			
			
			params.putString("userName", m.getCurrentUser().getUsername());
			params.putString("headUrl", m.getCurrentUser().getHeadurl());
			params.putString("email", "N/A");
			LevelInfo levelInfo=m.getImageView().getLlkGame().getLevelInfo();
			int size=levelInfo.x*levelInfo.y;
			params.putString("gridSize", ""+size);
			params.putString("level",levelInfo.level+"" );
			params.putString("xiaoNeiId", m.getCurrentUser().getXiaoNeiId());
			long now=new Date().getTime();
			long miniSeconds=(now-m.getgStartTime());
			params.putString("miniSeconds", String.valueOf(miniSeconds));
			
			LLKImageViewActivity activity=(LLKImageViewActivity)mContext;
			uploader.uploadChengJi(activity,listener,params);
			
			
			
		}

		
	};
	private android.content.DialogInterface.OnClickListener noListener=new android.content.DialogInterface.OnClickListener(){
		
		@Override
		public void onClick(DialogInterface dialog, int which) {
			// TODO Auto-generated method stub
			LLKImageViewActivity activity=(LLKImageViewActivity)mContext;
			
			dialog.dismiss();
			activity.finish();
		}

		
		
	};
	LLKMainGame mGame=null;
	Toast preToast;
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
			        
			        int xDelta=toloc[0]-fromloc[0];
			        int yDelta=toloc[1]-fromloc[1];
			        TranslateAnimation translateAnimation = new TranslateAnimation(
			                0, xDelta,0,yDelta);
			        translateAnimation.setDuration(250);
			        //translateAnimation.setFillEnabled(true);
			        //translateAnimation.setFillAfter(true);
			        translateAnimation.setAnimationListener(new MoveAnimation(current));

			        pre.startAnimation(translateAnimation);
			        pre.bringToFront();
			        //current.bringToFront();
			        pre.setVisibility(View.INVISIBLE);
			        //current.setVisibility(View.INVISIBLE);
			        if(preToast!=null){
			        	preToast.cancel();
			        preToast=Toast.makeText(mContext, "~~~~"+pre.getGrid().getName()+"~~~~",Toast.LENGTH_SHORT);
			        preToast.setGravity(Gravity.BOTTOM, 0, 0);
			        preToast.show();
			        preToast.getView().bringToFront();
			        }else{
			        	preToast=Toast.makeText(mContext, "~~~~"+pre.getGrid().getName()+"~~~~",Toast.LENGTH_SHORT);
				        preToast.setGravity(Gravity.BOTTOM, 0, 0);
				        preToast.show();
				        preToast.getView().bringToFront();
			        }
					Log.v("removed",""+mGame.getGridRemoved()+" "+(mGame.getGridSize()-10));
					
					if(mGame.getGridRemoved()==(mGame.getGridSize())){
						 Dialog dialog = new AlertDialog.Builder(mContext).setTitle("是否上传您的成绩")  
						                 .setIcon(R.drawable.icon).setMessage("请选择")  
						                 // .setItems(str, Test_Dialog.this)// 设置对话框要显示的一个list  
						                 // .setSingleChoiceItems(str, 0, Test_Dialog.this)//  
						                 // 设置对话框显示一个单选的list  
						                 .setPositiveButton("确定", this.yesListener)  
						 //              .setNegativeButton("取消", Test_Dialog.this)  
						                 .setNeutralButton("退出",this.noListener)  
						                 .create();  
						dialog.show();  
						
					}
				}
			}else{
				pre = current;
				current = null;
				return;
			}
			pre = null;
			current = null;
		}
	}
class MoveAnimation implements AnimationListener{
	ImageView iv;
	public MoveAnimation(ImageView iv){
		this.iv=iv;
	}
	@Override
	public void onAnimationEnd(Animation arg0) {
		// TODO Auto-generated method stub
		iv.setVisibility(View.INVISIBLE);
	}

	@Override
	public void onAnimationRepeat(Animation arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onAnimationStart(Animation arg0) {
		// TODO Auto-generated method stub
		
	}
	
}
}
