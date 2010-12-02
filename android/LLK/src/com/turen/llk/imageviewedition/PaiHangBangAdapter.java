package com.turen.llk.imageviewedition;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.turen.llk.Main;
import com.turen.llk.R;
import com.turen.llk.domain.ChengJi;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class PaiHangBangAdapter extends BaseAdapter {
	/*ArrayList<ChengJi> chengJis;
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
	}*/
	private ArrayList<ChengJi> chengJis;

    private Context context;

    public PaiHangBangAdapter(Context context, ArrayList<ChengJi> chengJis) {
        this.context = context;
        this.chengJis = chengJis;
    }

    @Override
    public int getCount() {
        return this.chengJis.size();
    }

    @Override
    public Object getItem(int position) {
        return this.chengJis.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        ViewHolder viewHolder = null;
        if (convertView == null) {
            convertView = LayoutInflater.from(context).inflate(R.layout.friendllk, null);
            viewHolder = new ViewHolder();
            viewHolder.image = (ImageView) convertView.findViewById(R.id.head);
            viewHolder.text = (TextView) convertView.findViewById(R.id.name);
            convertView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }
        ChengJi friend = this.chengJis.get(position);
        viewHolder.text.setText(friend.getUsername());
        this.setViewImage(viewHolder.image, friend.getHeaderUrl());
        return convertView;
    }

    private Map<String, Bitmap> bmps = new HashMap<String, Bitmap>();

    private void setViewImage(ImageView v, String headurl) {
        Bitmap bmp = bmps.get(headurl);
        if (bmp != null) {
            v.setImageBitmap(bmp);
            return;
        }
        try {
            URL url = new URL(headurl);
            bmp = BitmapFactory.decodeStream(url.openStream());
            v.setImageBitmap(bmp);
            bmps.put(headurl, bmp);
        } catch (IOException e) {
            e.printStackTrace();
            setViewImage(v, headurl);
        }
    }

    static class ViewHolder {

        ImageView image;

        TextView text;
    }
}
