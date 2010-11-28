package com.turen.linklinklook;

import java.util.HashMap;
import java.util.List;

import android.app.Activity;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.view.View;
import android.widget.RadioGroup;
import android.widget.RadioGroup.OnCheckedChangeListener;

public class CopyOfMain {
/*extends Activity implements OnCheckedChangeListener {
private String apiKey = "6c5eb56d05ab4bcd860dce8c05e0a474";// your apiKey

	private String apiSecret = "bfc868eaca1c44ffaaae887494ede11c";// your
																	// apiSecret
	String dataFormat = "json";
	private Renren renren;

	private HashMap<String,Bitmap> headerImages=null;
	private AsyncRenren asyncRenren;

	private SimpleRequestListener simpleRequestListener;

	private ConnectButton login;

	*//** Called when the activity is first created. *//*
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setHeaderImages(new HashMap<String,Bitmap>());
		if (apiKey == null || apiSecret == null) {
			Util.showAlert(this, "警告", "人人应用的apiKey和apiSecret必须提供！");
		}
		setContentView(R.layout.main);
		this.renren = new Renren(this, apiKey, apiSecret);
		this.asyncRenren = new AsyncRenren(renren);

		this.login = (ConnectButton) findViewById(R.id.login);
		this.login.init(renren);
		this.login.setConnectButtonListener(new RenRenConnectButtonListener(
				this));

		this.simpleRequestListener = new SimpleRequestListener(this);
	}

	public void onClick(View v) {
		if (v.getId() == R.id.startGame) {
			// simpleRequestListener.showProgress("获取图片元素等等");
			Bundle params = new Bundle();
			params.putString("method", "friends.getFriends");
			params.putString("page", "1");
			params.putString("count", "10");
			FriendRequestListener listener = new FriendRequestListener(this,
					dataFormat);
			this.asyncRenren.request(params, listener, dataFormat);
		}
	}

	@Override
	public void onCheckedChanged(RadioGroup arg0, int arg1) {
		// TODO Auto-generated method stub
		
	}

	public void setHeaderImages(HashMap<String,Bitmap> headerImages) {
		this.headerImages = headerImages;
	}

	public HashMap<String,Bitmap> getHeaderImages() {
		return headerImages;
	}*/
}