package com.turen.lianzhang.listeners;

import android.os.Bundle;

import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.exception.RenrenError;
import com.renren.api.connect.android.view.ConnectButtonListener;
import com.turen.lianzhang.Main;

/**
 * @author 李勇(yong.li@opi-corp.com) 2010-7-15
 */
public class RenRenConnectButtonListener implements ConnectButtonListener {
	private Main example;

	public RenRenConnectButtonListener(Main example) {
		this.example = example;
	}

	@Override
	public void onLogined(Bundle values) {
		example.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				Util.showAlert(example, "已登录", "登录成功!", true);
				/*example.display.setText("sessionKey:"
						+ example.renren.getSessionKey());*/
			}
		});
	}

	@Override
	public void onLogouted(final Bundle values) {
		example.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				/*example.display.setText("sessionKey:null");*/
				Util.showAlert(example, "已注销", "注销成功!");
			}
		});
	}

	@Override
	public void onRenrenError(final RenrenError error) {
		example.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				Util.showAlert(example, "RenrenError", error.toString());
			}
		});
	}

	@Override
	public void onException(final Exception exception) {
		exception.printStackTrace();
		example.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				Util.showAlert(example, "Exception", exception.toString());
			}
		});

	}
}
