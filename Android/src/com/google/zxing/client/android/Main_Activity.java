package com.google.zxing.client.android;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;

/**
 * Created by Cristian Cantillo on 12/11/13.
 */
public class Main_Activity extends Activity {

    public void onCreate(Bundle savedInstanceState) {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.splash);

        Button btnIngresar = (Button)findViewById(R.id.btnIngresar);
        btnIngresar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent i = new Intent(Main_Activity.this, MenuActivity.class);
                startActivity(i);
            }
        });
    }
}