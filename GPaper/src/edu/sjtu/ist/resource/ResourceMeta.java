package edu.sjtu.ist.resource;

import java.io.Serializable;

public class ResourceMeta implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private int resourceId;
	public String resourcePath;
	public int getResourceId() {
		return resourceId;
	}
	public void setResourceId(int resourceId) {
		this.resourceId = resourceId;
	}
	public String getResourcePath() {
		return resourcePath;
	}
	public void setResourcePath(String resourcePath) {
		this.resourcePath = resourcePath;
	}
}
