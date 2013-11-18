package com.google.zxing.client.android;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.Window;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Toast;

import com.votovisible.twitter.Constantes;

public class webTwitter extends Activity {
    private Intent mIntent;

    public void onCreate(Bundle savedInstanceState) {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_webtwitter);
        mIntent = getIntent();
        String url = (String) mIntent.getExtras().get("URL");
        WebView webView = (WebView) findViewById(R.id.webview);
        webView.setWebViewClient(new WebViewClient() {
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                if (url.contains(Constantes.CALLBACK_URL)) {
                    Uri uri = Uri.parse(url);
                    String oauthVerifier = uri.getQueryParameter("oauth_verifier");
                    mIntent.putExtra("oauth_verifier", oauthVerifier);
                    setResult(2, mIntent);
                    finish();
                    return true;
                }
                return false;
            }
        });
        try{
            webView.loadUrl(url);
        }catch (Exception e){
            Toast.makeText(this, "No Se pudo Conectar con Twitter", Toast.LENGTH_LONG).show();
            finish();
        }
    }
}
