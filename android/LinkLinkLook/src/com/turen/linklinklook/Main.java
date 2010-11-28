package com.turen.linklinklook;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.loon.framework.android.game.LGameAndroid2DActivity;
import org.loon.framework.android.game.core.graphics.LImage;

import com.turen.linklinklook.renren.RenRen;

import android.graphics.Bitmap;


public class Main extends LGameAndroid2DActivity{
	private HashMap<String,LImage> headerImages=null;
	private List<String> headerUrls=null;
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
		
		
		headerImages=new HashMap<String,LImage>();
		headerUrls=new ArrayList<String>();
	}
	public List<String> getHeaderUrls() {
		return headerUrls;
	}
	public void setHeaderUrls(List<String> headerUrls) {
		this.headerUrls = headerUrls;
	}
	public void setHeaderImages(HashMap<String,LImage> headerImages) {
		this.headerImages = headerImages;
		this.headerUrls.addAll(headerImages.keySet());
	}
	public HashMap<String,LImage> getHeaderImages() {
		return headerImages;
	}
	
}