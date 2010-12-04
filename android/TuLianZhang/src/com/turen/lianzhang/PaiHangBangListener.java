package com.turen.lianzhang;

import java.util.ArrayList;

import android.app.ProgressDialog;
import android.app.AlertDialog.Builder;
import android.widget.ListAdapter;
import android.widget.ListView;

import com.turen.lianzhang.Main;
import com.turen.lianzhang.domain.ChengJi;

public class PaiHangBangListener {
	private Main main;
	
	private ProgressDialog progress;
	public PaiHangBangListener(Main main){
		this.main=main;
	}
	public void showProgress(Main context,String title,String text){
		progress = ProgressDialog.show(context, title, text);
		progress.show();
	}
	public void paiHangBangOnComplete(int level,ArrayList<ChengJi> chengJis){
		
		ListAdapter adapter = new PaiHangBangAdapter(this.main, chengJis);
        ListView lv = new ListView(this.main);
        
        lv.setAdapter(adapter);        
        this.showResult(lv,level);
        this.progress.dismiss();
    }

    private void showResult(final ListView lv,final int level) {
        this.main.runOnUiThread(new Runnable() {

            @Override
            public void run() {
                Builder alertBuilder = new Builder(main);
                switch(level){
                case 1:
                	alertBuilder.setTitle("练习排行榜");
                	break;
                case 2:
                	alertBuilder.setTitle("初等新手排行榜");
                	break;
                case 3:
                	alertBuilder.setTitle("新手排行榜");
                	break;
                case 4:
                	alertBuilder.setTitle("老手排行榜");
                	break;
                case 5:
                	alertBuilder.setTitle("高手排行榜");
                	break;
                case 6:
                	alertBuilder.setTitle("高高手排行榜");
                	break;
                	default:
                		alertBuilder.setTitle("排行榜");
                		
                }
                //alertBuilder.setTitle("排行榜");
                alertBuilder.setView(lv);
                alertBuilder.setNeutralButton("确定", null);
                alertBuilder.create().show();
            }
        });
    }
}
