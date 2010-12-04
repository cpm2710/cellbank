package com.turen.lianzhang;

import java.util.ArrayList;

import android.content.Context;
import android.widget.GridView;

import com.turen.lianzhang.LLKMainGame;
import com.turen.lianzhang.R;
import com.turen.lianzhang.util.HeaderImageCacher;
import com.turen.lianzhang.domain.LevelInfo;
import com.turen.lianzhang.domain.NameBitmapPair;
import com.turen.lianzhang.domain.NameHeaderUrlPair;

public class LLKImageView extends GridView{

	private HeaderImageCacher cacher;
	private LLKMainGame llkGame;
	public LLKMainGame getLlkGame() {
		return llkGame;
	}
	public void setLlkGame(LLKMainGame llkGame) {
		this.llkGame = llkGame;
	}
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
		this.setBackgroundResource(R.drawable.guilie);
	}

}
