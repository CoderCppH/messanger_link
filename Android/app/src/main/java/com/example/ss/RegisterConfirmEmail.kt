package com.example.ss

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.ss.Init.Init
import com.example.ss.MailSs.MailSs
import kotlin.concurrent.thread
import kotlin.random.Random

class RegisterConfirmEmail:AppCompatActivity() {
    private var g_code:String = ""
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activirt_register_confirm_email)
        __init__()
        thread() {
            var email_sender = MailSs()
            g_code = generatorCode()
            var body_text = "только ни кому не сообщай об этом коде !!! \n code: ${g_code}"
            var head_text = "очень важный код от ChatLink"
            var email_for_send_this_message = intent.getStringExtra("email")
            email_sender.sendMessage(body_text, head_text, email_for_send_this_message);
        }
    }
    fun __init__()
    {
        var init = Init(this)
    }
    fun generatorCode():String
    {
        var code = ""
        var alpha = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890"
        for (i:Int in 0..5)
        {
            var c_rand_alpha = alpha[Random.nextInt(0, alpha.length-1)]
            code+= c_rand_alpha;
        }
        return code
    }
    fun showError(message:String){
        var error_text = findViewById<TextView>(R.id.text_error_view_code)
        error_text.text = message;
        error_text.visibility = TextView.VISIBLE
    }
    fun on_click_confirm_code(view: View){
        var code = findViewById<EditText>(R.id.input_confirm_code).text.toString()
        if(g_code.equals(code)){
            Toast.makeText(this,"код правильный",Toast.LENGTH_SHORT).show()
            var intent_a = Intent(this, RegisterPersonalData::class.java);
            intent_a.putExtra("email", intent.getStringExtra("email"))
            startActivity(intent_a)
        }
        else
            showError("код не правильный :( !!!")
    }

}