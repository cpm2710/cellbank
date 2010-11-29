package com.turen.llk.domain;

import java.io.Serializable;

public class NameHeaderUrlPair implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private String name;
	private String headerUrl;
	public NameHeaderUrlPair(String name,String headerUrl){
		this.name=name;
		this.headerUrl=headerUrl;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getHeaderUrl() {
		return headerUrl;
	}
	public void setHeaderUrl(String headerUrl) {
		this.headerUrl = headerUrl;
	}
}
