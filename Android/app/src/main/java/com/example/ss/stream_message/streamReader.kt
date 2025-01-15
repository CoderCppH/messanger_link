package com.example.ss.stream_message

import android.util.Log
import android.util.Xml.Encoding
import java.io.IOException
import java.net.Socket
import java.nio.charset.Charset

class streamReader {
    fun readBytes(socket: Socket): ByteArray? {
        Log.d("pack_transform", "read_stream_start")
        var getByts = ByteArray(0);
        try
        {
            var sizeData = ByteArray(4)
            socket.getInputStream().read(sizeData, 0, sizeData.size);
            var fileSize = byteArrayToInt(sizeData)
            var fileData = ByteArray(fileSize)
            var totalBytesRead = 0
            while(totalBytesRead < fileSize)
            {
                var byteRead = socket.getInputStream().read(fileData, totalBytesRead, fileSize - totalBytesRead)
                if(byteRead == 0)
                    break

                totalBytesRead += byteRead
            }
            getByts = fileData

        }
        catch (e:Exception)
        {
            Log.d("error_stream_read", e.message.toString())
            return null
        }
        Log.d("pack_transform", "read_stream_end")
        return getByts;
    }
    fun byteArrayToInt(byteArray: ByteArray): Int {
        if (byteArray.size < 4) {
            throw IllegalArgumentException("ByteArray must have at least 4 bytes")
        }
        return (byteArray[3].toInt() shl 24) or
                (byteArray[2].toInt() and 0xFF shl 16) or
                (byteArray[1].toInt() and 0xFF shl 8) or
                (byteArray[0].toInt() and 0xFF)
    }
}