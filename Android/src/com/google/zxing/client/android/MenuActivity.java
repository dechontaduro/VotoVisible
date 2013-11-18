package com.google.zxing.client.android;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.votovisible.twitter.Constantes;
import com.votovisible.twitter.DatosGlobales;
import com.votovisible.twitter.Preferences;
import com.votovisible.twitter.TestConexion;
import com.votovisible.twitter.TwetManager;

/**
 * Created by Cristian Cantillo on 14/11/13.
 */
public class MenuActivity extends Activity {

    TwetManager twet = null;
    private int AUT_TWITTER = 2;
    Context c = null;
    TestConexion con = null;

    public void onCreate(Bundle savedInstanceState) {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.menu);
        con = new TestConexion();

        c = this;

        if(!con.tieneConectividad(c)){
            Toast.makeText(c, "No Tiene Conexion a Internet", Toast.LENGTH_LONG).show();
            return;
        }

        Button btnVotar = (Button)findViewById(R.id.btnVotar);
        btnVotar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!con.tieneConectividad(c)){
                    Toast.makeText(c, "No Tiene Conexion a Internet", Toast.LENGTH_LONG).show();
                    return;
                }

                Preferences settings = new Preferences(Constantes.PREFERENCE_NAME,c);
                twet = new TwetManager(settings);

                if(twet.existenKeys()){
                    twet.setTokens();
                    DatosGlobales.getInstance().setTwitter(twet);
                    lanzarActivityEscaner();
                }else{
                    Intent i = new Intent(MenuActivity.this, webTwitter.class);
                    i.putExtra("URL",twet.getUrlAutenticacion());
                    startActivityForResult(i, AUT_TWITTER);
                }
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == AUT_TWITTER) {
            String oauthVerifier = (String) data.getExtras().get("oauth_verifier");
            twet.setTokens(oauthVerifier);
            DatosGlobales.getInstance().setTwitter(twet);
            lanzarActivityEscaner();
        }
    }

    public void lanzarActivityEscaner(){
        Intent i = new Intent(MenuActivity.this, EscanerQr.class);
        startActivity(i);
    }
}