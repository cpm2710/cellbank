package com.turen.llk.listeners;

import android.app.ProgressDialog;
import android.content.Context;

import com.turen.llk.Main;

public class ChengJiUploaderListener {
	private ProgressDialog progress;
	public void showProgress(Context context,String title,String text){
		progress = ProgressDialog.show(context, title, text);
		progress.show();
	}
	public void chengJiUploaded(){
		progress.dismiss();
	}
}
