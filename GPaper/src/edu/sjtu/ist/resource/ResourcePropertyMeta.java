package edu.sjtu.ist.resource;

import javax.persistence.Column;
import javax.persistence.JoinColumn;
import javax.persistence.OneToOne;

public class ResourcePropertyMeta {
	private ResourceMeta resourceMetaOne;
	private ResourceMeta resourceMetaTwo;
	private String propertyName;
	@OneToOne
	@JoinColumn(name="resource_meta_one")
	public ResourceMeta getResourceMetaOne() {
		return resourceMetaOne;
	}
	public void setResourceMetaOne(ResourceMeta resourceMetaOne) {
		this.resourceMetaOne = resourceMetaOne;
	}
	@OneToOne
	@JoinColumn(name="resource_meta_two")
	public ResourceMeta getResourceMetaTwo() {
		return resourceMetaTwo;
	}
	public void setResourceMetaTwo(ResourceMeta resourceMetaTwo) {
		this.resourceMetaTwo = resourceMetaTwo;
	}
	@Column(name = "property_name")
	public String getPropertyName() {
		return propertyName;
	}
	public void setPropertyName(String propertyName) {
		this.propertyName = propertyName;
	}
	
}
