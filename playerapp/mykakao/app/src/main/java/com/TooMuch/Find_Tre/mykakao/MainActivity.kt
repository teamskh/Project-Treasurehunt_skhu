package com.TooMuch.Find_Tre.mykakao

import android.content.pm.PackageManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Base64
import android.util.Log
import java.security.MessageDigest

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        getKeyKeyHash()
    }

    fun getKeyKeyHash(){
        val packageInfo=packageManager.getPackageInfo(packageName,PackageManager.GET_SIGNATURES)

        for(signature in packageInfo!!.signatures){
            try {
                val md = MessageDigest.getInstance("SHA")
                md.update(signature.toByteArray())
                Log.d("KeyHash",Base64.encodeToString(md.digest(),Base64.NO_WRAP))
            }catch (e: NoSuchFieldException){

            }
        }
    }
}
