package com.example.gergosipos.hikex_smarphone_app;

import android.Manifest;
import android.app.Activity;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.nfc.Tag;
import android.nfc.tech.Ndef;
import android.os.Parcelable;
import android.provider.MediaStore;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v4.widget.ImageViewCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.AppCompatEditText;
import android.support.v7.widget.AppCompatSpinner;
import android.util.SparseArray;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.google.android.gms.vision.Frame;
import com.google.android.gms.vision.barcode.Barcode;
import com.google.android.gms.vision.barcode.BarcodeDetector;
import com.google.gson.Gson;
import com.google.gson.JsonObject;

import org.joda.time.DateTime;
import org.joda.time.DateTimeZone;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.io.UnsupportedEncodingException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import POJO.CheckPoint;
import POJO.Hike;
import POJO.HikeCourse;
import POJO.User;

import static android.support.v4.graphics.TypefaceCompatUtil.getTempFile;

public class AdminActivity extends AppCompatActivity {

    AppCompatSpinner courseSpinner;
    AppCompatSpinner checkPointSpinner;
    AppCompatEditText startNumberText;
    AppCompatButton sendButton;
    AppCompatButton readQrButton;

    VolleySingleton volleySingleton;
    User user;
    private SharedPreferences sharedPreferences;
    Hike hike;
    private NfcAdapter mNfcAdapter;

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        handleIntent(intent);
    }

    @Override
        protected void onResume() {
            super.onResume();
            if (mNfcAdapter != null) {
                setupForegroundDispatch(this, mNfcAdapter);
            }
        }

    private void setupForegroundDispatch(final AdminActivity activity, NfcAdapter mNfcAdapter) {
        final Intent intent = new Intent(activity.getApplicationContext(), activity.getClass());
        intent.setFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP);

        final PendingIntent pendingIntent = PendingIntent.getActivity(activity.getApplicationContext(), 0, intent, 0);

        IntentFilter[] filters = new IntentFilter[1];
        String[][] techList = new String[][]{};

        filters[0] = new IntentFilter();
        filters[0].addAction(NfcAdapter.ACTION_NDEF_DISCOVERED);
        filters[0].addCategory(Intent.CATEGORY_DEFAULT);
        try {
            filters[0].addDataType("text/plain");
        } catch (IntentFilter.MalformedMimeTypeException e) {
            throw new RuntimeException("Check your mime type.");
        }
        mNfcAdapter.enableForegroundDispatch(activity, pendingIntent, filters, techList);
    }

    void handleIntent(Intent intent) {
            String action = intent.getAction();
            if (NfcAdapter.ACTION_NDEF_DISCOVERED.equals(action)) {
                String type = intent.getType();
                if ("text/plain".equals(type)) {

                    Tag tag = intent.getParcelableExtra(NfcAdapter.EXTRA_TAG);
                    ReadData(tag);
                } else {
                    Toast.makeText(getApplicationContext(), getString(R.string.nfc_error), Toast.LENGTH_LONG).show();
                }
            }
        }

    private void ReadData(Tag tag) {
        Ndef ndef = Ndef.get(tag);
        if (ndef == null) {
            return;
        }
        NdefMessage ndefMessage = ndef.getCachedNdefMessage();
        NdefRecord[] records = ndefMessage.getRecords();
        NdefRecord ndefRecord = records[0];
            if (ndefRecord.getTnf() == NdefRecord.TNF_WELL_KNOWN && Arrays.equals(ndefRecord.getType(), NdefRecord.RTD_TEXT)) {
                try {
                    byte[] payload = ndefRecord.getPayload();
                    String textEncoding = ((payload[0] & 128) == 0) ? "UTF-8" : "UTF-16";
                    int languageCodeLength = payload[0] & 0063;
                    String s = new String(payload, languageCodeLength + 1, payload.length - languageCodeLength - 1, textEncoding);
                    startNumberText.setText(s);
                    sendPass();
                } catch (UnsupportedEncodingException e) {
                    throw new RuntimeException();
                }
            }
        }

    @Override
    protected void onPause() {
        super.onPause();
        if (mNfcAdapter != null) {
            mNfcAdapter.disableForegroundDispatch(this);
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_admin);


        mNfcAdapter = NfcAdapter.getDefaultAdapter(this);
        sharedPreferences = getApplicationContext().getSharedPreferences(getString(R.string.preference_file_key), Context.MODE_PRIVATE);
        user = new Gson().fromJson(sharedPreferences.getString("user", ""), User.class);
        volleySingleton = VolleySingleton.getInstance(getApplicationContext());

        courseSpinner = findViewById(R.id.courseSpinner);
        checkPointSpinner = findViewById(R.id.checkPointSpinner);
        sendButton = findViewById(R.id.sendButton);
        startNumberText = findViewById(R.id.startNumEditText);
        readQrButton = findViewById(R.id.readQrButton);

        hike = (Hike)getIntent().getSerializableExtra("hike");

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
        final  Activity act = this;
        readQrButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if ( ContextCompat.checkSelfPermission( getApplicationContext(), android.Manifest.permission.ACCESS_COARSE_LOCATION ) != PackageManager.PERMISSION_GRANTED ) {

                    ActivityCompat.requestPermissions( act, new String[] {  Manifest.permission.CAMERA  }, 1000);
                }
                Intent cameraIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                startActivityForResult(cameraIntent,1000);
            }
        });

        sendButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                sendPass();
            }
        });

    }

    void sendPass(){
        String url = getString(R.string.api_url)+"Admin/record-checkpoint-pass";
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

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == RESULT_OK && requestCode == 1000){
            Bundle extras = data.getExtras();
            Bitmap imageBitmap = (Bitmap) extras.get("data");
            BarcodeDetector barcodeDetector = new BarcodeDetector.Builder(this) .setBarcodeFormats(Barcode.QR_CODE) .build();
            Frame myFrame = new Frame.Builder() .setBitmap(imageBitmap) .build();
            SparseArray<Barcode> barcodes = barcodeDetector.detect(myFrame);
            if( barcodes.size() != 0){
                startNumberText.setText(barcodes.valueAt(0).displayValue);
                sendPass();
            }
        }
    }
}
