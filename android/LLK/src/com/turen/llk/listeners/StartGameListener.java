package com.turen.llk.listeners;

import java.util.Date;

import android.app.ProgressDialog;
import android.content.Context;

import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.exception.RenrenError;
import com.turen.llk.Main;

public class StartGameListener {
	private ProgressDialog progress;
	Main context;
	public void showProgress(Main context,String title,String text){
		this.context=context;
		progress = ProgressDialog.show(context, title, text);
		progress.show();
	}
	public void gameStarted(){
		context.setgStartTime(new Date().getTime());
		progress.dismiss();
	}
}
