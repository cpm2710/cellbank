package com.turen.llk;

import java.util.ArrayList;
import java.util.Random;

import com.turen.llk.cache.GraphicsUtil;
import com.turen.llk.domain.LevelInfo;
import com.turen.llk.domain.NameBitmapPair;

public class LLKMainGame {
	//LLKMainActivity main;
	private ArrayList<HeaderPictureGrid> headerPictureGrids;
	private ArrayList<NameBitmapPair> headerImageList=null;
	private HeaderPictureGrid[][]grid;
	private int gridWidth=40;
	private int gridHeight=40;
	private LevelInfo levelInfo;
	public int getGridWidth() {
		return gridWidth;
	}

	public void setGridWidth(int gridWidth) {
		this.gridWidth = gridWidth;
	}

	public int getGridHeight() {
		return gridHeight;
	}

	public void setGridHeight(int gridHeight) {
		this.gridHeight = gridHeight;
	}
	
	public HeaderPictureGrid[][] getGrid() {
		return grid;
	}

	public void setGrid(HeaderPictureGrid[][] grid) {
		this.grid = grid;
	}

	
	public LLKMainGame(ArrayList<NameBitmapPair> headerImageList,int screenWidth,int screenHeight,LevelInfo levelInfo){
		this.levelInfo=levelInfo;
		this.headerImageList=headerImageList;
		headerPictureGrids=new ArrayList<HeaderPictureGrid>();
		grid=new HeaderPictureGrid[levelInfo.x][levelInfo.y];
		
		gridWidth=(int)(screenWidth/levelInfo.x);
		gridHeight=(int)(screenHeight/levelInfo.y);
		
		Random r=new Random();
		for(int i=0;i<levelInfo.x;i++)
		{
			for(int j=0;j<levelInfo.y;j++){
				grid[i][j]=new HeaderPictureGrid();
				int index=r.nextInt(headerImageList.size());
				NameBitmapPair nbp=headerImageList.get(index);
				grid[i][j].setHeaderImage(GraphicsUtil.getResize(nbp.getHeaderImage(), gridWidth, gridHeight));
				grid[i][j].setName(nbp.getName());
				grid[i][j].setX(i);
				grid[i][j].setY(j);
			}
		}
		/*float division=(int)Math.sqrt(headerImageList.size());
		float horizonSize=division+1;
		float verticalSize=headerImageList.size()/horizonSize;
		
		int gridWidth=(int)(screenWidth/horizonSize);
		int gridHeight=gridWidth;
		
		int horizonSizeInt=(int)horizonSize;
		int verticalSizeInt=(int)verticalSize;*/
		//main.get
		/*for(int i=0;i<headerImageList.size();i++){
			HeaderPictureGrid grid=new HeaderPictureGrid();
			NameBitmapPair nbp=headerImageList.get(i);
			
			grid.setX(i/horizonSizeInt);
			grid.setY(i%horizonSizeInt);
			grid.setName(nbp.getName());
			grid.setHeaderImage(GraphicsUtil.getResize(nbp.getHeaderImage(), gridWidth, gridHeight));
			// (nbp.getHeaderImage());
			headerPictureGrids.add(grid);
		}	*/	
	}
	
	public void setHeaderPictureGrids(ArrayList<HeaderPictureGrid> headerPictureGrids) {
		this.headerPictureGrids = headerPictureGrids;
	}
	public ArrayList<HeaderPictureGrid> getHeaderPictureGrids() {
		return headerPictureGrids;
	}

	public void setLevelInfo(LevelInfo lefelInfo) {
		this.levelInfo = lefelInfo;
	}

	public LevelInfo getLevelInfo() {
		return levelInfo;
	}
}
