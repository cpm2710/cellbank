package com.turen.linklinklook;

public class LevelInfo {
	private int xBound,yBound;
	public LevelInfo(int xBound,int yBound){
		this.setxBound(xBound);
		this.setyBound(yBound);
	}
	public void setyBound(int yBound) {
		this.yBound = yBound;
	}
	public int getyBound() {
		return yBound;
	}
	public void setxBound(int xBound) {
		this.xBound = xBound;
	}
	public int getxBound() {
		return xBound;
	}
}
