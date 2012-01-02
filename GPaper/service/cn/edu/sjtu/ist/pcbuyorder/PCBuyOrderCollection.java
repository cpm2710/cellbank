package cn.edu.sjtu.ist.pcbuyorder;

import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElementWrapper;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
@XmlType(name="PCBuyOrderCollection", namespace="http://company.ist.sjtu.edu.cn")
@XmlRootElement(name = "PCBuyOrderCollection",namespace="http://company.ist.sjtu.edu.cn")
public class PCBuyOrderCollection {
	private List<PCBuyOrder> users;
	public void setCustomers(List<PCBuyOrder> users) {
		this.users = users;
	}
	public PCBuyOrderCollection(){
		users=new ArrayList<PCBuyOrder>();
		PCBuyOrder o=new PCBuyOrder();
		o.setId(1);
		o.setName("andy");
		users.add(o);
		o=new PCBuyOrder();
		o.setId(222);
		o.setName("andy2dd");
		users.add(o);
	}	
	
	@XmlElementWrapper(name="pcbuyorders")
    public List<PCBuyOrder> getUsers() {
		return users;
	}
	public void setUsers(List<PCBuyOrder> users) {
		this.users = users;
	}
}
