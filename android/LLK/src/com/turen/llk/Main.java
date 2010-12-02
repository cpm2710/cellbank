package com.turen.llk;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

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
import com.turen.llk.domain.CurrentUser;
import com.turen.llk.domain.LevelInfo;
import com.turen.llk.domain.NameHeaderUrlPair;
import com.turen.llk.imageviewedition.GameStarter;
import com.turen.llk.imageviewedition.LLKImageViewActivity;
import com.turen.llk.imageviewedition.PaiHangBangListener;
import com.turen.llk.imageviewedition.PaiHangBangStarter;
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
	private long gStartTime;
	private ArrayList<NameHeaderUrlPair> nameHeaderUrlList=null;
	
	private AsyncRenren asyncRenren;
	private CurrentUser currentUser;
	
	public CurrentUser getCurrentUser() {
		return currentUser;
	}

	public void setCurrentUser(CurrentUser currentUser) {
		this.currentUser = currentUser;
	}

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
		ratingBar.setRating((float)1.5);
		initialRenRen();
		Spinner s = (Spinner) findViewById(R.id.friendNumerSpin);
		String []friendNumber=new String[]{"好友人数:10","好友人数:20","好友人数:50","所有好友"};
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
			if(currentUser==null){
				currentUser=new CurrentUser();
			}
			Bundle getUserInfoBundle=new Bundle();
			getUserInfoBundle.putString("method", "users.getLoggedInUser");
			String uIdResponse=getRenren().request(getUserInfoBundle,"json");
			JSONObject uIdObj;
			try {
				uIdObj = new JSONObject(uIdResponse);
				String uid=uIdObj.get("uid").toString();
				currentUser.setXiaoNeiId(uid);
				
				getUserInfoBundle=new Bundle();
				getUserInfoBundle.putString("method", "users.getInfo");
				String infoResponse=getRenren().request(getUserInfoBundle,"json");
				JSONArray infoJobj = new JSONArray(infoResponse);
				JSONObject obj=(JSONObject) infoJobj.get(0);
				String userName=(String)obj.get("name");
				String headerUrl=(String)obj.get("headurl");
				currentUser.setUsername(userName);
				currentUser.setHeadurl(headerUrl);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
				return;
			}
			
			
			
			StartGameListener startGameListener=new StartGameListener();
			startGameListener.showProgress(this, "加载好友头像资源...","请耐心等待...");
			
			GameStarter gameStarter=new GameStarter();
			gameStarter.startGame(this, startGameListener);
		}
		if (v.getId() == R.id.connectFacebook) {
			
		}
		if(v.getId()==R.id.levelBar){
			
		}
		/*if(v.getId()==R.id.levelUpButton){
			this.runOnUiThread(new Runnable(){
				 @Override
				 public void run() { 
						RatingBar ratingBar=(RatingBar)findViewById(R.id.levelBar);
						if(ratingBar.getRating()<3){
						ratingBar.setRating((float) (ratingBar.getRating()+0.5));}
				 }
			});
		}
		if(v.getId()==R.id.levelDownButton){
			this.runOnUiThread(new Runnable(){
				 @Override
				 public void run() { 
						RatingBar ratingBar=(RatingBar)findViewById(R.id.levelBar);
						if(ratingBar.getRating()>0){							
						ratingBar.setRating((float) (ratingBar.getRating()-0.5));}
				 }
			});
		
		}*/
		if(v.getId()==R.id.statistics){
			PaiHangBangListener startGameListener=new PaiHangBangListener(this);
			startGameListener.showProgress(this, "跳转到排行榜","请耐心等待...");
			
			PaiHangBangStarter gameStarter=new PaiHangBangStarter();
			gameStarter.startPaiHangBang(startGameListener);
		}
	}
	public Renren getRenren() {
		return renren;
	}

	public void setRenren(Renren renren) {
		this.renren = renren;
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

	public void setgStartTime(long gStartTime) {
		this.gStartTime = gStartTime;
	}

	public long getgStartTime() {
		return gStartTime;
	}
}