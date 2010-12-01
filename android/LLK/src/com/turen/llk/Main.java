package com.turen.llk;

import java.util.ArrayList;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.ArrayAdapter;
import android.widget.RadioGroup;
import android.widget.RatingBar;
import android.widget.Spinner;
import android.widget.RadioGroup.OnCheckedChangeListener;

import com.renren.api.connect.android.AsyncRenren;
import com.renren.api.connect.android.Renren;
import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.view.ConnectButton;
import com.turen.llk.domain.LevelInfo;
import com.turen.llk.domain.NameHeaderUrlPair;
import com.turen.llk.imageviewedition.GameStarter;
import com.turen.llk.imageviewedition.LLKImageViewActivity;
import com.turen.llk.listeners.FriendParser;
import com.turen.llk.listeners.RenRenConnectButtonListener;
import com.turen.llk.listeners.SimpleRequestListener;
import com.turen.llk.listeners.StartGameListener;

public class Main extends Activity implements OnCheckedChangeListener {
	private String apiKey = "6c5eb56d05ab4bcd860dce8c05e0a474";// your apiKey

	private String apiSecret = "bfc868eaca1c44ffaaae887494ede11c";// your
	// apiSecret
	String dataFormat = "json";
	private Renren renren;
	
	
	private ArrayList<NameHeaderUrlPair> nameHeaderUrlList=null;
	
	private AsyncRenren asyncRenren;

	private SimpleRequestListener simpleRequestListener;

	private ConnectButton connectRenRen;

	// ** Called when the activity is first created. *//*
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE); // 隐藏标题
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,

		WindowManager.LayoutParams.FLAG_FULLSCREEN); // 设置全屏
		
		nameHeaderUrlList=new ArrayList<NameHeaderUrlPair>();
		
		if (apiKey == null || apiSecret == null) {
			Util.showAlert(this, "警告", "人人应用的apiKey和apiSecret必须提供！");
		}
		
		setContentView(R.layout.main);
		
		RatingBar ratingBar=(RatingBar)findViewById(R.id.levelBar);
		ratingBar.setMax(6);
		ratingBar.setNumStars(3);
		ratingBar.setStepSize((float) 0.5);
		ratingBar.setRating(3);
		initialRenRen();
		Spinner s = (Spinner) findViewById(R.id.friendNumerSpin);
		String []friendNumber=new String[]{"10","20","50","All"};
		ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
				android.R.layout.simple_spinner_item, friendNumber);
		s.setAdapter(adapter);		
	}

	private void initialRenRen() {
		this.renren = new Renren(this, apiKey, apiSecret);
		this.asyncRenren = new AsyncRenren(renren);

		this.connectRenRen = (ConnectButton) findViewById(R.id.connectRenRen);
		this.connectRenRen.init(renren);
		this.connectRenRen
				.setConnectButtonListener(new RenRenConnectButtonListener(this));

		this.simpleRequestListener = new SimpleRequestListener(this);
	}

	public void initialFriendResources(int friendNumber) {
		FriendParser parser = new FriendParser(this.renren);
		
		this.nameHeaderUrlList = parser.getFriendNameHeaderUrl(friendNumber);
	}

	public void onClick(View v) {
		if (v.getId() == R.id.startGame) {
			StartGameListener startGameListener=new StartGameListener();
			startGameListener.showProgress(this, "加载好友头像资源...","请耐心等待...");
			
			GameStarter gameStarter=new GameStarter();
			gameStarter.startGame(this, startGameListener);
		}
		if (v.getId() == R.id.connectFacebook) {
			
		}
		if(v.getId()==R.id.levelBar){
			
		}
		if(v.getId()==R.id.levelUpButton){
			this.runOnUiThread(new Runnable(){
				 @Override
				 public void run() { 
						RatingBar ratingBar=(RatingBar)findViewById(R.id.levelBar);
						if(ratingBar.getRating()<3){
						ratingBar.setRating(ratingBar.getRating()+1);}
				 }
			});
		}
		if(v.getId()==R.id.levelDownButton){
			this.runOnUiThread(new Runnable(){
				 @Override
				 public void run() { 
						RatingBar ratingBar=(RatingBar)findViewById(R.id.levelBar);
						if(ratingBar.getRating()>0){
						ratingBar.setRating(ratingBar.getRating()-1);}
				 }
			});
		
		}
	}

	@Override
	public void onCheckedChanged(RadioGroup arg0, int arg1) {
		// TODO Auto-generated method stub

	}

	public void setNameHeaderUrlList(ArrayList<NameHeaderUrlPair> nameHeaderUrlList) {
		this.nameHeaderUrlList = nameHeaderUrlList;
	}

	public ArrayList<NameHeaderUrlPair> getNameHeaderUrlList() {
		return nameHeaderUrlList;
	}
}