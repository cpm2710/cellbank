package com.turen.linklinklook.Listeners;

import android.app.ProgressDialog;

import com.renren.api.connect.android.RequestListener;
import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.exception.RenrenError;
import com.turen.linklinklook.Main;

public class SimpleRequestListener implements RequestListener {
	Main main;
	private ProgressDialog progress;
	private Thread uiThread;

	public SimpleRequestListener(Main main) {
		this.main = main;
		uiThread = Thread.currentThread();
	}
	void showProgress(String title) {
		progress = ProgressDialog.show(main, title, "Loading...");
		progress.show();
	}

	@Override
	public void onComplete(final String response) {
		this.updateDisplay("Response Complete", response);
	}

	@Override
	public void onFault(final Throwable fault) {
		fault.printStackTrace();
		this.updateDisplay("Fault", fault.toString());
	}

	@Override
	public void onRenrenError(final RenrenError e) {
		e.printStackTrace();
		this.updateDisplay("RenrenError", e.toString());
	}

	void updateDisplay(final String title, final String text) {
		if (Thread.currentThread() == this.uiThread) {
			this.showResult(title, text);
		} else {
			main.runOnUiThread(new Runnable() {
				@Override
				public void run() {
					showResult(title, text);
				}
			});
		}
	}

	private void showResult(final String title, final String text) {
		if (progress != null)
			progress.dismiss();
		Util.showAlert(main, title, text);
	}

}
