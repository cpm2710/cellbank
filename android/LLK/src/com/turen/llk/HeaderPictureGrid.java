package com.turen.llk;

import android.graphics.Bitmap;

public class HeaderPictureGrid {
	private Bitmap headerImage;
	private int x;
	private int y;
	private boolean removed;
	private String name;
	private int crossNum;
	public HeaderPictureGrid(){
		crossNum=4;
		removed=false;
	}
	public Bitmap getHeaderImage() {
		return headerImage;
	}
	public void setHeaderImage(Bitmap headerImage) {
		this.headerImage = headerImage;
	}
	public int getX() {
		return x;
	}
	public void setX(int x) {
		this.x = x;
	}
	public int getY() {
		return y;
	}
	public void setY(int y) {
		this.y = y;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public void setRemoved(boolean removed) {
		this.removed = removed;
	}
	public boolean isRemoved() {
		return removed;
	}
	public void setCrossNum(int crossNum) {
		this.crossNum = crossNum;
	}
	public int getCrossNum() {
		return crossNum;
	}
}
