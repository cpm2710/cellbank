package com.turen.llk.imageviewedition;

import java.util.ArrayList;

import com.turen.llk.Main;
import com.turen.llk.domain.ChengJi;

import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

public class PaiHangBangAdapter extends BaseAdapter {
	ArrayList<ChengJi> chengJis;
	Main main;
	public PaiHangBangAdapter(Main main,ArrayList<ChengJi> chengJis){
		this.chengJis=chengJis;
		this.main=main;
	}
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return chengJis.size();
	}

	@Override
	public Object getItem(int arg0) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		TextView view=null;
		if (convertView == null) {
			view=new TextView(main);
			ChengJi chengJi=chengJis.get(position);
			view.setText(chengJi.getUsername()+chengJi.getEmail());
		}else{
			view = (TextView) convertView;
		}		
		return view;
	}

}
