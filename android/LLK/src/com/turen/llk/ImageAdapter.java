package com.turen.llk;

import java.util.ArrayList;
import java.util.HashMap;

import android.graphics.Bitmap;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.GridView;
import android.widget.ImageView;

public class ImageAdapter extends BaseAdapter {
	LLKMainActivity mContext;
	ArrayList<NameBitmapPair> headerImageList=null;
	public ImageAdapter(LLKMainActivity mContext,ArrayList<NameBitmapPair> headerImageList){
		this.mContext=mContext;
		this.headerImageList=headerImageList;
	}
	
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		Log.v("llk", ""+headerImageList.size());
		return headerImageList.size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public View getView(final int position, View convertView, ViewGroup parent) {
		ImageView imageView;
		  if (convertView == null) {  
		    // if it's not recycled, initialize some attributes
		    imageView = new ImageView(mContext);
		    imageView.setLayoutParams(new GridView.LayoutParams(85, 85));
		    imageView.setScaleType(ImageView.ScaleType.CENTER_CROP);
		    imageView.setPadding(8, 8, 8, 8);
		    imageView.setOnClickListener(new View.OnClickListener() {

		      @Override
		      public void onClick(View view) {
		        Log.d("onClick","position ["+position+"]");
		      }

		    });

		  } 
		  else {
		    imageView = (ImageView) convertView;
		  }

		  //imageView.setImageResource(mThumbIds[position]);
		  imageView.setImageBitmap(headerImageList.get(0).getHeaderImage());
		  return imageView;

	}

}
