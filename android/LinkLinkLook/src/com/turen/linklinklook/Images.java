package com.turen.linklinklook;

import org.loon.framework.android.game.core.graphics.LImage;
import org.loon.framework.android.game.core.resource.LPKResource;
import org.loon.framework.android.game.utils.GraphicsUtils;


public class Images {

	private static Images imagefactory;

	private static LImage images[];

	private Images() {

		images = new LImage[9];
		for (int i = 0; i < 8; i++) {
			images[i] = GraphicsUtils.loadImage("res/" + i + ".jpg",false);
		}
		images[8]=GraphicsUtils.loadImage("res/background.jpg",false);
	}

	public LImage getImage(int i) {
		return images[i];
	}

	public static synchronized Images getInstance() {
		if (imagefactory != null) {
			return imagefactory;
		} else {
			imagefactory = new Images();
			return imagefactory;
		}
	}

}
