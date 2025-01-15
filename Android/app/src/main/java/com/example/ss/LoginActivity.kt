package com.example.ss

import android.content.Intent
import android.os.Bundle
import android.text.InputType
import android.util.Log
import android.view.View
import android.widget.CheckBox
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.example.ss.Commands.Commands
import com.example.ss.Init.Init
import com.example.ss.Patterns.p_user
import kotlin.concurrent.thread

class LoginActivity:AppCompatActivity() {
    private lateinit var checkbox_show_or_hide_login:CheckBox
    private lateinit var input_password:EditText
    private lateinit var input_email:EditText
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)
        __init__()
    }
    fun __init__(){
        var init = Init(this)
        input_password = findViewById(R.id.login_input_password_text)
        input_email = findViewById(R.id.login_input_email_text)
        checkbox_show_or_hide_login = findViewById(R.id.checkbox_show_or_hide_password_login)
    }
    private fun showErrorEmail(message: String) {
        var error:TextView = findViewById(R.id.login_error_email)
        error.text = message
        error.visibility = TextView.VISIBLE
    }
    private fun showErrorPassword(message: String) {
        var error:TextView = findViewById(R.id.login_error_password)
        error.text = message
        error.visibility = TextView.VISIBLE
    }
    fun check_gmail_index(email:String):Boolean
    {
        return android.util.Patterns.EMAIL_ADDRESS.matcher(email.toString()).matches()
    }
    fun on_click_checkbox_show_or_hide_login(view: View){
        if(checkbox_show_or_hide_login.isChecked())
            input_password.inputType = InputType.TYPE_CLASS_TEXT or InputType.TYPE_TEXT_VARIATION_NORMAL
        else
            input_password.inputType = InputType.TYPE_CLASS_TEXT or InputType.TYPE_TEXT_VARIATION_PASSWORD
    }
    fun on_click_next_menu(view: View){
        var email = input_email.text.toString()
        var password = input_password.text.toString()
        if(check_gmail_index(email) && password.length > 0)
        {
            thread ()
            {
                try{
                    var com = Commands();
                    var user = p_user();
                    user.Gmail = email
                    user.LastName = "";
                    user.FirstName = "";
                    user.Password = password;
                    user.Id = 0;
                    var reg = com.Login(user)
                    Log.d("status_register", reg.Status)
                    runOnUiThread {
                        if (reg.Status.equals("SUCCESS")) {
                            var intent_a = Intent(this, MenuMessanger::class.java)
                            intent_a.putExtra("email", email)
                            intent_a.putExtra("password", password)
                            startActivity(intent_a)
                        } else {
                            showErrorPassword("Такой пользователь не найден")
                        }
                    }
                }
                catch (e:Exception)
                {
                }
            }
        }
        else
        {
            showErrorPassword("Вы что-то забыли ввести или ввели не правильно")
        }
    }
}