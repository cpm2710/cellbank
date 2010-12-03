package com.turen.llk.server.util;

import java.util.List;

import com.turen.llk.server.domain.ChengJi;

public class JSONGenerator {
	public static String generate(List<ChengJi> chengJis){
		if(chengJis.size()==0){
			return "[]";
		}
		StringBuilder sb=new StringBuilder();
		sb.append("[");
		for(int i=0;i<chengJis.size()-1;i++){
			ChengJi cj=chengJis.get(i);
			
			sb.append("{id:'"+cj.getId()+"',");
			sb.append("userName:'"+cj.getUsername()+"',");
			sb.append("email:'"+cj.getEmail()+"',");
			sb.append("xiaoNeiId:'"+cj.getXiaoNeiId()+"',");
			sb.append("headUrl:'"+cj.getHeadUrl()+"',");
			sb.append("level:'"+cj.getLevel()+"',");
			sb.append("miniSeconds:'"+cj.getMiniSeconds()+"'},");
		}
		ChengJi cj=chengJis.get(chengJis.size()-1);
		
		sb.append("{id:'"+cj.getId()+"',");
		sb.append("userName:'"+cj.getUsername()+"',");
		sb.append("email:'"+cj.getEmail()+"',");
		sb.append("xiaoNeiId:'"+cj.getXiaoNeiId()+"',");
		sb.append("headUrl:'"+cj.getHeadUrl()+"',");
		sb.append("level:'"+cj.getLevel()+"',");
		sb.append("miniSeconds:'"+cj.getMiniSeconds()+"'}]");
		return sb.toString();
	}
}
