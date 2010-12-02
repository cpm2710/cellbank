package com.turen.llk.domain;

import java.io.Serializable;

public class CurrentUser implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private Long id;
	private String username;
	private String email;
	private Integer seconds;
	private String xiaoNeiId;
	private String headurl;
	
	public void setId(Long id) {
		this.id = id;
	}

	public Long getId() {
		return id;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public String getUsername() {
		return username;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getEmail() {
		return email;
	}

	public Integer getSeconds() {
		return seconds;
	}

	public void setSeconds(Integer seconds) {
		this.seconds = seconds;
	}

	public void setXiaoNeiId(String xiaoNeiId) {
		this.xiaoNeiId = xiaoNeiId;
	}

	public String getXiaoNeiId() {
		return xiaoNeiId;
	}

	public void setHeadurl(String headurl) {
		this.headurl = headurl;
	}

	public String getHeadurl() {
		return headurl;
	}
}
