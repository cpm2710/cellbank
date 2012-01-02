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
package cn.edu.sjtu.ist.workstationbuyorder;

import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;

@Path("/laptops")
@Consumes("application/xml")
@Produces("application/xml")
public class LaptopService {
    long currentId = 123;
    //Map<Long, Customer> customers = new HashMap<Long, Customer>();
    LaptopCollection cc=new LaptopCollection();
    public LaptopService() {
        init();
    }

    /*@GET
    public CustomerCollection getCustomers(){
    	
    	return cc;
    }*/
    
    @GET
    public LaptopCollection findLaptop(@QueryParam("name") String name ){
    	System.out.println(name);
    	if(name.equals("shit")){
    		
    		LaptopCollection c=
    		 new LaptopCollection();
    		c.getUsers().clear();
    		return c;
    	}
    	return cc;
    }
    @GET
    @Path("/{id}/")
    public Laptop getLaptop(@PathParam("id") String id) {
        System.out.println("----invoking getCustomer, Customer id is: " + id);
        long idNumber = Long.parseLong(id);
        for(Laptop c: cc.getUsers()){
        	if(c.getId()==idNumber){
        		return c;
        	}
        }
        return null;
    }

    @PUT  
    public Laptop updateLaptop(Laptop customer) {
        System.out.println("----invoking updateCustomer, "+customer.getId()+"Customer name is: " + customer.getName());
        
        for(Laptop c: cc.getUsers()){
        	if(c.getId()==customer.getId()){
        		c.setName(customer.getName());
        		return c;
        	}
        }
        return customer;
    }

    @POST
    public Laptop addLaptop(Laptop customer) {
        System.out.println("----invoking addCustomer, Customer name is: " + customer.getName());
        //customer.setId(++currentId);
        cc.getUsers().add(customer);
        //customers.put(customer.getId(), customer);

        return customer;//Response.ok(customer).build();
    }

    @DELETE
    @Path("/{id}/")
    public Laptop deleteLaptop(@PathParam("id") String id) {
        System.out.println("----invoking deleteCustomer, Customer id is: " + id);
        long idNumber = Long.parseLong(id);
        for(Laptop c: cc.getUsers()){
        	if(c.getId()==idNumber){
        		cc.getUsers().remove(c);
        		return c;
        	}
        }
        return null;
    }

    
    final void init() {
        
        
    }

}
