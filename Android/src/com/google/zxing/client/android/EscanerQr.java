package com.google.zxing.client.android;

import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Toast;

import com.votovisible.twitter.DatosGlobales;
import com.votovisible.twitter.TwetManager;

import java.io.LineNumberReader;

/**
 * Created by Cristian Cantillo on 14/11/13.
 */
public class EscanerQr extends Activity {
    TwetManager twet = null;
    String voto = "";
    Context c = null;
    int tipo = 0;

    public void onCreate(Bundle savedInstanceState) {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_qr);

        c = this;
        twet = DatosGlobales.getInstance().getTwitter();

        Button btnEscaner = (Button)findViewById(R.id.btnQr);
        btnEscaner.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(EscanerQr.this, com.google.zxing.client.android.CaptureActivity.class);
                startActivityForResult(i, 0);
            }
        });

        Button btnPublico = (Button)findViewById(R.id.btnPublico);
        btnPublico.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                tipo = 1;
                LinearLayout lyVotar = (LinearLayout)findViewById(R.id.lyVotar);
                lyVotar.setVisibility(View.VISIBLE);
            }
        });

        Button btnPrivado = (Button)findViewById(R.id.btnPrivado);
        btnPrivado.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                tipo = 2;
                LinearLayout lyVotar = (LinearLayout)findViewById(R.id.lyVotar);
                lyVotar.setVisibility(View.VISIBLE);
            }
        });

        Button btnAbstengo = (Button)findViewById(R.id.btnAbs);
        btnAbstengo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                tipo = 3;
                LinearLayout lyVotar = (LinearLayout)findViewById(R.id.lyVotar);
                lyVotar.setVisibility(View.GONE);
            }
        });

        Button btnSi = (Button)findViewById(R.id.btnSi);
        btnSi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                voto = "Si";
            }
        });

        Button btnNo = (Button)findViewById(R.id.btnNo);
        btnSi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                voto = "No";
            }
        });

        Button btnVotar = (Button)findViewById(R.id.btnVotar);
        btnVotar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(tipo == 0){
                    Toast.makeText(c, "Debe Seleccionar Una Opcion Para Votar", Toast.LENGTH_LONG).show();
                    return;
                }else if(tipo == 3){
                    voto = "Abstengo";
                }

                if(tipo > 0 && tipo < 3 && voto.equals("")){
                    Toast.makeText(c, "Seleccione Si o No para Votar", Toast.LENGTH_LONG).show();
                    return;
                }

                EditText just = (EditText)findViewById(R.id.justi);
                if(just.getText().toString().equals("")){
                    Toast.makeText(c, "Debe Indicar una Justificacion", Toast.LENGTH_LONG).show();
                    return;
                }

                String mensaje = getMensaje();
                if(twet.enviarTwet(mensaje)){
                    Toast.makeText(c, "Tweet Enviado Correctamente", Toast.LENGTH_LONG).show();
                }else{
                    Toast.makeText(c,"No Se Pudo Enviar el Tweet", Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent intent) {
        if (resultCode == RESULT_OK) {
            String resultado = intent.getStringExtra("SCAN_RESULT");
            ponerDatosQr(resultado);
        }
    }

    public String getMensaje(){
        EditText corp = (EditText)findViewById(R.id.corp);
        EditText proy = (EditText)findViewById(R.id.proy);
        EditText num = (EditText)findViewById(R.id.num);
        EditText anio = (EditText)findViewById(R.id.anio);
        EditText url = (EditText)findViewById(R.id.url);
        EditText just = (EditText)findViewById(R.id.justi);

        String mensaje = "" + proy.getText().toString() + " #" + corp.getText().toString() + " #pl"
                + num.getText().toString() + "_" + anio.getText().toString() + " " + url.getText().toString().replace("$", "&")
                + " #" + voto + " " + just.getText().toString();

        return mensaje;
    }

    public void ponerDatosQr(String datos){
        String[] arrDatos = datos.split("¬");
        try{
            EditText corp = (EditText)findViewById(R.id.corp);
            EditText proy = (EditText)findViewById(R.id.proy);
            EditText num = (EditText)findViewById(R.id.num);
            EditText anio = (EditText)findViewById(R.id.anio);
            EditText url = (EditText)findViewById(R.id.url);

            corp.setText(arrDatos[0]);
            proy.setText(arrDatos[1]);
            num.setText(arrDatos[2]);
            anio.setText(arrDatos[3]);
            url.setText(arrDatos[4]);
        }catch(Exception e){
            Toast.makeText(this, "Datos Incorrectos", Toast.LENGTH_LONG).show();
            e.printStackTrace();
        }
    }
}