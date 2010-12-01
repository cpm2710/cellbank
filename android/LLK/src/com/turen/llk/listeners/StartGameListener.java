package com.turen.llk.listeners;

import android.app.ProgressDialog;
import android.content.Context;

import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.exception.RenrenError;
import com.turen.llk.Main;

public class StartGameListener {
	private ProgressDialog progress;
	public void showProgress(Main context,String title,String text){
		progress = ProgressDialog.show(context, title, text);
		progress.show();
	}
	public void gameStarted(){
		progress.dismiss();
	}
}
