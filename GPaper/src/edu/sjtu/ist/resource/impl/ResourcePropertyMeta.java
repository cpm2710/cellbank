package edu.sjtu.ist.resource.impl;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToOne;
import javax.persistence.Table;
@Entity
@Table(name="resource_property_meta")
public class ResourcePropertyMeta {
	private int propertyMetaId;
	
	private ResourceMeta resourceMetaOne;
	private ResourceMeta resourceMetaTwo;
	private String propertyName;
	@Id
	@Column(name = "property_meta_id")
	@GeneratedValue(strategy=GenerationType.AUTO)
	public int getPropertyMetaId() {
		return propertyMetaId;
	}
	public void setPropertyMetaId(int propertyMetaId) {
		this.propertyMetaId = propertyMetaId;
	}@OneToOne
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
