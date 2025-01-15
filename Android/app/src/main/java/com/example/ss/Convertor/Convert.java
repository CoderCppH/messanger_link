package com.example.ss.Convertor;
public class Convert {
    public static String byte_array_to_string(byte[] buffer)
    {
        return new String(buffer, java.nio.charset.StandardCharsets.UTF_8);
    }
    public static byte[] string_to_byte_array(String text)
    {
        return text.getBytes(java.nio.charset.StandardCharsets.UTF_8);
    }
}
