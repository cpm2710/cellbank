package com.turen.llk;

import java.util.ArrayList;
import java.util.HashMap;

import com.turen.llk.domain.NameBitmapPair;

import android.graphics.Bitmap;
import android.util.Log;
import android.view.Display;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.BaseAdapter;
import android.widget.GridView;
import android.widget.ImageView;

public class ImageAdapter extends BaseAdapter {
	LLKMainActivity mContext;
	ArrayList<NameBitmapPair> headerImageList = null;

	public ImageAdapter(LLKMainActivity mContext,
			ArrayList<NameBitmapPair> headerImageList) {
		this.mContext = mContext;
		this.headerImageList = headerImageList;
	}
	private int width =40;
	private int height =40;
	public int getWidth() {
		return width;
	}

	public void setWidth(int width) {
		this.width = width;
	}

	public int getHeight() {
		return height;
	}

	public void setHeight(int height) {
		this.height = height;
	}

	@Override
	public int getCount() {
		return headerImageList.size();
	}

	@Override
	public Object getItem(int position) {
		return null;
	}

	@Override
	public long getItemId(int position) {
		return 0;
	}

	@Override
	public View getView(final int position, View convertView, ViewGroup parent) {
		
		
		ImageView imageView;
		if (convertView == null) {
			// if it's not recycled, initialize some attributes
			imageView = new ImageView(mContext);
			imageView.setLayoutParams(new GridView.LayoutParams(width,height));
			imageView.setScaleType(ImageView.ScaleType.CENTER_CROP);
			//imageView.setPadding(8, 8, 8, 8);
			imageView.setOnClickListener(new View.OnClickListener() {

				@Override
				public void onClick(View view) {
					Log.d("onClick", "position [" + position + "]");
				}

			});

		} else {
			imageView = (ImageView) convertView;
		}

		imageView
				.setImageBitmap(headerImageList.get(position).getHeaderImage());
		return imageView;

	}

}
