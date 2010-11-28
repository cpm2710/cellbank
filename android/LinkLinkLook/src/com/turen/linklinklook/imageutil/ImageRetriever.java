package com.turen.linklinklook.imageutil;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

public class ImageRetriever {
	public static Bitmap getImage(String headurl){
		URL url;
		try {
			url = new URL(headurl);
			Bitmap bmp = BitmapFactory.decodeStream(url.openStream());
			return bmp;
		} catch (MalformedURLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return getImage(headurl);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return getImage(headurl);
		}
	}
}
