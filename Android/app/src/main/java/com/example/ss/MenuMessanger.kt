package com.example.ss

import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.os.Bundle
import android.util.Log
import android.widget.ImageView
import androidx.appcompat.app.AppCompatActivity
import com.example.ss.Commands.Commands
import com.example.ss.DataBase.UserRepository
import com.example.ss.Init.Init
import com.example.ss.Patterns.p_img_u_info
import com.example.ss.Patterns.p_user
import kotlin.concurrent.thread

class MenuMessanger:AppCompatActivity() {
    private  lateinit var profile_img:ImageView
    private lateinit var user_info: p_img_u_info
    private lateinit var user_rep:UserRepository
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_menu_messanger)
        __init__()
    }

    fun __init__(){
        var init = Init(this)
        user_rep = UserRepository(this)
        profile_img = findViewById(R.id.profil_img)
        var email:String = ""
        var password:String = ""
        var user_sql = user_rep.getUser()
        if(user_sql.Id == 1)
        {
            email = user_sql.Gmail
            password = user_sql.Password
        }
        else if(user_sql.Id == -1)
        {
            email = intent.getStringExtra("email").toString()
            password =intent.getStringExtra("password").toString()
        }
        thread()
        {
           /* try {*/
                // var mail = MailSs()
                //mail.sendMessage("Hello this is android", "head for java", "hellopley4@gmail.com")
                var com = Commands();
                var user = p_user();
                user.Gmail = email
                user.LastName = "";
                user.FirstName = "";
                user.Password = password
                user.Id = 0;

                var reg = com.get_img_user_info(user)
                user_info = reg
                if(user_sql.Id == -1){
                    user_rep.addUser(user_info.user)
                }
                runOnUiThread{
                    val bmp: Bitmap = BitmapFactory.decodeByteArray(user_info.img, 0, user_info.img.size)
                    profile_img.setImageBitmap(bmp)
                }
                Log.d("status_register", user_info.user.LastName )
            /*}
            catch (e:Exception){
                Log.d("error_client_thread", e.message.toString())
            }*/
        }
    }
}