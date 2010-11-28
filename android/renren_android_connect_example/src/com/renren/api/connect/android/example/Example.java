package com.renren.api.connect.android.example;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.RadioGroup.OnCheckedChangeListener;

import com.renren.api.connect.android.AsyncRenren;
import com.renren.api.connect.android.Renren;
import com.renren.api.connect.android.Util;
import com.renren.api.connect.android.bean.FeedParam;
import com.renren.api.connect.android.view.ConnectButton;

public class Example extends Activity implements OnCheckedChangeListener {

    public static final String USER_FULL_FIELDS = "name,email_hash, sex,star,birthday,tinyurl,headurl,mainurl,hometown_location,hs_history,university_history,work_history,contact_info";

    public static final String USER_COMMON_FIELDS = "name,email_hash,sex,star,birthday,tinyurl,headurl,mainurl";

    /** Called when the activity is first created. */
    TextView display;

    ConnectButton login;

    RadioGroup dataFormatGroup;

    String dataFormat = "json";

    private String apiKey = null;//your apiKey

    private String apiSecret = null;//your apiSecret

    Renren renren;

    AsyncRenren asyncRenren;

    SimpleRequestListener simpleRequestListener;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if (apiKey == null || apiSecret == null) {
            Util.showAlert(this, "警告", "人人应用的apiKey和apiSecret必须提供！");
        }

        setContentView(R.layout.main);

        this.renren = new Renren(this, apiKey, apiSecret);
        this.asyncRenren = new AsyncRenren(renren);

        this.display = (TextView) findViewById(R.id.display);

        this.login = (ConnectButton) findViewById(R.id.login);

        this.dataFormatGroup = (RadioGroup) findViewById(R.id.dataFormatGroup);
        this.dataFormatGroup.check(R.id.JSON);
        this.dataFormatGroup.setOnCheckedChangeListener(this);

        this.login.init(renren);
        this.login.setConnectButtonListener(new TestConnectButtonListener(this));

        this.simpleRequestListener = new SimpleRequestListener(this);

    }

    public void onClick(View v) {
        Log.i("tag", "view.id:" + v.getId());

        if (v.getId() == R.id.display) {
            String text = "sessionKey:" + renren.getSessionKey();
            this.display.setText(text);
        } else if (v.getId() == R.id.getUser) {
            Bundle params = new Bundle();
            params.putString("method", "users.getInfo");
            String uids = 700034618 + "," + 226009681 + "," + 252090984;
            params.putString("uids", uids);
            String fields = USER_FULL_FIELDS;
            params.putString("fields", fields);
            simpleRequestListener.showProgress("获取用户信息");
            this.asyncRenren.request(params, simpleRequestListener, dataFormat);
        } else if (v.getId() == R.id.postFeed) {
            FeedParam feedParam = new FeedParam();
            feedParam.setTemplateId(1);
            feedParam.setUserMessage("renren connect test user message!");
            feedParam.setUserMessagePrompt("user message prompt");
            feedParam.setBodyGeneral("[By Android Connect]");
            feedParam.setTemplateData(this.getTemplateData());
            this.renren.feed(feedParam, new PostFeedListener(this));
        } else if (v.getId() == R.id.getFriend) {
            Bundle params = new Bundle();
            params.putString("method", "friends.getFriends");
            params.putString("page", "1");
            params.putString("count", "10");
            //simpleRequestListener.showProgress("获取好友");
            //this.asyncRenren.request(params, simpleRequestListener, dataFormat);
            FriendRequestListener listener = new FriendRequestListener(this, dataFormat);
            this.asyncRenren.request(params, listener, dataFormat);
        }
    }

    @Override
    public void onCheckedChanged(RadioGroup group, int checkedId) {
        RadioButton rb = (RadioButton) findViewById(group.getCheckedRadioButtonId());
        this.dataFormat = rb.getText().toString();
        Toast.makeText(this, "Server return " + this.dataFormat + " string", Toast.LENGTH_SHORT)
                .show();
    }

    private String getTemplateData() {
        JSONObject obj = new JSONObject();
        try {
            obj.put("feedtype", "android connect");
            obj.put("content", "android connect content");
            obj.put("android", "milestone");
            JSONArray images = new JSONArray();
            JSONObject img = new JSONObject();
            img.put("src", "http://www.android.com/images/froyo.png");
            img.put("href", "http://www.android.com/");
            images.put(img);
            obj.put("images", images);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return obj.toString();
    }
}
