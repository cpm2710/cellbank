package com.turen.llk;

import android.graphics.Bitmap;

public class HeaderPictureGrid {
	private Bitmap headerImage;
	private int x;
	private int y;
	private String name;
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
}
