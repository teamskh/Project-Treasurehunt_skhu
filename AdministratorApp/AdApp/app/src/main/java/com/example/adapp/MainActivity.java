package com.example.adapp;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Dialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.ContextMenu;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.Toast;
import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {//뒤로가기 종료 추가해야함
    ArrayList<String>alContests;
    ArrayAdapter<String>adapter;
    String ViewPosition;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ListView lvContests=findViewById(R.id.lvContests);
//        final ArrayList<String>alContests=new ArrayList<String>();
////        final ArrayAdapter<String>adapter=new ArrayAdapter<String>
////                (this, android.R.layout.simple_list_item_1,alContests);
        alContests=new ArrayList<String>();
        adapter=new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1,alContests);
        lvContests.setAdapter(adapter);
        lvContests.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent ctIntent = new Intent(getApplicationContext(), AdministratorMenuActivity.class);
                startActivity(ctIntent);
            }
        });
        registerForContextMenu(lvContests);
    }

    @Override
    public void onCreateContextMenu(ContextMenu menu, View v, ContextMenu.ContextMenuInfo menuInfo) {
        MenuInflater inflater=getMenuInflater();
        inflater.inflate(R.menu.cmenu_activity_main_lvcontests, menu);
    }

    @Override
    public boolean onContextItemSelected(@NonNull MenuItem item) {
        AdapterView.AdapterContextMenuInfo info=(AdapterView.AdapterContextMenuInfo)item.getMenuInfo();
        switch (item.getItemId()){
            case R.id.cmenu_Delete:
                alContests.remove(info.position);
                adapter.notifyDataSetChanged();
                return true;
            default:
                return false;
        }
    }

    public void onRadioButtonClicked(View view){
        boolean checked=((RadioButton)view).isChecked();
        switch (view.getId()){
            case R.id.individualG_rb:
                if(checked)
                    //Toast.makeText(getApplicationContext(), "개인전", Toast.LENGTH_SHORT).show();
                break;
            case R.id.TeamG_rb:
                if(checked)
                    //Toast.makeText(getApplicationContext(), "팀전", Toast.LENGTH_SHORT).show();
                break;
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.contest_add, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch(item.getItemId()){
            case R.id.addCt:

                final Dialog AddCDialog=new Dialog(this);
                AddCDialog.setContentView(R.layout.add_contest_dialog);
                AddCDialog.setTitle("대회 추가 팝업창");

                final EditText Contest_et=(EditText)AddCDialog.findViewById(R.id.Contest_et);
                final EditText TeamM_et=(EditText)AddCDialog.findViewById(R.id.TeamM_et);
                final EditText CtPW_et=(EditText)AddCDialog.findViewById(R.id.CtPW_et);

                final Button CtAdd_bt=(Button)AddCDialog.findViewById(R.id.CtAdd_bt);
                final Button CtCancel_bt=(Button)AddCDialog.findViewById(R.id.CtCancel_bt);

                CtAdd_bt.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        if(Contest_et.getText().toString().trim().length()>0&&
                        TeamM_et.getText().toString().trim().length()>0&&
                        CtPW_et.getText().toString().trim().length()>0){
                            alContests.add(Contest_et.getText().toString());
                            Contest_et.setText("");
                            adapter.notifyDataSetChanged();
                            AddCDialog.dismiss();
                        }
                        else{
                            Toast.makeText(getApplicationContext(), "모든 칸을 채워주세요",Toast.LENGTH_LONG).show();
                        }
                    }
                });

                CtCancel_bt.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        AddCDialog.dismiss();
                    }
                });
                AddCDialog.show();
        }
        return super.onOptionsItemSelected(item);
    }
}
