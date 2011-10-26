package edu.sjtu.ist.resource.impl;

import java.io.Serializable;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
@Entity
@Table(name="resource_meta")
public class ResourceMeta implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private int resourceId;
	public String resourcePath;
	public String resourceDefinition;
	
	@Id
	@Column(name = "resource_id")
	@GeneratedValue(strategy=GenerationType.AUTO)
	public int getResourceId() {
		return resourceId;
	}
	public void setResourceId(int resourceId) {
		this.resourceId = resourceId;
	}
	@Column(name="resource_path")
	public String getResourcePath() {
		return resourcePath;
	}
	public void setResourcePath(String resourcePath) {
		this.resourcePath = resourcePath;
	}
	
	@Column(name="resource_definition")
	public String getResourceDefinition() {
		return resourceDefinition;
	}
	public void setResourceDefinition(String resourceDefinition) {
		this.resourceDefinition = resourceDefinition;
	}
}
