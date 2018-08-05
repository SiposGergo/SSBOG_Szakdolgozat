package com.example.gergosipos.hikex_smarphone_app;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.AppCompatEditText;
import android.support.v7.widget.AppCompatSpinner;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.google.gson.Gson;
import com.google.gson.JsonObject;

import org.joda.time.DateTime;
import org.joda.time.DateTimeZone;
import org.json.JSONException;
import org.json.JSONObject;

import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import POJO.CheckPoint;
import POJO.Hike;
import POJO.HikeCourse;
import POJO.User;

public class AdminActivity extends AppCompatActivity {

    AppCompatSpinner courseSpinner;
    AppCompatSpinner checkPointSpinner;
    AppCompatEditText startNumberText;
    AppCompatButton sendButton;

    VolleySingleton volleySingleton;
    User user;
    private SharedPreferences sharedPreferences;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_admin);

        sharedPreferences = getApplicationContext().getSharedPreferences(getString(R.string.preference_file_key), Context.MODE_PRIVATE);
        user = new Gson().fromJson(sharedPreferences.getString("user", ""), User.class);

        volleySingleton = VolleySingleton.getInstance(getApplicationContext());
        courseSpinner = findViewById(R.id.courseSpinner);
        checkPointSpinner = findViewById(R.id.checkPointSpinner);
        sendButton = findViewById(R.id.sendButton);
        startNumberText = findViewById(R.id.startNumEditText);

        final Hike hike = (Hike)getIntent().getSerializableExtra("hike");

        List<String> courseNames = new ArrayList<>();
        for(HikeCourse course : hike.getCourses()) {
            courseNames.add(course.getName());
        }
        ArrayAdapter<String> courseAdapter = new ArrayAdapter<String>(this,android.R.layout.simple_spinner_item,courseNames);
        courseAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        courseSpinner.setAdapter(courseAdapter);

        List<String> checkpointNames = new ArrayList<String>();
        for (CheckPoint cp : hike.getCourses().get(0).getCheckPoints()) {
            checkpointNames.add(cp.getName());
        }
        final ArrayAdapter<String> checkpointAdapter = new ArrayAdapter<String>(this,android.R.layout.simple_spinner_item,checkpointNames);
        checkpointAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        checkPointSpinner.setAdapter(checkpointAdapter);

        courseSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                checkpointAdapter.clear();
                for (CheckPoint cp : hike.getCourses().get(i).getCheckPoints()) {
                    checkpointAdapter.add(cp.getName());
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        sendButton.setOnClickListener(new View.OnClickListener() {

            String url = "https://hikex.azurewebsites.net/Admin/record-checkpoint-pass";
            @Override
            public void onClick(View view) {
                JSONObject jsonBody = null;
                try {
                    jsonBody = new JSONObject()
                            .put("startNumber",startNumberText.getText())
                            .put("checkpointId", hike.getCourses().get((int)courseSpinner.getSelectedItemId()).getCheckPoints()[(int)checkPointSpinner.getSelectedItemId()].getId())
                            .put("timeStamp", new DateTime(DateTimeZone.UTC).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                JsonObjectRequest request = new JsonObjectRequest(Request.Method.POST, url, jsonBody,
                        new Response.Listener<JSONObject>() {
                            @Override
                            public void onResponse(JSONObject response) {
                                try {
                                    Toast.makeText(getApplicationContext(),response.getString("message"),Toast.LENGTH_LONG).show();
                                } catch (JSONException e) {
                                    e.printStackTrace();
                                }
                            }
                        },

                        new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {
                                String errorString = new String(error.networkResponse.data);
                                Toast.makeText(getApplicationContext(),errorString,Toast.LENGTH_LONG).show();
                            }

                        }){
                    @Override
                    public Map<String, String> getHeaders() throws AuthFailureError {
                        Map<String, String> params = new HashMap<String, String>();
                        params.put("Authorization", "Bearer "+user.getToken());
                        params.put("content-type", "application/json");
                        return params;
                    }
                };
                volleySingleton.getRequestQueue().add(request);
            }
        });

    }
}
