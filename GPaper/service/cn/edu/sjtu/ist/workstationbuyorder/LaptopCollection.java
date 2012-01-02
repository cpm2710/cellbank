package cn.edu.sjtu.ist.workstationbuyorder;

import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElementWrapper;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
@XmlType(name="LaptopCollection", namespace="http://laptop.ist.sjtu.edu.cn")
@XmlRootElement(name = "LaptopCollection",namespace="http://laptop.ist.sjtu.edu.cn")
public class LaptopCollection {
	private List<Laptop> users;
	public void setCustomers(List<Laptop> users) {
		this.users = users;
	}
	public LaptopCollection(){
		users=new ArrayList<Laptop>();
		Laptop o=new Laptop();
		o.setId(1);
		o.setName("andy");
		users.add(o);
		o=new Laptop();
		o.setId(222);
		o.setName("andy2dd");
		users.add(o);
	}	
	
	@XmlElementWrapper(name="laptops")
    public List<Laptop> getUsers() {
		return users;
	}
	public void setUsers(List<Laptop> users) {
		this.users = users;
	}
}
