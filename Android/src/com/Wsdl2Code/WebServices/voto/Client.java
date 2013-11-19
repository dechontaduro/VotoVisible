package com.Wsdl2Code.WebServices.voto;

import android.os.AsyncTask;
import com.votovisible.twitter.VotoTweet;

/**
 * Created by jgonzalez on 18/11/13.
 */
public class Client extends AsyncTask<Void, Void, Void> {
    public interface OnAsyncResult {
        public abstract void onResultSuccess(int resultCode, String message);
        public abstract void onResultFail(int resultCode, String message);
    }

    private VotoTweet votoTweet;
    public void setVotoTweet(VotoTweet votoTweet) { this.votoTweet = votoTweet; }


    OnAsyncResult onAsyncResult;
    public void setOnResultListener(OnAsyncResult onAsyncResult) {
        if (onAsyncResult != null) {
            this.onAsyncResult = onAsyncResult;
        }
    }

    @Override
    protected Void  doInBackground(Void... params) {
        if (onAsyncResult != null) {
            voto client = new voto();

            try
            {
                String id = client.put(
                        votoTweet.getTwitterAccount(), votoTweet.getVotacionId(),
                        "", "", "", "", "",
                        votoTweet.getTweetId(),
                        votoTweet.getTipo(), votoTweet.getDecision(), votoTweet.getComentario());

                if(id != null && !id.equals(""))
                    onAsyncResult.onResultSuccess(0, id);
                else
                    onAsyncResult.onResultFail(1, "error");

            }
            catch (Exception e)
            {
                e.printStackTrace();
                onAsyncResult.onResultFail(2, "error no definido");
            }
        }
        return null;
    }

    protected void onProgressUpdate() {
    }

    protected void onPostExecute() {
    }
}
