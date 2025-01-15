package com.example.ss

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.example.ss.Init.Init
import kotlinx.coroutines.newSingleThreadContext

class RegisterPersonalData:AppCompatActivity() {
    private lateinit var firstname:EditText
    private lateinit var lastname:EditText
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register_personal_data)
        __init__()
    }
    fun __init__(){
        var init = Init(this)
        firstname = findViewById(R.id.register_input_firstname_text)
        lastname = findViewById(R.id.register_input_lastname_text)
    }
    fun showErrorFirstnameText(message:String)
    {
        var error:TextView = findViewById(R.id.text_error_firstname_text)
        error.text = message
        error.visibility = TextView.VISIBLE
    }
    fun showErrorLastnameText(message: String)
    {
        var error:TextView = findViewById(R.id.text_error_lastname_text)
        error.text = message
        error.visibility = TextView.VISIBLE
    }
    fun on_click_next_activity_for_password(view:View){
        var firstname_text = firstname.text.toString()
        var lastname_text = lastname.text.toString()
        var b_f_text = false;
        var b_l_text = false;
        if(firstname_text.isEmpty())
            showErrorFirstnameText("FirstName не может быть пустым")
        else{
            findViewById<TextView>(R.id.text_error_firstname_text).visibility = TextView.GONE
            b_f_text = true
        }
        if(lastname_text.isEmpty())
            showErrorLastnameText("Lastname не может быть пустым")
        else {
            findViewById<TextView>(R.id.text_error_lastname_text).visibility = TextView.GONE
            b_l_text = true
        }
        if(b_f_text && b_l_text){
            var intent_a = Intent(this, RegisterPassword::class.java)
            intent_a.putExtra("email", intent.getStringExtra("email"))
            intent_a.putExtra("firstname", firstname_text)
            intent_a.putExtra("lastname", lastname_text)
            startActivity(intent_a)
        }

    }

}