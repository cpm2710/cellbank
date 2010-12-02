package com.turen.llk.imageviewedition;

import android.content.Context;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.GridView;
import android.widget.ImageView;

import com.turen.llk.HeaderPictureGrid;
import com.turen.llk.LLKMainGame;

public class HeaderImageAdapter extends BaseAdapter {
	LLKMainGame mGame;
	Context mContext;
	LLKHeaderGridOnClickListener clickListener=null;
	public HeaderImageAdapter(Context c,LLKMainGame mGame){
		this.mContext=c;
		this.mGame=mGame;
		clickListener=new LLKHeaderGridOnClickListener(mContext,mGame);
	}
	@Override
	public int getCount() {
		return (this.mGame.getLevelInfo().x+2)*(this.mGame.getLevelInfo().y+2);
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
	public View getView(int position, View convertView, ViewGroup parent) {
		HeaderGridImageView imageView;
        if (convertView == null) {  // if it's not recycled, initialize some attributes
            imageView = new HeaderGridImageView(mContext);
            imageView.setLayoutParams(new GridView.LayoutParams(this.mGame.getGridWidth(), this.mGame.getGridHeight()));
            imageView.setScaleType(ImageView.ScaleType.CENTER_CROP);
            imageView.setPadding(0, 0,0, 0);
        } else {
            imageView = (HeaderGridImageView) convertView;
        }
        
        int x=position%(mGame.getLevelInfo().x+2);
        int y=position/(mGame.getLevelInfo().x+2);
        //Log.v("position==",""+position);
        //Log.v("x==",""+x);
		//Log.v("y==",""+y);
        HeaderPictureGrid grid=mGame.getGrid()[y][x];
        imageView.setGrid(grid);
        if(grid.isRemoved()){
        	imageView.setVisibility(imageView.INVISIBLE);
        }
        imageView.setImageBitmap(grid.getHeaderImage());
        imageView.setOnClickListener(this.clickListener);
        return imageView;

	}

}
