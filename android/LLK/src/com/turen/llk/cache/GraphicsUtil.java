package com.turen.llk.cache;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;


import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Matrix;
import android.graphics.drawable.BitmapDrawable;

public class GraphicsUtil {
	final static private Matrix matrix = new Matrix();
	final static private Canvas canvas = new Canvas();
	final static public BitmapFactory.Options ARGB4444options = new BitmapFactory.Options();

	final static public BitmapFactory.Options ARGB8888options = new BitmapFactory.Options();

	final static public BitmapFactory.Options RGB565options = new BitmapFactory.Options();
	static {
		ARGB8888options.inDither = false;
		ARGB8888options.inPreferredConfig = Bitmap.Config.ARGB_8888;
		ARGB4444options.inDither = false;
		ARGB4444options.inPreferredConfig = Bitmap.Config.ARGB_4444;
		RGB565options.inDither = false;
		RGB565options.inPreferredConfig = Bitmap.Config.RGB_565;
		try {
			BitmapFactory.Options.class.getField("inPurgeable").set(
					ARGB8888options, true);
			BitmapFactory.Options.class.getField("inPurgeable").set(
					ARGB4444options, true);
			BitmapFactory.Options.class.getField("inPurgeable").set(
					RGB565options, true);
		} catch (Exception e) {
		}
	}
	/**
	 * 加载标准位图文件
	 * 
	 * @param in
	 * @param transparency
	 * @return
	 */
	final static public Bitmap loadBitmap(InputStream in, boolean transparency) {
		return BitmapFactory.decodeStream(in, null,
				transparency ? ARGB4444options : RGB565options);
	}
	/**
	 * 改变指定Bitmap大小
	 * 
	 * @param image
	 * @param w
	 * @param h
	 * @return
	 */
	public static Bitmap getResize(Bitmap image, int w, int h, boolean flag) {
		int width = image.getWidth();
		int height = image.getHeight();
		if (width == w && height == h) {
			return image;
		}
		int newWidth = w;
		int newHeight = h;
		float scaleWidth = ((float) newWidth) / width;
		float scaleHeight = ((float) newHeight) / height;
		matrix.reset();
		matrix.postScale(scaleWidth, scaleHeight);
		Bitmap resizedBitmap = Bitmap.createBitmap(image, 0, 0, width, height,
				matrix, flag);
		return resizedBitmap;
	}
	/**
	 * 改变指定Bitmap大小
	 * 
	 * @param image
	 * @param w
	 * @param h
	 * @return
	 */
	public static Bitmap getResize(Bitmap image, int w, int h) {
		return getResize(image, w, h, true);
	}

	/**
	 * 转换位图大小，创建为ARGB4444格式
	 * 
	 * @param bmp
	 * @param w
	 * @param h
	 * @return
	 */
	public static Bitmap resizeBitmap(Bitmap bmp, int w, int h) {
		Bitmap result = Bitmap.createBitmap(w, h, Bitmap.Config.ARGB_4444);
		canvas.setBitmap(result);
		BitmapDrawable drawable = new BitmapDrawable(bmp);
		drawable.setBounds(0, 0, bmp.getWidth(), bmp.getHeight());
		drawable.draw(canvas);
		return result;
	}
	/**
	 * 将指定图像保存为PNG格式
	 * 
	 * @param bitmap
	 * @param output
	 * @return
	 * @throws FileNotFoundException
	 */
	public static boolean saveAsPNG(Bitmap bitmap, String fileName)
			throws FileNotFoundException {
		return bitmap.compress(Bitmap.CompressFormat.PNG, 1,
				new FileOutputStream(fileName));
	}
}
