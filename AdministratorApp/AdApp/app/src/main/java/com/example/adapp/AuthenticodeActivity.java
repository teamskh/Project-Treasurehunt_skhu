package com.example.adapp;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

public class AuthenticodeActivity extends AppCompatActivity {
    EditText et_Acode;
    String sAcPw;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_authenticode);

        et_Acode=(EditText)findViewById(R.id.authenticode_et); //edittext 데베 연동 후 확인 버튼 눌렀을 때 비밀번호와 대조해서 맞으면 로그인
        sAcPw=et_Acode.getText().toString();

        Button btACok=findViewById(R.id.acOK_bt);
        btACok.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(et_Acode.getText().toString().trim().length()>0) {
                    Intent mIntent = new Intent(getApplicationContext(), MainActivity.class);
                    startActivity(mIntent);
                }
                else{
                    Toast.makeText(getApplicationContext(), "인증번호를 입력해주세요", Toast.LENGTH_LONG).show();
                }
            }
        });
    }
}
