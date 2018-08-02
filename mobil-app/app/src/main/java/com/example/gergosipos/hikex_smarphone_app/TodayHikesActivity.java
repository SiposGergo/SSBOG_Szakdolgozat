package com.example.gergosipos.hikex_smarphone_app;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.List;

import POJO.Hike;
import POJO.User;

public class TodayHikesActivity extends AppCompatActivity {

    TextView userNameTxt;
    Button exitBtn;
    ListView hikeListView;

    User user;
    SharedPreferences sharedPreferences;
    VolleySingleton volleySingleton;
    List<Hike> hikes;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_today_hikes);

        userNameTxt = findViewById(R.id.userNameTxt);
        exitBtn = findViewById(R.id.exitBtn);
        hikeListView = findViewById(R.id.hikeListView);

        volleySingleton = VolleySingleton.getInstance(getApplicationContext());
        sharedPreferences = getSharedPreferences(getString(R.string.preference_file_key), Context.MODE_PRIVATE);
        user = new Gson().fromJson(sharedPreferences.getString("user",""), User.class);
        userNameTxt.setText(user.getUserNam());

        hikeListView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                Hike selected = hikes.get(i);
                if (selected.isHelper(user.getId())){
                    Intent myIntent = new Intent(getBaseContext(),AdminActivity.class);
                    myIntent.putExtra("hike",selected);
                    startActivity(myIntent);
                }
                else {
                    Toast.makeText(getApplicationContext(), "Nincs jogod teljesítési adatokat felvinni!",Toast.LENGTH_LONG).show();
                }
            }
        });

        exitBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SharedPreferences.Editor edit =  sharedPreferences.edit().remove("user");
                edit.apply();
                Intent myIntent = new Intent(getBaseContext(), LoginActivity.class);
                startActivity(myIntent);
                finish();
            }
        });

        String url = "https://api.myjson.com/bins/vzas8";
        StringRequest request = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                Type listType = new TypeToken<List<Hike>>(){}.getType();
                List<Hike> responseHikes = new Gson().fromJson(response, listType);
                hikes = responseHikes;
                hikeListView.setAdapter(new MySimpleArrayAdapter(getApplicationContext(), hikes));
            }

        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getApplicationContext(), error.getLocalizedMessage(),Toast.LENGTH_LONG).show();
            }
        });
        volleySingleton.getRequestQueue().add(request);
    }
}
