package com.turen.lianzhang.domain;

import java.io.Serializable;

import android.graphics.Bitmap;

public class NameBitmapPair implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private String name;
	private Bitmap headerImage;
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public Bitmap getHeaderImage() {
		return headerImage;
	}
	public void setHeaderImage(Bitmap headerImage) {
		this.headerImage = headerImage;
	}
}
