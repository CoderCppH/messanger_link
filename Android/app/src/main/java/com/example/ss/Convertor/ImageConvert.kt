package com.example.ss.Convertor

import android.graphics.Bitmap
import android.graphics.BitmapFactory
import java.io.ByteArrayOutputStream

class ImageConvert {
    companion object
    {
        public fun BytesToImg(img:ByteArray):Bitmap
        {
            return  BitmapFactory.decodeByteArray(img, 0, img.size)
        }
        public fun ImgToBytes(img:Bitmap):ByteArray
        {
            val stream = ByteArrayOutputStream()
            img.compress(Bitmap.CompressFormat.PNG, 100, stream)
            return stream.toByteArray()
        }
    }
}