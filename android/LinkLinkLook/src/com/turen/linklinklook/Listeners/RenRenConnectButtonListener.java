package com.turen.linklinklook.Listeners;

import android.os.Bundle;

import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.exception.RenrenError;
import com.renren.api.connect.android.view.ConnectButtonListener;
import com.turen.linklinklook.Main;


public class RenRenConnectButtonListener implements ConnectButtonListener {
	private Main main;

	public RenRenConnectButtonListener(Main main) {
		this.main = main;
	}

	@Override
	public void onLogined(Bundle values) {
		main.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				Util.showAlert(main, "Logined", "Success logined!", true);
				/*main.display.setText("sessionKey:"
						+ main.renren.getSessionKey());*/
			}
		});
	}

	@Override
	public void onLogouted(final Bundle values) {
		main.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				/*main.display.setText("sessionKey:null");*/
				Util.showAlert(main, "Logouted", "Success logouted!");
			}
		});
	}

	@Override
	public void onRenrenError(final RenrenError error) {
		main.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				Util.showAlert(main, "RenrenError", error.toString());
			}
		});
	}

	@Override
	public void onException(final Exception exception) {
		exception.printStackTrace();
		main.runOnUiThread(new Runnable() {

			@Override
			public void run() {
				Util.showAlert(main, "Exception", exception.toString());
			}
		});

	}
}
