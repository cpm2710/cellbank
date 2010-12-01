package com.turen.llk.imageviewedition;

import com.turen.llk.HeaderPictureGrid;
import com.turen.llk.LLKMainGame;

import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;

public class LLKHeaderGridOnClickListener implements OnClickListener {
	HeaderGridImageView pre=null;
	//private HeaderPictureGrid pre=null;
	HeaderGridImageView current=null;
	//private HeaderPictureGrid current=null;
	
	LLKMainGame mGame=null;
	public LLKHeaderGridOnClickListener(LLKMainGame game){
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
			pre = current;
			current = null;
		} else {
			if(pre==current){
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
				}
			}
			pre = null;
			current = null;
		}
	}

}
