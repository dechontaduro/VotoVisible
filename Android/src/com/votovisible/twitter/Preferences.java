package com.votovisible.twitter;

import android.content.Context;
import android.content.SharedPreferences;

/**
 * Created by Cristian Cantillo on 13/11/13.
 */
public class Preferences {
    SharedPreferences settings;

    public Preferences(String Shared_pref_name, Context context) {
        // creamos referencia al shared preferences
        settings = context.getSharedPreferences(Shared_pref_name,
                Context.MODE_PRIVATE);

    }

    // Retorna T o F si existe el valor
    public boolean isExist(String val) {
        //si no existe retorna null
        String value = settings.getString(val, null);

        if (value == null) {
            return false;
        } else {
            return true;
        }
    }

    // Retorna un String con el conetenido del parametro val sacado del Shared
    // Preferences
    public String getValor(String val) {
        String valor = settings.getString(val, null);
        return valor;
    }

    //Escribimos un valor string
    public void addValor(String clave, String valor) {
        SharedPreferences.Editor editor = settings.edit();

        editor.putString(clave, valor);
        editor.commit();
    }

    //borramos un valor
    public void DelValor(String clave) {
        SharedPreferences.Editor editor = settings.edit();
        editor.remove(clave);
        editor.commit();
    }
}
