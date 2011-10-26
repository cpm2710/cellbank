package edu.sjtu.ist.resource;

import edu.sjtu.ist.resource.impl.ResourceMeta;
import edu.sjtu.ist.resource.impl.ResourcePropertyMeta;

public interface IResourceMetaRepository {
	void AddResource(ResourceMeta resourceMeta);//when one resource's meta data is inserted,
						//one resource service will up at the same time
	void AddRelation(ResourcePropertyMeta resourcePropertyMeta);
	void RemoveResource(ResourceMeta resourceMeta);
}
