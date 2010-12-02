package com.turen.llk.imageviewedition;

import android.os.Bundle;
import android.util.Log;

import com.renren.api.connect.android.Util;
import com.turen.llk.listeners.ChengJiUploaderListener;

public class ChengJiUploader {
	public void uploadChengJi(final ChengJiUploaderListener listener,final Bundle params) {
		new Thread() {
			@Override
			public void run() {
				String response=Util.openUrl("http://turenllm.appspot.com/friendllkserver/addChengJiServlet", "POST", params);
				Log.v("response",response);
				listener.chengJiUploaded();
			}
		}.start();
	}
}
