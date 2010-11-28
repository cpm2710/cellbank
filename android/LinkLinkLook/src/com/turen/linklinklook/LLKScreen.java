package com.turen.linklinklook;

import java.util.List;
import java.util.Map;

import org.loon.framework.android.game.action.sprite.Picture;
import org.loon.framework.android.game.core.graphics.Screen;
import org.loon.framework.android.game.core.graphics.device.LGraphics;
import org.loon.framework.android.game.core.graphics.window.LButton;
import org.loon.framework.android.game.core.graphics.window.LMessage;

import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;

import com.turen.linklinklook.renren.FriendParser;
import com.turen.linklinklook.renren.RenRen;
import com.turen.linklinklook.renren.RenrenError;
import com.turen.linklinklook.renren.Util;


public class LLKScreen extends Screen {
	private RenRen renren;
	public RenRen getRenren() {
		return renren;
	}

	public void setRenren(RenRen renren) {
		this.renren = renren;
	}

	private Picture role;
	private LMessage mes;
	private Main main;
	private LButton button;
	public LLKScreen(Main main) {
		this.main=main;
	}

	public void onLoad() {
		setBackground(Images.getInstance().getImage(8));
		//renren.authorize(new LoginDialogListener());
		initUI();
	}
	private void initUI(){
		//ConnectButton button;
		
		role = new Picture(Images.getInstance().getImage(0),0,0);
		button=new LButton("login",0,0,100,100){
			public void doClick(){
				renren.authorize();
				main.setHeaderImages(FriendParser.parseHeaderImages(renren));
				//main.getHeaderImages().put(key, value)
			}
		};
		
		add(role);
		add(button);
	}
	@Override
	public void draw(LGraphics arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onTouch(float arg0, float arg1, MotionEvent arg2, int arg3,
			int arg4) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public boolean onKeyDown(int arg0, KeyEvent arg1) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean onKeyUp(int arg0, KeyEvent arg1) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean onTouchDown(MotionEvent arg0) {
		// TODO Auto-generated method stub
		
		return false;
	}

	@Override
	public boolean onTouchMove(MotionEvent arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean onTouchUp(MotionEvent arg0) {
		// TODO Auto-generated method stub
		return false;
	}	
}
