package cn.edu.sjtu.ist.workstation;

import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElementWrapper;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
@XmlType(name="WorkStationCollection", namespace="http://WorkStation")
@XmlRootElement(name = "WorkStationCollection",namespace="http://WorkStation")
public class WorkStationCollection {
	private List<WorkStation> users;
	public void setCustomers(List<WorkStation> users) {
		this.users = users;
	}
	public WorkStationCollection(){
		users=new ArrayList<WorkStation>();
		WorkStation o=new WorkStation();
		o.setId(1);
		o.setName("andy");
		users.add(o);
		o=new WorkStation();
		o.setId(222);
		o.setName("andy2dd");
		users.add(o);
	}	
	
	@XmlElementWrapper(name="laptops")
    public List<WorkStation> getUsers() {
		return users;
	}
	public void setUsers(List<WorkStation> users) {
		this.users = users;
	}
}
