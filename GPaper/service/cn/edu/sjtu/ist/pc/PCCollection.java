package cn.edu.sjtu.ist.pc;

import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElementWrapper;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
@XmlType(name="PCCollection", namespace="http://company.ist.sjtu.edu.cn")
@XmlRootElement(name = "PCCollection",namespace="http://company.ist.sjtu.edu.cn")
public class PCCollection {
	private List<PC> users;
	public void setCustomers(List<PC> users) {
		this.users = users;
	}
	public PCCollection(){
		users=new ArrayList<PC>();
		PC o=new PC();
		o.setId(1);
		o.setName("andy");
		users.add(o);
		o=new PC();
		o.setId(222);
		o.setName("andy2dd");
		users.add(o);
	}	
	
	@XmlElementWrapper(name="laptops")
    public List<PC> getUsers() {
		return users;
	}
	public void setUsers(List<PC> users) {
		this.users = users;
	}
}
