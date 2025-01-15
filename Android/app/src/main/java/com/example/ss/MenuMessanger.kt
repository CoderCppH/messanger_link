package com.example.ss

import android.content.Intent
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.net.Uri
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.ImageView
import androidx.appcompat.app.AppCompatActivity
import com.example.ss.Commands.Commands
import com.example.ss.Convertor.ImageConvert
import com.example.ss.DataBase.UserRepository
import com.example.ss.Init.Init
import com.example.ss.Patterns.p_cm_img
import com.example.ss.Patterns.p_img_u_info
import com.example.ss.Patterns.p_user
import kotlin.concurrent.thread

class MenuMessanger:AppCompatActivity() {
    private  lateinit var profile_img:ImageView
    private lateinit var user_info: p_img_u_info
    private lateinit var user_rep:UserRepository
    private  var imageUri:Uri? = null
    private val PICK_IMAGE_REQUEST = 1
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
            password = intent.getStringExtra("password").toString()
        }
        Log.d("I_MENU_GET_USER_INFO", " email: $email password: $password user_id: ${user_sql.Id}")
        thread()
        {
           /* try {*/
                // var mail = MailSs()
                //mail.sendMessage("Hello this is android", "head for java", "hellopley4@gmail.com")
                var command = Commands();
                var user = p_user();
                user.Gmail = email
                user.LastName = "";
                user.FirstName = "";
                user.Password = password
                user.Id = 0;

                var reg = command.get_img_user_info(user)
                if(reg.user.FirstName == "")
                {
                    if(user_rep.getUser().Id == 1){
                        user_rep.deleteUser()
                        var intent_a = Intent(this, MainActivity::class.java);
                        intent_a.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK)
                        startActivity(intent_a)
                    }
                }
                user_info = reg
                if(user_sql.Id == -1){
                    user_rep.addUser(user_info.user)
                }
                runOnUiThread{
                    val bmp: Bitmap = BitmapFactory.decodeByteArray(user_info.img, 0, user_info.img.size)
                    profile_img.setImageBitmap(bmp)
                }
                Log.d("status_register", user_info.user.FirstName.toString() )
            /*}
            catch (e:Exception){
                Log.d("error_client_thread", e.message.toString())
            }*/
        }
    }
    fun on_click_exit_akk(view:View)
    {
        if(user_rep.getUser().Id == 1){
            user_rep.deleteUser()
            var intent_a = Intent(this, MainActivity::class.java);
            intent_a.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK)
            startActivity(intent_a)
        }
    }
    fun on_click_change_photo(view:View)
    {
        openImageChooser()
    }
    private fun openImageChooser() {
        val intent = Intent(Intent.ACTION_PICK)
        intent.type = "image/*"
        startActivityForResult(intent, PICK_IMAGE_REQUEST)
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (requestCode == PICK_IMAGE_REQUEST && resultCode == RESULT_OK && data != null) {
            imageUri = data.data
            Log.d("image_uri","image get ${imageUri.toString()}")
            thread ()
            {
                var command = Commands();
                var status_change_img = command.change_img_profile(user_rep.getUser(), ImageConvert.uriToByteArray(imageUri, contentResolver))
                Log.d("status_change_img", "status $status_change_img  user{${user_rep.getUser().Gmail}, ${user_rep.getUser().FirstName}, ${user_rep.getUser().LastName}, ${user_rep.getUser().Password}}" )
                /*var reg = command.get_img_user_info(user_rep.getUser())
                user_info = reg;
                runOnUiThread{
                    val bmp: Bitmap = BitmapFactory.decodeByteArray(user_info.img, 0, user_info.img.size)
                    profile_img.setImageBitmap(bmp)
                }*/
            }
        }
    }
}