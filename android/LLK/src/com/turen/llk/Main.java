package com.turen.llk;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.RadioGroup;
import android.widget.RatingBar;
import android.widget.RadioGroup.OnCheckedChangeListener;

import com.renren.api.connect.android.AsyncRenren;
import com.renren.api.connect.android.Renren;
import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.view.ConnectButton;
import com.turen.llk.domain.NameHeaderUrlPair;
import com.turen.llk.listeners.FriendParser;
import com.turen.llk.listeners.RenRenConnectButtonListener;
import com.turen.llk.listeners.SimpleRequestListener;

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
		//setHeaderImages(new HashMap<String, Bitmap>());
		
		nameHeaderUrlList=new ArrayList<NameHeaderUrlPair>();
		
		if (apiKey == null || apiSecret == null) {
			Util.showAlert(this, "警告", "人人应用的apiKey和apiSecret必须提供！");
		}
		
		setContentView(R.layout.main);
		RatingBar ratingBar=(RatingBar)findViewById(R.id.levelBar);
		ratingBar.setNumStars(20);
		ratingBar.setRating(5);
		initialRenRen();
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

	private void initialFriendResources() {
		FriendParser parser = new FriendParser(this.renren);
		this.nameHeaderUrlList = parser.getFriendNameHeaderUrl();
	}

	public void onClick(View v) {
		if (v.getId() == R.id.startGame) {
			initialFriendResources();
			Intent intent=new Intent();
			intent.setClass(Main.this,LLKMainActivity.class);
			Bundle bundle=new Bundle();
			bundle.putSerializable("nameHeaderUrlList", nameHeaderUrlList);
			intent.putExtras(bundle);
			startActivity(intent);
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
						ratingBar.setRating(ratingBar.getRating()+1);
						//ratingBar.setNumStars(ratingBar.getNumStars()+1);
						//ratingBar.refreshDrawableState();
				 }
			});
		}
		if(v.getId()==R.id.levelDownButton){
			this.runOnUiThread(new Runnable(){
				 @Override
				                         public void run() { 
						RatingBar ratingBar=(RatingBar)findViewById(R.id.levelBar);
						ratingBar.setRating(ratingBar.getRating()-1);
						//ratingBar.setNumStars(ratingBar.getNumStars()-1);
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