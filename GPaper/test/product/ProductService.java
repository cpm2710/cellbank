/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements. See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership. The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
package product;

import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;

@Path("/customers")
@Consumes("application/xml")
@Produces("application/xml")
public class ProductService {
    long currentId = 123;
    //Map<Long, Customer> customers = new HashMap<Long, Customer>();
    ProductCollection cc=new ProductCollection();
    public ProductService() {
        init();
    }

    /*@GET
    public CustomerCollection getCustomers(){
    	
    	return cc;
    }*/
    
    @GET
    public ProductCollection findCustomer(@QueryParam("id") String id ){
    	return cc;
    }
    @GET
    @Path("/{id}/")
    public ProductCollection getCustomer(@PathParam("id") String id) {
    	id = id.replaceAll("\\{", "");
    	id = id.replaceAll("\\}", "");
        System.out.println("----invoking getCustomer, Customer id is: " + id);
        ProductCollection oc = new ProductCollection();
		oc.getOrders().clear();
		for(Product o : cc.getOrders()){
			if(String.valueOf(o.getId()).equals(id))
				oc.getOrders().add(o);    					
		}
		return oc;
    }

    @PUT  
    public Product updateCustomer(Product customer) {
        System.out.println("----invoking updateCustomer, "+customer.getId()+"Customer name is: " + customer.getName());
        
        for(Product c: cc.getOrders()){
        	if(c.getId()==customer.getId()){
        		c.setName(customer.getName());
        		c.setQuantity(customer.getQuantity());
        		c.setPrice(customer.getPrice());
        		return c;
        	}
        }
        return customer;
    }

    @POST
    public Product addCustomer(Product customer) {
        System.out.println("----invoking addCustomer, Customer name is: " + customer.getName());
        //customer.setId(++currentId);
        cc.getOrders().add(customer);
        //customers.put(customer.getId(), customer);

        return customer;//Response.ok(customer).build();
    }

    @DELETE
    @Path("/{id}/")
    public Product deleteCustomer(@PathParam("id") String id) {
    	id = id.replaceAll("\\{", "");
    	id = id.replaceAll("\\}", "");
        System.out.println("----invoking deleteCustomer, Customer id is: " + id);
        long idNumber = Long.parseLong(id);
        for(Product c: cc.getOrders()){
        	if(c.getId()==idNumber){
        		cc.getOrders().remove(c);
        		return c;
        	}
        }
        return null;
    }

    
    final void init() {
        
        
    }

}
