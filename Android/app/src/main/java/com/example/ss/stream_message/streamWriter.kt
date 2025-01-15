package com.example.ss.stream_message

import android.util.Log
import java.io.OutputStream
import java.net.Socket
import kotlin.contracts.Effect

class streamWriter {

    fun writeBytes(socket: Socket, writeBuffer: ByteArray) {
        Log.d("pack_transform", "write_stream_start")
        try
        {
            var sizeData:ByteArray = toByteArray(writeBuffer.size)
            Log.d("pack_transform", "send size data: " + (writeBuffer.size).toString() + "; bytes { ${sizeData[0]}, ${sizeData[1]}, ${sizeData[2]}, ${sizeData[3]}}")
            socket.getOutputStream().write(sizeData, 0, sizeData.size)
            socket.getOutputStream().write(writeBuffer, 0, writeBuffer.size)
            socket.getOutputStream().flush()
        }
        catch (e:Exception)
        {
            Log.d("error_stream_write", e.message.toString())
        }
        Log.d("pack_transform", "write_stream_end")
    }
    fun toByteArray(size:Int): ByteArray {

        val buffer = ByteArray(4).also {
            it[3] = (size shr 24 and 0xFF).toByte() // высший байт
            it[2] = (size shr 16 and 0xFF).toByte() // второй байт
            it[1] = (size shr 8 and 0xFF).toByte()  // третий байт
            it[0] = (size and 0xFF).toByte()        // младший байт
        }
        return buffer
    }
}