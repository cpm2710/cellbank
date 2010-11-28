package com.turen.linklinklook.imageutil;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;

import org.loon.framework.android.game.core.graphics.LImage;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;

public class ImageRetriever {
	public static Bitmap getImage(String headurl){
		URL url;
		try {
			Log.v("llk", "retreiving "+headurl);
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
	final static public BitmapFactory.Options ARGB4444options = new BitmapFactory.Options();
	static{
		ARGB4444options.inDither = false;
		ARGB4444options.inPreferredConfig = Bitmap.Config.ARGB_4444;
		try {
			BitmapFactory.Options.class.getField("inPurgeable").set(
					ARGB4444options, true);
		} catch (Exception e) {
		}
	}
	static int tryTimes=0;
	public static LImage getLImage(String headerUrl){
		URL url;
		try {
			Log.v("llk", "retreiving "+headerUrl);
			url = new URL(headerUrl);
			LImage image=new LImage(BitmapFactory.decodeStream(url.openStream()));
			tryTimes=0;
			return image;
		} catch (MalformedURLException e) {
			// TODO Auto-generated catch block
			tryTimes++;
			if(tryTimes>2){
				return null;
			}
			e.printStackTrace();
			return getLImage(headerUrl);
		} catch (IOException e) {
			tryTimes++;
			if(tryTimes>2){
				return null;
			}
			e.printStackTrace();
			return getLImage(headerUrl);
		}
	}
}
