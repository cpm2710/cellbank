package product;

import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElementWrapper;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;
@XmlType(name="ProductCollection", namespace="http://www.ist.sjtu.edu.cn")
@XmlRootElement(name = "ProductCollection",namespace="http://www.ist.sjtu.edu.cn")
public class ProductCollection {
	private List<Product> orders;
	public void setCustomers(List<Product> orders) {
		this.orders = orders;
	}
	public ProductCollection(){
		orders=new ArrayList<Product>();
		Product o=new Product();
		o.setId(1);
		o.setName("andy");
		o.setQuantity(100);
		o.setPrice(85.78);
		orders.add(o);
		o=new Product();
		o.setId(2);
		o.setName("andy2dd");
		o.setQuantity(50);
		o.setPrice(56.78);
		orders.add(o);
		o=new Product();
		o.setId(3);
		o.setName("andy3dd");
		o.setQuantity(120);
		o.setPrice(98.33);
		orders.add(o);
		o=new Product();
		o.setId(4);
		o.setName("andy4dd");
		o.setQuantity(20);
		o.setPrice(45.33);
		orders.add(o);
		o=new Product();
		o.setId(5);
		o.setName("andy5dd");
		o.setQuantity(110);
		o.setPrice(12.34);
		orders.add(o);
		o=new Product();
		o.setId(6);
		o.setName("andy6dd");
		o.setQuantity(90);
		o.setPrice(87.65);
		orders.add(o);
		o=new Product();
		o.setId(7);
		o.setName("andy7dd");
		o.setQuantity(180);
		o.setPrice(43.98);
		orders.add(o);
		o=new Product();
		o.setId(8);
		o.setName("andy8dd");
		o.setQuantity(180);
		o.setPrice(43.98);
		orders.add(o);
		o=new Product();
		o.setId(9);
		o.setName("andy9dd");
		o.setQuantity(180);
		o.setPrice(43.98);
		orders.add(o);
		o=new Product();
		o.setId(10);
		o.setName("andy10dd");
		o.setQuantity(180);
		o.setPrice(43.98);
		orders.add(o);
		o=new Product();
		o.setId(11);
		o.setName("andy11dd");
		o.setQuantity(180);
		o.setPrice(43.98);
		orders.add(o);
		o=new Product();
		o.setId(12);
		o.setName("andy12dd");
		o.setQuantity(180);
		o.setPrice(43.98);
		orders.add(o);
		o=new Product();
		o.setId(13);
		o.setName("andy13dd");
		o.setQuantity(180);
		o.setPrice(43.98);
		orders.add(o);
		o=new Product();
		o.setId(14);
		o.setName("andy14dd");
		o.setQuantity(180);
		o.setPrice(43.98);
		orders.add(o);
	}	
	
	@XmlElementWrapper(name="orders")
    public List<Product> getOrders() {
		return orders;
	}
	public void setOrders(List<Product> orders) {
		this.orders = orders;
	}
}
