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
import com.example.ss.Patterns.r_user
import java.util.Locale
import kotlin.concurrent.thread

class RegisterPassword:AppCompatActivity() {
    private lateinit var checkbox_show_or_hide:CheckBox
    private lateinit var input_password:EditText
    private lateinit var input_confirm_password:EditText
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register_password)
        __init__()
    }
    fun __init__(){
        var init = Init(this)
        input_password = findViewById(R.id.register_input_password_text)
        input_confirm_password = findViewById(R.id.register_input_confirm_password_text)
        checkbox_show_or_hide = findViewById(R.id.checkbox_show_or_hide_password)
    }
    fun on_click_checkbox_show_or_hide_register(view:View){
        if(checkbox_show_or_hide.isChecked())
        {
            input_password.inputType = InputType.TYPE_CLASS_TEXT or InputType.TYPE_TEXT_VARIATION_NORMAL
            input_confirm_password.inputType = InputType.TYPE_CLASS_TEXT or InputType.TYPE_TEXT_VARIATION_NORMAL
        }
        else
        {
            input_password.inputType = InputType.TYPE_CLASS_TEXT or InputType.TYPE_TEXT_VARIATION_PASSWORD
            input_confirm_password.inputType = InputType.TYPE_CLASS_TEXT or InputType.TYPE_TEXT_VARIATION_PASSWORD
        }
    }
    fun showErrorPassword(message:String){
        var error:TextView = findViewById(R.id.text_error_password_text)
        error.text = message
        error.visibility = TextView.VISIBLE
    }
    fun showErrorConfirmPassword(message:String){
        var error:TextView = findViewById(R.id.text_error_confirm_password_text)
        error.text = message
        error.visibility = TextView.VISIBLE
    }
    fun search_symbl(str:String, array:CharArray):Boolean
    {
        var b_ok = false
        for(i_ch:Char in array){
            if(str.contains(i_ch))
            {
                b_ok = true
                break
            }
        }
        return b_ok
    }
    fun isAnyUpperCase(s: String): Boolean {
        if (s == s.lowercase(Locale.getDefault())) return false
        return true
    }
    fun check_password(password:String):Boolean{
        var list_simbls = "!@#\$%^&*<>{}():/.\\|".toCharArray()
        var error_list = Array<String>(3){""}
        //main
        var b_check_password = false
        //other
        var b_size_password = false
        var b_have_one_Up = false
        var b_have_one_symbl = false

        if(isAnyUpperCase(password)){
            b_have_one_Up = true
            error_list[0] = ""
        }
        else {
            error_list[0] = "* В пароле должен быть хотя бы одна заглавная буква"
            //error one up
        }
        if(search_symbl(password, list_simbls)){
            b_have_one_symbl = true
            error_list[1] = ""
        }
        else{
            error_list[1] = "* В пароле должен быть хотя бы один символ от сюдова !, @, #, \$, %, ^, &, *, <, >, {, }, (, ), :, /, ., \\, |"
            //error one symbl
        }
        if(password.length >= 9)
        {
            b_size_password = true
            error_list[2] = ""
        }
        else {
            error_list[2] = "* Пароль должен быть больше 8 букв"
            //error size passowrd
        }
        if(b_size_password && b_have_one_Up && b_have_one_symbl)
        {
            b_check_password = true
        }
        else
        {
            var error_message = ""
            for(str in error_list)
            {
                if(str != "")
                {
                    error_message+= "${str} \n"
                }
            }
            showErrorPassword(error_message)
        }
        return b_check_password
    }
    fun on_click_end_register(view:View)
    {
        if(input_password.text.toString().equals(input_confirm_password.text.toString())) {
            if (check_password(input_password.text.toString())) {
                showErrorPassword("")
                showErrorConfirmPassword("")
                thread() {
                    var reg: r_user = r_user()
                    reg.Status = ""
                    reg.UserId -1

                        try {
                            var com = Commands()
                            var user = p_user()
                            user.Id = 1
                            user.Password = input_password.text.toString()
                            user.LastName = intent.getStringExtra("lastname")
                            user.FirstName = intent.getStringExtra("firstname")
                            user.Gmail = intent.getStringExtra("email")
                             reg = com.register(user)
                            Log.d("status_register", reg.Status)
                        }
                        catch (e:Exception){
                            Log.d("error_client_thread", e.message.toString())
                        }
                    runOnUiThread{
                         if(reg.Status.equals("SUCCESS"))
                         {
                             var intent_a = Intent(this, MenuMessanger::class.java);
                             intent_a.putExtra("firstname", intent.getStringExtra("firstname"))
                             intent_a.putExtra("lastname", intent.getStringExtra("lastname"))
                             intent_a.putExtra("email", intent.getStringExtra("email"))
                             intent_a.putExtra("password", input_password.text.toString())
                             startActivity(intent_a)
                         }
                         else
                             showErrorPassword("Ошибка такой пользователь уже есть")
                    }
                }
            }
        }
        else
        {
            //confirm password был введен неправильно
            showErrorConfirmPassword("confirm password был введен неправильно")
        }
    }
}














