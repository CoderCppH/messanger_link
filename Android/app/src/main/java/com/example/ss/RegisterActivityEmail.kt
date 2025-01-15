package com.example.ss

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.example.ss.Init.Init

class RegisterActivityEmail:AppCompatActivity() {
    private lateinit var input_gmail:EditText
    private lateinit var error_text_view:TextView
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register_email)
        __init__()
    }
    fun __init__(){
        var init = Init(this);
        input_gmail = findViewById(R.id.register_input_gmail_text)
        error_text_view = findViewById(R.id.textViewError)
    }
    private fun showError(message: String) {
        error_text_view.text = message
        error_text_view.visibility = TextView.VISIBLE
    }
    fun check_gmail_index(email:String):Boolean
    {
        return android.util.Patterns.EMAIL_ADDRESS.matcher(email.toString()).matches()
    }
    fun on_click_next_activity(view:View){
        var email = input_gmail.text.toString().trim()
        if(check_gmail_index(email)) {
            error_text_view.visibility = TextView.GONE
            var intent_a = Intent(this, RegisterConfirmEmail::class.java)
            intent_a.putExtra("email", email)
            startActivity(intent_a)
        }
        else if(email.isEmpty())
            showError("Email не может быть пустым")
        else
            showError("Неверный формат email")
    }
}










