package com.turen.llk.cache;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;

import com.turen.llk.domain.NameBitmapPair;
import com.turen.llk.domain.NameHeaderUrlPair;
import com.turen.llk.util.ImageRetriever;

import android.app.Activity;
import android.graphics.Bitmap;

public class HeaderImageCacher {
	private Activity activity;

	public HeaderImageCacher(Activity activity) {
		this.activity = activity;
	}
	public ArrayList<NameBitmapPair> getNameBitmapList(ArrayList<NameHeaderUrlPair> nameHeaderUrls){
		ArrayList<NameBitmapPair> pairs=new ArrayList<NameBitmapPair>();
		for(NameHeaderUrlPair pair : nameHeaderUrls){
			NameBitmapPair p=new NameBitmapPair();
			p.setName(pair.getName());
			Bitmap fetched=fetchImage(pair.getName());
			if(fetched!=null){
				p.setHeaderImage(fetched);
			}else{
			Bitmap map=ImageRetriever.getImage(pair.getHeaderUrl());
			p.setHeaderImage(map);
			this.writeImage(pair.getName(), map);
			}
			pairs.add(p);
		}
		return pairs;
	}
	public void writeImage(String name,Bitmap bitmap){
		try {
			activity.deleteFile(name);
			bitmap.compress(Bitmap.CompressFormat.PNG, 80,
					activity.openFileOutput(name,activity.MODE_WORLD_WRITEABLE));
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	public Bitmap fetchImage(String name) {
		try {
			FileInputStream fis = activity.openFileInput(name);
			Bitmap map=GraphicsUtil.loadBitmap(fis, true);
			fis.close();
			return map;
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			//e.printStackTrace();
			return null;
		} catch (IOException e) {
			//e.printStackTrace();
			return null;
		}

	}
}
