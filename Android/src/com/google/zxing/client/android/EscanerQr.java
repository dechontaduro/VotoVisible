package com.google.zxing.client.android;

import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.RadioGroup;
import android.widget.Toast;

import com.Wsdl2Code.WebServices.voto.Client;
import com.votovisible.twitter.DatosGlobales;
import com.votovisible.twitter.TwetManager;
import com.votovisible.twitter.VotoTweet;

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

        LinearLayout lyForm = (LinearLayout)findViewById(R.id.lyForm);
        lyForm.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                hideSoftKeyboard();
            }
        });

        Button btnEscaner = (Button)findViewById(R.id.btnQr);
        btnEscaner.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(EscanerQr.this, com.google.zxing.client.android.CaptureActivity.class);
                i.putExtra("Escanear", "Si");
                startActivityForResult(i, 0);
            }
        });

        RadioGroup opciones = (RadioGroup)findViewById(R.id.opciones);
        opciones.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup radioGroup, int checkId) {
                if(checkId == R.id.opc_publico){
                    tipo = 1;
                }else{
                    tipo = 2;
                }
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

        Button btnAbstengo = (Button)findViewById(R.id.btnAbstengo);
        btnAbstengo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                voto ="Abstengo";
            }
        });

        Button btnVotar = (Button)findViewById(R.id.btnVotar);
        btnVotar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(tipo == 0){
                    Toast.makeText(c, "Debe Seleccionar Una Opcion Para Votar", Toast.LENGTH_LONG).show();
                    return;
                }

                if(voto.equals("")){
                    Toast.makeText(c, "Seleccione Si, No o Abstengo para Votar", Toast.LENGTH_LONG).show();
                    return;
                }

                EditText just = (EditText)findViewById(R.id.justi);
                if(just.getText().toString().equals("")){
                    Toast.makeText(c, "Debe Indicar una Justificacion", Toast.LENGTH_LONG).show();
                    return;
                }

                String mensaje = getMensaje();

                sendVotoWS();

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
        String votacion = (tipo == 1)?voto:"Privado";

        String mensaje =
                this.getString(R.string.tweet
                    , corp.getText().toString()
                    , proy.getText().toString()
                    , num.getText().toString()
                    , anio.getText().toString()
                    , url.getText().toString().replace("$", "&")
                    , votacion
                    ,just.getText().toString()
                    );

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


    private void hideSoftKeyboard() {
        InputMethodManager inputManager = (InputMethodManager) getBaseContext()
                .getSystemService(Context.INPUT_METHOD_SERVICE);
        if (inputManager != null)
            inputManager.hideSoftInputFromWindow(getWindow().getDecorView().getWindowToken(), 0);
    }

    private void sendVotoWS() {
        String justificacion = ((EditText)findViewById(R.id.justi)).getText().toString();

        VotoTweet votoTweet = new VotoTweet("", "10", "dechontaduro", "0",  voto, justificacion, "123456", "", "", "", "", "");

        Client client = new Client();
        client.setVotoTweet(votoTweet);
        client.setOnResultListener(asynResult);
        client.execute();
    }

    Client.OnAsyncResult asynResult = new Client.OnAsyncResult() {
        @Override
        public void onResultSuccess(final int resultCode, final String message) {
            runOnUiThread(new Runnable() {
                public void run() {
                    Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                }
            });
        }

        @Override
        public void onResultFail(final int resultCode, final String message) {
            runOnUiThread(new Runnable() {
                public void run() {
                    Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
                }
            });
        }
    };
}