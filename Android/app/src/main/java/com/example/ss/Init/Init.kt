package com.example.ss.Init

import android.app.Activity
import android.content.pm.ActivityInfo
import android.view.WindowManager
import androidx.appcompat.app.AppCompatActivity
import com.example.ss.MainActivity

class Init:AppCompatActivity {
    constructor(activity: Activity)
    {
        activity.window.setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        activity.setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT)
    }
}