package com.turen.lianzhang;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.widget.RatingBar;
import android.widget.Spinner;
import android.widget.Toast;

import com.turen.lianzhang.Main;
import com.turen.lianzhang.R;
import com.turen.lianzhang.domain.LevelInfo;
import com.turen.lianzhang.listeners.StartGameListener;

public class GameStarter {
	public void startGame(final Main main,final StartGameListener listener){
		new Thread() {
			@Override
			public void run() {
				float rating=main.getRatingBar().getRating();
				int friendNumber=(int)rating*10;
				main.initialFriendResources(friendNumber);
				/*Spinner s = (Spinner) main.findViewById(R.id.friendNumerSpin);
				String friendNumber=(String)s.getSelectedItem();
				try{if(friendNumber.equalsIgnoreCase("所有好友")){
					main.initialFriendResources(99999);
				}else{
					friendNumber=friendNumber.substring(friendNumber.indexOf(":")+1);
					Log.v("friendNumber",friendNumber);
					main.initialFriendResources(Integer.parseInt(friendNumber));
				}}catch(Exception e){
					Toast.makeText(main, e.getMessage(), 100).show();
				}*/
				LevelInfo levelInfo=new LevelInfo();
				RatingBar ratingBar=(RatingBar)main.findViewById(R.id.levelBar);
				rating=ratingBar.getRating();
				levelInfo.level=(int) (rating*2);
				//Log.v("rating",""+rating);
				levelInfo.x=(int)(rating*5);
				levelInfo.y=(int)(rating*5);
				//Log.v("x==",""+levelInfo.x);
				//Log.v("y==",""+levelInfo.y);
				if(levelInfo.y%2!=0){
					levelInfo.y=levelInfo.y+1;
				}
				if(levelInfo.x%2!=0){
					levelInfo.x=levelInfo.x+1;
				}
				//Log.v("x==",""+levelInfo.x);
				//Log.v("y==",""+levelInfo.y);
				
				Intent intent=new Intent();
				intent.setClass(main,LLKImageViewActivity.class);
				Bundle bundle=new Bundle();
				bundle.putSerializable("nameHeaderUrlList", main.getNameHeaderUrlList());
				bundle.putSerializable("levelInfo", levelInfo);
				bundle.putSerializable("currentUser", main.getCurrentUser());
				intent.putExtras(bundle);
				main.startActivity(intent);
				listener.gameStarted();
			}
		}.start();
	}
}
