package com.example.gergosipos.hikex_smarphone_app;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

import POJO.Hike;

public class MySimpleArrayAdapter extends ArrayAdapter<Hike> {
    private final Context context;
    private final List<Hike> hikes;

    public MySimpleArrayAdapter(Context context, List<Hike> hikes) {
        super(context, -1, hikes);
        this.context = context;
        this.hikes = hikes;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        View list_item_view = inflater.inflate(R.layout.list_view_item, parent, false);
        TextView textView = (TextView) list_item_view.findViewById(R.id.hikeName);
        textView.setText(hikes.get(position).getName());
        return textView;
    }
}
