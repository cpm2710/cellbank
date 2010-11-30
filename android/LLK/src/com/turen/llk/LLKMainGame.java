package com.turen.llk;

import java.util.ArrayList;
import java.util.Random;

import android.util.Log;

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
	public void findAll(HeaderPictureGrid current,ArrayList<HeaderPictureGrid> T){
		HeaderPictureGrid now=null;
		for(int y=current.getY();y>-1;y--){
			now=grid[current.getX()][y];
			if(now.isRemoved()){
				T.add(now);
			}else{
				Log.v("llkadd","added "+now.getName());
				T.add(now);
				break;
			}
		}
		for(int y=current.getY();y<levelInfo.y;y++){
			now=grid[current.getX()][y];
			if(now.isRemoved()){
				T.add(now);
			}else{
				Log.v("llkadd","added "+now.getName());
				T.add(now);
				break;
			}
		}
		for(int x=current.getX();x>-1;x--){
			now=grid[x][current.getY()];
			if(now.isRemoved()){
				T.add(now);
			}else{
				Log.v("llkadd","added "+now.getName());
				T.add(now);
				break;
			}
		}
		for(int x=current.getX();x<levelInfo.x;x++){
			now=grid[x][current.getY()];
			if(now.isRemoved()){
				T.add(now);
			}else{
				Log.v("llkadd","added "+now.getName());
				T.add(now);
				break;
			}
		}
	}
	
	public boolean findPath(HeaderPictureGrid g1,HeaderPictureGrid g2){
		ArrayList<HeaderPictureGrid> S=new ArrayList<HeaderPictureGrid>();
		ArrayList<HeaderPictureGrid> T=new ArrayList<HeaderPictureGrid>();
		S.add(g1);		
		int crossNum = 0 ;		
		while(!S.contains(g2)&&crossNum<3){
			for(HeaderPictureGrid g : S){
				//if(g.isRemoved()){
				findAll(g,T);
				if(T.contains(g2)){
					return true;
				}
				//S.addAll(T);}
			}
			T.clear();
			crossNum++;
		}
		if(S.contains(g2)){
			return true;
		}		
		return false;
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
