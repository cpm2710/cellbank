package com.turen.llk.imageviewedition;

import com.turen.llk.listeners.ChengJiUploaderListener;

public class ChengJiUploader {
	public void uploadChengJi(final ChengJiUploaderListener listener) {
		new Thread() {
			@Override
			public void run() {
				
				listener.chengJiUploaded();
			}
		}.start();
	}
}
