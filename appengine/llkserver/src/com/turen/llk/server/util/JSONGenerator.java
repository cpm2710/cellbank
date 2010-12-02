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
			
			sb.append("{"+cj.getId()+",");
			sb.append(cj.getUsername()+",");
			sb.append(cj.getEmail()+",");
			sb.append(cj.getSeconds()+"},");
		}
		ChengJi cj=chengJis.get(chengJis.size()-1);
		
		sb.append("{"+cj.getId()+",");
		sb.append(cj.getUsername()+",");
		sb.append(cj.getEmail()+",");
		sb.append(cj.getSeconds()+"}]");
		return sb.toString();
	}
}
