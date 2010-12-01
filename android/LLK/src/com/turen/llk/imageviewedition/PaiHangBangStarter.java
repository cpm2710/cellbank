package com.turen.llk.imageviewedition;

import com.turen.llk.Main;
import com.turen.llk.listeners.StartGameListener;

public class PaiHangBangStarter {
	public void startPaiHangBang(final PaiHangBangListener listener) {
		new Thread() {
			@Override
			public void run() {
				
				listener.paiHangBangOnComplete();
			}
		}.start();
	}
}
