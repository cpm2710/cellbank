package cn.edu.sjtu.ist.laptopbuyorder;

import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElementWrapper;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
@XmlType(name="LaptopBuyOrderCollection", namespace="http://company.ist.sjtu.edu.cn")
@XmlRootElement(name = "LaptopBuyOrderCollection",namespace="http://company.ist.sjtu.edu.cn")
public class LaptopBuyOrderCollection {
	private List<LaptopBuyOrder> users;
	public void setCustomers(List<LaptopBuyOrder> users) {
		this.users = users;
	}
	public LaptopBuyOrderCollection(){
		users=new ArrayList<LaptopBuyOrder>();
		LaptopBuyOrder o=new LaptopBuyOrder();
		o.setId(1);
		o.setName("andy");
		users.add(o);
		o=new LaptopBuyOrder();
		o.setId(222);
		o.setName("andy2dd");
		users.add(o);
	}	
	
	@XmlElementWrapper(name="laptopbuyorders")
    public List<LaptopBuyOrder> getUsers() {
		return users;
	}
	public void setUsers(List<LaptopBuyOrder> users) {
		this.users = users;
	}
}
