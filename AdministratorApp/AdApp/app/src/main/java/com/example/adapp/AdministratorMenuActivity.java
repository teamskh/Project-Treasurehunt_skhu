package com.example.adapp;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

public class AdministratorMenuActivity extends AppCompatActivity {
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_administrator_menu);

        final Button AdmQuiz_bt=(Button)findViewById(R.id.quiz_bt);
        final Button AdmCtSetup_bt=(Button)findViewById(R.id.ctsetup_bt);
        final Button AdmCtSE_bt=(Button)findViewById(R.id.ctSE_bt);
        final Button AdmRTRank_bt=(Button)findViewById(R.id.RTRank_bt);

        AdmQuiz_bt.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            }
        });
        AdmCtSetup_bt.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            }
        });
        AdmCtSE_bt.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            }
        });
        AdmRTRank_bt.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
            }
        });
    }
}
