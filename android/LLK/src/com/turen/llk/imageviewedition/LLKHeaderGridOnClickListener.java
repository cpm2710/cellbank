package com.turen.llk.imageviewedition;

import com.turen.llk.HeaderPictureGrid;
import com.turen.llk.LLKMainGame;
import com.turen.llk.R;

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
		// TODO Auto-generated method stub
		HeaderGridImageView view=(HeaderGridImageView)v;
		//view.setVisibility(view.INVISIBLE);
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
					Log.v("remove","removed "+pre.getGrid().getName()+" "+pre.getGrid().getName());
					pre.setVisibility(pre.INVISIBLE);
					current.setVisibility(current.INVISIBLE);
					Animation Anim_Alpha = AnimationUtils.loadAnimation(mContext, R.anim.alpha_action);  
					pre.startAnimation(Anim_Alpha); 
					Animation Anim_Alpha2 = AnimationUtils.loadAnimation(mContext, R.anim.alpha_action);  
					current.startAnimation(Anim_Alpha); 
					
				}
			}
			pre = null;
			current = null;
		}
	}

}
