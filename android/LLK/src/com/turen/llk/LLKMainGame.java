package com.turen.llk;

import java.util.ArrayList;

import com.turen.llk.domain.NameBitmapPair;

public class LLKMainGame {
	private ArrayList<HeaderPictureGrid> headerPictureGrids;
	ArrayList<NameBitmapPair> headerImageList=null;
	public LLKMainGame(ArrayList<NameBitmapPair> headerImageList){
		this.headerImageList=headerImageList;
		headerPictureGrids=new ArrayList<HeaderPictureGrid>();
		float division=(int)Math.sqrt(headerImageList.size());
		float horizonSize=division+1;
		float verticalSize=headerImageList.size()/horizonSize;
		int horizonSizeInt=(int)horizonSize;
		int verticalSizeInt=(int)verticalSize;
		for(int i=0;i<headerImageList.size();i++){
			HeaderPictureGrid grid=new HeaderPictureGrid();
			NameBitmapPair nbp=headerImageList.get(i);
			grid.setHeaderImage(nbp.getHeaderImage());
			grid.setX(i/horizonSizeInt);
			grid.setY(i%horizonSizeInt);
			grid.setName(nbp.getName());
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
