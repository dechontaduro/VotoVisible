package com.google.zxing.client.android;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;

/**
 * Created by Cristian Cantillo on 14/11/13.
 */
public class EscanerQr extends Activity {
    public void onCreate(Bundle savedInstanceState) {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_qr);

        Button btnEscaner = (Button)findViewById(R.id.btnQr);
        btnEscaner.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(EscanerQr.this, com.google.zxing.client.android.CaptureActivity.class);
                startActivityForResult(i, 0);
            }
        });
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent intent) {
        if (resultCode == RESULT_OK) {
            String resultado = intent.getStringExtra("SCAN_RESULT");
        }
    }
}