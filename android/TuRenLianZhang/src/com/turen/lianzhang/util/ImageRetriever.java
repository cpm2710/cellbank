package com.turen.lianzhang.util;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;


import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;

//thread dangerous
public class ImageRetriever {
	static int tryTimes=0;
	public static Bitmap getImage(String headurl){
		URL url;
		try {
			Log.v("llk", "retreiving "+headurl);
			url = new URL(headurl);
			Bitmap bmp = BitmapFactory.decodeStream(url.openStream());
			tryTimes=0;
			return bmp;
		} catch (MalformedURLException e) {
			tryTimes++;
			e.printStackTrace();
			if(tryTimes>2){
				return null;
			}
			return getImage(headurl);
		} catch (IOException e) {
			tryTimes++;
			e.printStackTrace();
			if(tryTimes>2){
				return null;
			}
			return getImage(headurl);
		}
	}	
}
