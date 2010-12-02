package com.turen.llk.imageviewedition;

import java.util.ArrayList;

import android.app.ProgressDialog;
import android.app.AlertDialog.Builder;
import android.widget.ListAdapter;
import android.widget.ListView;

import com.turen.llk.Main;
import com.turen.llk.domain.ChengJi;

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
	public void paiHangBangOnComplete(ArrayList<ChengJi> chengJis){
		
		ListAdapter adapter = new PaiHangBangAdapter(this.main, chengJis);
        ListView lv = new ListView(this.main);
        
        lv.setAdapter(adapter);

        
        this.showResult(lv);
        this.progress.dismiss();
    }

    private void showResult(final ListView lv) {
        this.main.runOnUiThread(new Runnable() {

            @Override
            public void run() {
                Builder alertBuilder = new Builder(main);
                alertBuilder.setTitle("排行榜");
                alertBuilder.setView(lv);
                alertBuilder.setNeutralButton("确定", null);
                alertBuilder.create().show();
            }
        });
    }
}
