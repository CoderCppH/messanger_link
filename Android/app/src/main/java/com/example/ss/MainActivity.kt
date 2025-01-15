package com.example.ss

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.View
import androidx.appcompat.app.AppCompatActivity
import com.example.ss.Commands.Commands
import com.example.ss.DataBase.UserRepository
import com.example.ss.Init.Init
import com.example.ss.Patterns.p_user
import kotlin.concurrent.thread

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        var init = Init(this)

       /* thread()
        {
            try {
                // var mail = MailSs()
                //mail.sendMessage("Hello this is android", "head for java", "hellopley4@gmail.com")
                var com = Commands();
                var user = p_user();
                user.Gmail = "pargev20002607@gmail.com"
                user.LastName = "";
                user.FirstName = "";
                user.Password = "Pargev2005@";
                user.Id = 0;

                var reg = com.get_img_user_info(user)
                Log.d("status_register", reg.img.toString())
            }
            catch (e:Exception){
                Log.d("error_client_thread", e.message.toString())
            }
        }*/
        var user_rep = UserRepository(this)
        if(user_rep.getUser().Id == 1)
        {
            var intent_a = Intent(this, MenuMessanger::class.java);
            intent_a.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK)
            startActivity(intent_a)
        }
    }
    public fun on_click_login(view: View){
        var intent = Intent(this, LoginActivity::class.java)
        startActivity(intent)
    }
    public fun on_click_register(view: View){
        var intent = Intent(this, RegisterActivityEmail::class.java)
        startActivity(intent)
    }


}