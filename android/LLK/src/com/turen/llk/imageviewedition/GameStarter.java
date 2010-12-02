package com.turen.llk.imageviewedition;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.widget.RatingBar;
import android.widget.Spinner;

import com.turen.llk.Main;
import com.turen.llk.R;
import com.turen.llk.domain.LevelInfo;
import com.turen.llk.listeners.StartGameListener;

public class GameStarter {
	public void startGame(final Main main,final StartGameListener listener){
		new Thread() {
			@Override
			public void run() {
				Spinner s = (Spinner) main.findViewById(R.id.friendNumerSpin);
				String friendNumber=(String)s.getSelectedItem();
				if(friendNumber.equalsIgnoreCase("all")){
					main.initialFriendResources(99999);
				}else{
					main.initialFriendResources(Integer.parseInt(friendNumber));
				}			
				LevelInfo levelInfo=new LevelInfo();
				RatingBar ratingBar=(RatingBar)main.findViewById(R.id.levelBar);
				float rating=ratingBar.getRating();
				//Log.v("rating",""+rating);
				levelInfo.x=(int)(rating*5);
				levelInfo.y=(int)(rating*5);
				//Log.v("x==",""+levelInfo.x);
				//Log.v("y==",""+levelInfo.y);
				if(levelInfo.x%2!=0&&levelInfo.y%2!=0){
					levelInfo.y=levelInfo.y+1;
				}
				//Log.v("x==",""+levelInfo.x);
				//Log.v("y==",""+levelInfo.y);
				
				Intent intent=new Intent();
				intent.setClass(main,LLKImageViewActivity.class);
				Bundle bundle=new Bundle();
				bundle.putSerializable("nameHeaderUrlList", main.getNameHeaderUrlList());
				bundle.putSerializable("levelInfo", levelInfo);
				intent.putExtras(bundle);
				main.startActivity(intent);
				listener.gameStarted();
			}
		}.start();
	}
}
