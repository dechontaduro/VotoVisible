package com.google.zxing.client.android;

import android.app.Activity;
import android.os.Bundle;
import android.view.Window;
import android.webkit.WebView;
import android.widget.Toast;

/**
 * Created by Cristian Cantillo on 18/11/13.
 */
public class activity_votaciones extends Activity {
    public void onCreate(Bundle savedInstanceState) {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_votacion);

        WebView web = (WebView)findViewById(R.id.webVotaciones);
        try{
            web.loadUrl("http://www.google.com.co");
        }catch (Exception e){
            Toast.makeText(this, "No Se pudo Conectar con Twitter", Toast.LENGTH_LONG).show();
            finish();
        }
    }
}