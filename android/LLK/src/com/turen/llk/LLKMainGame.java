package com.turen.llk;

import java.util.ArrayList;

import com.turen.llk.cache.GraphicsUtil;
import com.turen.llk.domain.NameBitmapPair;

public class LLKMainGame {
	//LLKMainActivity main;
	private ArrayList<HeaderPictureGrid> headerPictureGrids;
	ArrayList<NameBitmapPair> headerImageList=null;
	public LLKMainGame(ArrayList<NameBitmapPair> headerImageList,int screenWidth,int screenHeight){
		this.headerImageList=headerImageList;
		headerPictureGrids=new ArrayList<HeaderPictureGrid>();
		float division=(int)Math.sqrt(headerImageList.size());
		float horizonSize=division+1;
		float verticalSize=headerImageList.size()/horizonSize;
		
		int gridWidth=(int)(screenWidth/horizonSize);
		int gridHeight=gridWidth;
		
		int horizonSizeInt=(int)horizonSize;
		int verticalSizeInt=(int)verticalSize;
		//main.get
		for(int i=0;i<headerImageList.size();i++){
			HeaderPictureGrid grid=new HeaderPictureGrid();
			NameBitmapPair nbp=headerImageList.get(i);
			
			grid.setX(i/horizonSizeInt);
			grid.setY(i%horizonSizeInt);
			grid.setName(nbp.getName());
			grid.setHeaderImage(GraphicsUtil.getResize(nbp.getHeaderImage(), gridWidth, gridHeight));
			// (nbp.getHeaderImage());
			headerPictureGrids.add(grid);
		}		
	}
	public void setHeaderPictureGrids(ArrayList<HeaderPictureGrid> headerPictureGrids) {
		this.headerPictureGrids = headerPictureGrids;
	}
	public ArrayList<HeaderPictureGrid> getHeaderPictureGrids() {
		return headerPictureGrids;
	}
}
