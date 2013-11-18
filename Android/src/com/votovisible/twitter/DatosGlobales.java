package com.votovisible.twitter;

/**
 * Created by Cristian Cantillo on 16/11/13.
 */
public class DatosGlobales implements Cloneable{

    private static TwetManager twet = null;
    private static DatosGlobales instance = null;

    public DatosGlobales() {}

    private synchronized static void createInstance() {
        instance = new DatosGlobales();
    }

    public static DatosGlobales getInstance(){
        if(instance == null){
            createInstance();
        }
        return instance;
    }

    public void setTwitter(TwetManager twet){
        this.twet = twet;
    }

    public TwetManager getTwitter(){
        return this.twet;
    }
}
