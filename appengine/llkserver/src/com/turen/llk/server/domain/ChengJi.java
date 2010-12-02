package com.turen.llk.server.domain;

import javax.jdo.annotations.IdGeneratorStrategy;
import javax.jdo.annotations.IdentityType;
import javax.jdo.annotations.PersistenceCapable;
import javax.jdo.annotations.Persistent;
import javax.jdo.annotations.PrimaryKey;

@PersistenceCapable(identityType = IdentityType.APPLICATION)
public class ChengJi {
	@PrimaryKey
	@Persistent(valueStrategy = IdGeneratorStrategy.IDENTITY)
	private Long id;

	@Persistent
	private String username;
	@Persistent
	private String xiaoNeiId;
	@Persistent
	private String email;
	@Persistent
	private String headUrl;	
	@Persistent
	private Long miniSeconds;//it's miniseconds actually

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
	public String getHeadUrl() {
		return headUrl;
	}

	public void setHeadUrl(String headUrl) {
		this.headUrl = headUrl;
	}

	public void setXiaoNeiId(String xiaoNeiId) {
		this.xiaoNeiId = xiaoNeiId;
	}

	public String getXiaoNeiId() {
		return xiaoNeiId;
	}

	public void setMiniSeconds(Long miniSeconds) {
		this.miniSeconds = miniSeconds;
	}

	public Long getMiniSeconds() {
		return miniSeconds;
	}
}
