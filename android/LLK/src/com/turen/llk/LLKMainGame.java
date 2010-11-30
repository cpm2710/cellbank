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
	int times=0;
	public void findAll(HeaderPictureGrid current,ArrayList<HeaderPictureGrid> T){
		Log.v("llkadd","find the nodes connected to "+current.getX()+" "+current.getY());
		HeaderPictureGrid now=null;
		for(int y=current.getY()-1;y>-1;y--){
			now=grid[current.getX()][y];
			if(now.isRemoved()){
				Log.v("llkadd","up added "+now.getX()+" "+now.getY()+" "+now.getName()+" in "+times);
				T.add(now);
			}else{
				break;
			}
		}
		for(int y=current.getY()+1;y<levelInfo.y+1;y++){
			now=grid[current.getX()][y];
			if(now.isRemoved()){
				Log.v("llkadd","down added "+now.getX()+" "+now.getY()+" "+now.getName()+" in "+times);
				T.add(now);
			}else{
				break;
			}
		}
		for(int x=current.getX()-1;x>-1;x--){
			now=grid[x][current.getY()];
			if(now.isRemoved()){
				Log.v("llkadd","left added "+now.getX()+" "+now.getY()+" "+now.getName()+" in "+times);
				T.add(now);
			}else{
				break;
			}
		}
		for(int x=current.getX()+1;x<levelInfo.x+1;x++){
			now=grid[x][current.getY()];
			if(now.isRemoved()){
				Log.v("llkadd","right added "+now.getX()+" "+now.getY()+" "+now.getName()+" in "+times);
				T.add(now);
			}else{
				break;
			}
		}
	}
	
	public boolean findPath(HeaderPictureGrid g1,HeaderPictureGrid g2){
		Log.v("headerName","g1"+g1.getName());
		Log.v("headerName","g2"+g2.getName());
		
		ArrayList<HeaderPictureGrid> S=new ArrayList<HeaderPictureGrid>();
		ArrayList<HeaderPictureGrid> T=new ArrayList<HeaderPictureGrid>();
		S.add(g1);		
		int crossNum = 0 ;		
		while(!S.contains(g2)&&crossNum<3){
			for(HeaderPictureGrid g : S){
				//if(g.isRemoved()){
				times=crossNum;
				findAll(g,T);
				//if(T.contains(g2)){
				//	return true;
				//}
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
		this.setHeaderImageList(headerImageList);
		headerPictureGrids=new ArrayList<HeaderPictureGrid>();
		grid=new HeaderPictureGrid[levelInfo.x+2][levelInfo.y+2];
		
		gridWidth=(int)(screenWidth/(levelInfo.x+2));
		gridHeight=(int)(screenHeight/(levelInfo.y+2));
		
		Random r=new Random();
		int index=r.nextInt(headerImageList.size());
		for(int i=0;i<levelInfo.x+1;i++)
		{
			for(int j=0;j<levelInfo.y+1;j++){
				
				grid[i][j]=new HeaderPictureGrid();
				
				NameBitmapPair nbp=headerImageList.get(index);
				grid[i][j].setHeaderImage(GraphicsUtil.getResize(nbp.getHeaderImage(), gridWidth, gridHeight));
				grid[i][j].setName(nbp.getName());
				grid[i][j].setX(i);
				grid[i][j].setY(j);
				if(i==0||j==0){
					grid[i][j].setRemoved(true);
					grid[i][j].setName("狗剩");
				}
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

	public void setHeaderImageList(ArrayList<NameBitmapPair> headerImageList) {
		this.headerImageList = headerImageList;
	}

	public ArrayList<NameBitmapPair> getHeaderImageList() {
		return headerImageList;
	}
}
