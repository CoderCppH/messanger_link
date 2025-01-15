package com.example.ss.Client;

import com.example.ss.stream_message.streamReader;
import com.example.ss.stream_message.streamWriter;

import java.io.IOException;
import java.net.Socket;

public class Client {
    private  String _host;
    private  int _port;
    private Socket socket;
    private streamWriter out;
    private streamReader in;
    private String errMsg = "ok";
    public Client(String host, int port) {
        this._host = host;
        this._port = port;
        out = new streamWriter();
        in = new streamReader();
        try {
            socket = new Socket(_host, _port);
        } catch (Exception e){
            errMsg = e.getMessage();
        }
    }
    public void WriteString(String message) throws IOException {
        byte[] buffer = message.getBytes(java.nio.charset.StandardCharsets.UTF_8);
        out.writeBytes(socket, buffer);
    }
    public void WriteBytes(byte[] buffer) throws IOException {
        out.writeBytes(socket, buffer);
    }
    public String ReadString(){
        String text = "";
        byte[] getBuffer = new byte[1];
        getBuffer = in.readBytes(socket);
        text = new String(getBuffer, java.nio.charset.StandardCharsets.UTF_8);
        return text;
    }
    public byte[] ReadBytes(){
        byte[] getBuffer = new byte[1];
       getBuffer = in.readBytes(socket);
        return getBuffer;
    }
    public String GetErrorMsg(){
        return errMsg;
    }

}
