package com.example.gergosipos.hikex_smarphone_app;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;

import android.os.Build;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.google.gson.Gson;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

import POJO.User;

public class LoginActivity extends AppCompatActivity  {
    private VolleySingleton volleySingleton;
    private SharedPreferences sharedPreferences;
    private Boolean isLoginInProgress = false;

    // UI references.
    private EditText mUsernameView;
    private EditText mPasswordView;
    private View mProgressView;
    private View mLoginFormView;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        volleySingleton = VolleySingleton.getInstance(getApplicationContext());
        sharedPreferences = getApplicationContext().getSharedPreferences(getString(R.string.preference_file_key), Context.MODE_PRIVATE);
        setContentView(R.layout.activity_login);
        mUsernameView = (EditText) findViewById(R.id.email);
        mPasswordView = (EditText) findViewById(R.id.password);

        Button mEmailSignInButton = (Button) findViewById(R.id.email_sign_in_button);
        mEmailSignInButton.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View view) {
                attemptLogin();
            }
        });

        mLoginFormView = findViewById(R.id.email_login_form);
        mProgressView = findViewById(R.id.login_progress);

        if (sharedPreferences.contains("user")){
            final User user = new Gson().fromJson(sharedPreferences.getString("user", ""), User.class);
            StringRequest request = new StringRequest(Request.Method.GET,getString(R.string.api_url)+"Users/test",
             new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    Intent myIntent = new Intent(getBaseContext(),TodayHikesActivity.class);
                    startActivity(myIntent);
                    finish();
                }
            },

                    new Response.ErrorListener() {
                        @Override
                        public void onErrorResponse(VolleyError error) {
                            if (error.networkResponse.statusCode == 401) {
                                Toast.makeText(getApplicationContext(),"Lejárt a bejelentkezésed",Toast.LENGTH_LONG).show();
                                SharedPreferences.Editor edit = sharedPreferences.edit();
                                edit.remove("user");
                                edit.apply();
                            }

                        }}){
                @Override
                public Map<String, String> getHeaders() throws AuthFailureError {
                    Map<String, String> params = new HashMap<String, String>();
                    params.put("Authorization", "Bearer "+user.getToken());
                    return params;
                }
            };

            volleySingleton.getRequestQueue().add(request);
            volleySingleton.getRequestQueue().start();
        }
    }



    private void attemptLogin() {
        if (isLoginInProgress) {
            return;
        }

        // Reset errors.
        mUsernameView.setError(null);
        mPasswordView.setError(null);

        // Store values at the time of the login attempt.
        String userName = mUsernameView.getText().toString();
        String password = mPasswordView.getText().toString();

        boolean cancel = false;
        View focusView = null;

        if (TextUtils.isEmpty(password)) {
            mPasswordView.setError(getString(R.string.error_field_required));
            focusView = mPasswordView;
            cancel = true;
        }

        if (TextUtils.isEmpty(userName)) {
            mUsernameView.setError(getString(R.string.error_field_required));
            focusView = mUsernameView;
            cancel = true;
        }

        if (cancel) {
            focusView.requestFocus();
        } else {
            showProgress(true);
            isLoginInProgress = true;
            sendRequest(userName, password);
        }
    }

    private void sendRequest(String userName, String password) {

        String url = getString(R.string.api_url)+"Users/authenticate";
        JSONObject jsonBody = null;
        try {
            jsonBody = new JSONObject()
                    .put("userName",userName)
                    .put("password",password);
        } catch (JSONException e) {
            e.printStackTrace();
        }

        JsonObjectRequest request = new JsonObjectRequest(Request.Method.POST, url, jsonBody,
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        String json = response.toString();
                        User u = new Gson().fromJson(json,User.class);
                        Toast.makeText(getApplicationContext(),"Üdv, "+u.getUserNam(),Toast.LENGTH_LONG).show();
                        SharedPreferences.Editor editor = sharedPreferences.edit().putString("user",json);
                        editor.apply();
                        isLoginInProgress = false;
                        showProgress(false);
                        Intent myIntent = new Intent(getBaseContext(),TodayHikesActivity.class);
                        startActivity(myIntent);
                        finish();
                    }
                },

                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        String errorString = new String(error.networkResponse.data);
                        Toast.makeText(getApplicationContext(),errorString,Toast.LENGTH_LONG).show();
                        isLoginInProgress = false;
                        showProgress(false);
                    }

                });
        volleySingleton.getRequestQueue().add(request);
        volleySingleton.getRequestQueue().start();
    }

    /**
     * Shows the progress UI and hides the login form.
     */
    @TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
    private void showProgress(final boolean show) {
            int shortAnimTime = getResources().getInteger(android.R.integer.config_shortAnimTime);

            mLoginFormView.setVisibility(show ? View.GONE : View.VISIBLE);
            mLoginFormView.animate().setDuration(shortAnimTime).alpha(
                    show ? 0 : 1).setListener(new AnimatorListenerAdapter() {
                @Override
                public void onAnimationEnd(Animator animation) {
                    mLoginFormView.setVisibility(show ? View.GONE : View.VISIBLE);
                }
            });

            mProgressView.setVisibility(show ? View.VISIBLE : View.GONE);
            mProgressView.animate().setDuration(shortAnimTime).alpha(
                    show ? 1 : 0).setListener(new AnimatorListenerAdapter() {
                @Override
                public void onAnimationEnd(Animator animation) {
                    mProgressView.setVisibility(show ? View.VISIBLE : View.GONE);
                }
            });
    }
}

