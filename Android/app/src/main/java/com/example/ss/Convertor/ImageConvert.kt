package com.example.ss.Convertor

import android.content.ContentResolver
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.net.Uri
import java.io.ByteArrayOutputStream
import java.io.InputStream

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
        public fun uriToByteArray(uri: Uri?, contentResolver: ContentResolver): ByteArray? {
            val contentResolver: ContentResolver = contentResolver
            return try {
                val inputStream: InputStream? = uri?.let { contentResolver.openInputStream(it) }
                val bitmap: Bitmap? = BitmapFactory.decodeStream(inputStream)
                val byteArrayOutputStream = ByteArrayOutputStream()
                bitmap?.compress(Bitmap.CompressFormat.PNG, 100, byteArrayOutputStream)
                byteArrayOutputStream.toByteArray()
            } catch (e: Exception) {
                e.printStackTrace()
                null
            }
        }
    }
}