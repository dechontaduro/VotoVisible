package com.google.zxing.client.android;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.votovisible.twitter.Constantes;
import com.votovisible.twitter.Preferences;
import com.votovisible.twitter.TwetManager;

/**
 * Created by Cristian Cantillo on 14/11/13.
 */
public class MenuActivity extends Activity {

    TwetManager twet = null;
    private int AUT_TWITTER = 2;
    Context c = null;

    public void onCreate(Bundle savedInstanceState) {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.menu);

        c = this;
        Preferences settings = new Preferences(Constantes.PREFERENCE_NAME,this);
        twet = new TwetManager(settings);

        Button btnVotar = (Button)findViewById(R.id.btnVotar);
        btnVotar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(twet.existenKeys()){
                    twet.setTokens();
                }else{
                    Intent i = new Intent(MenuActivity.this, webTwitter.class);
                    i.putExtra("URL",twet.getUrlAutenticacion());
                    startActivityForResult(i, AUT_TWITTER);
                }
            }
        });

        Button btnEnvTweet = (Button)findViewById(R.id.btnEnviarTw);
        btnEnvTweet.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                TextView txt = (TextView)findViewById(R.id.txtQr);
                String tweeter = txt.getText().toString();

                if(twet.enviarTwet(tweeter)){
                    Toast.makeText(c, "Tweet Enviado Correctamente", Toast.LENGTH_LONG).show();
                }else{
                    Toast.makeText(c,"No Se Pudo Enviar el Tweet", Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == AUT_TWITTER) {
            String oauthVerifier = (String) data.getExtras().get("oauth_verifier");
            twet.setTokens(oauthVerifier);
            lanzarActivityEscaner();
        }

        if (resultCode == RESULT_OK) {
            String resultado = data.getStringExtra("SCAN_RESULT");
            TextView txt = (TextView)findViewById(R.id.txtQr);
            txt.setText(resultado);
        }
    }

    public void lanzarActivityEscaner(){
        Intent i = new Intent(MenuActivity.this, com.google.zxing.client.android.CaptureActivity.class);
        startActivityForResult(i, 0);
    }
}