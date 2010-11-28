package com.turen.linklinklook;

import java.util.HashMap;

import org.loon.framework.android.game.LGameAndroid2DActivity;

import com.turen.linklinklook.renren.RenRen;

import android.graphics.Bitmap;


public class Main extends LGameAndroid2DActivity{
	private HashMap<String,Bitmap> headerImages=null;
	private String apiKey = "6c5eb56d05ab4bcd860dce8c05e0a474";// your apiKey

	private String apiSecret = "bfc868eaca1c44ffaaae887494ede11c";// your apiSecret
	private RenRen renren;
	@Override
	public void onMain() {		
		this.initialization(true);
		this.setFPS(40);
		this.renren = new RenRen(this, apiKey, apiSecret);
		LLKScreen screen=new LLKScreen(this);
		screen.setRenren(renren);
		this.setScreen(screen);
		this.setShowLogo(false);
		this.setShowFPS(true);
		this.showScreen();
		
		
		headerImages=new HashMap<String,Bitmap>();
	}
	public void setHeaderImages(HashMap<String,Bitmap> headerImages) {
		this.headerImages = headerImages;
	}
	public HashMap<String,Bitmap> getHeaderImages() {
		return headerImages;
	}
	
}