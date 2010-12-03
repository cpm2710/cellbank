package com.turen.lianzhang;

import android.os.Bundle;
import android.util.Log;

import com.renren.api.connect.android.Util;
import com.turen.lianzhang.listeners.ChengJiUploaderListener;

public class ChengJiUploader {
	public void uploadChengJi(final LLKImageViewActivity activity,final ChengJiUploaderListener listener,final Bundle params) {
		new Thread() {
			@Override
			public void run() {
				String response=Util.openUrl("http://turenllm.appspot.com/friendllkserver/addChengJiServlet", "POST", params);
				Log.v("response",response);
				listener.chengJiUploaded();
				activity.finish();
			}
		}.start();
	}
}
