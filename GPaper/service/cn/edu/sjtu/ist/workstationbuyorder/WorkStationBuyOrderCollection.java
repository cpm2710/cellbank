package cn.edu.sjtu.ist.workstationbuyorder;

import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElementWrapper;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
@XmlType(name="WorkStationBuyOrderCollection", namespace="http://company.ist.sjtu.edu.cn")
@XmlRootElement(name = "WorkStationBuyOrderCollection",namespace="http://company.ist.sjtu.edu.cn")
public class WorkStationBuyOrderCollection {
	private List<WorkStationBuyOrder> users;
	public void setCustomers(List<WorkStationBuyOrder> users) {
		this.users = users;
	}
	public WorkStationBuyOrderCollection(){
		users=new ArrayList<WorkStationBuyOrder>();
		WorkStationBuyOrder o=new WorkStationBuyOrder();
		o.setId(1);
		o.setName("andy");
		users.add(o);
		o=new WorkStationBuyOrder();
		o.setId(222);
		o.setName("andy2dd");
		users.add(o);
	}	
	
	@XmlElementWrapper(name="workstationbuyorders")
    public List<WorkStationBuyOrder> getUsers() {
		return users;
	}
	public void setUsers(List<WorkStationBuyOrder> users) {
		this.users = users;
	}
}
