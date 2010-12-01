package com.turen.llk.imageviewedition;

import java.util.ArrayList;

import com.turen.llk.Main;

import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

public class PaiHangBangAdapter extends BaseAdapter {
	ArrayList<String> names;
	Main main;
	public PaiHangBangAdapter(Main main,ArrayList<String> names){
		this.names=names;
		this.main=main;
	}
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return names.size();
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
			view.setText(names.get(position));
		}else{
			view = (TextView) convertView;
		}		
		return view;
	}

}
