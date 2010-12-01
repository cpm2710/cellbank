package com.turen.llk.imageviewedition;

import java.util.ArrayList;

import android.content.Context;
import android.widget.GridView;

import com.turen.llk.LLKMainGame;
import com.turen.llk.R;
import com.turen.llk.cache.HeaderImageCacher;
import com.turen.llk.domain.LevelInfo;
import com.turen.llk.domain.NameBitmapPair;
import com.turen.llk.domain.NameHeaderUrlPair;

public class LLKImageView extends GridView{

	private HeaderImageCacher cacher;
	private LLKMainGame llkGame;
	public LLKImageView(Context context,HeaderImageCacher cacher,
			ArrayList<NameHeaderUrlPair> nameHeaderUrlList,
			int screenWidth,
			int screenHeight,
			LevelInfo levelInfo) {
		super(context);
		this.cacher=cacher;
		ArrayList<NameBitmapPair> headerImageList=this.cacher.getNameBitmapList(nameHeaderUrlList);
		
		this.llkGame=new LLKMainGame(headerImageList,screenWidth,screenHeight,levelInfo);
		this.setNumColumns(llkGame.getLevelInfo().x+2);
		
		HeaderImageAdapter adapter=new HeaderImageAdapter(context,llkGame);
		this.setAdapter(adapter);
		this.setBackgroundResource(R.drawable.bg);
	}

}
