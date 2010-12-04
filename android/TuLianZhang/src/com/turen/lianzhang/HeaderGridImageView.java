package com.turen.lianzhang;

import com.turen.lianzhang.HeaderPictureGrid;

import android.content.Context;
import android.widget.ImageView;

public class HeaderGridImageView extends ImageView{
	HeaderPictureGrid grid=null;
	public HeaderGridImageView(Context context,HeaderPictureGrid grid){
		super(context);
		this.grid=grid;
	}
	public HeaderPictureGrid getGrid() {
		return grid;
	}
	public void setGrid(HeaderPictureGrid grid) {
		this.grid = grid;
	}
	public HeaderGridImageView(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

}
