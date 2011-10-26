package edu.sjtu.ist.resource;

public interface IResourceMetaRepository {
	void AddResource(ResourceMeta resourceMeta);//when one resource's meta data is inserted,
						//one resource service will up at the same time
	void AddRelation();
	void RemoteResource();
	//void 
}
