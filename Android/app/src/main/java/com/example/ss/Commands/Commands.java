package com.example.ss.Commands;

import com.example.ss.Client.Client;
import com.example.ss.Convertor.Convert;
import com.example.ss.Patterns.*;
import com.example.ss.Resource;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import android.os.Build;
import android.util.Log;

import java.io.IOException;

public class Commands {

    private ObjectMapper obm = new ObjectMapper();
    private Client client;
    public Commands(){
        String host = Resource.host;
        int port = Resource.port;
        client = new Client(host, port);
    }
    private String getDeviceName() {
        var manufacturer = Build.MANUFACTURER;
        var model = Build.MODEL;
        return manufacturer.toString() + " " + model.toString();
    }
    public r_user register(p_user user) throws IOException {
        String json_p_user = obm.writeValueAsString(user);
        p_dtf SET = new p_dtf();
        p_dtf GET = new p_dtf();
        p_transport_command ptc = new p_transport_command();
        SET.NameUser = getDeviceName();
        SET.DataFormat = p_fdt.Json;
        SET.Request = p_rt.POST;
        ptc.Command = "register";
        Log.d("server_read", json_p_user);
        ptc.ExData = Convert.string_to_byte_array(json_p_user);
        SET.Data = Convert.string_to_byte_array(obm.writeValueAsString(ptc));
        client.WriteBytes(Convert.string_to_byte_array(obm.writeValueAsString(SET)));
        String str = Convert.byte_array_to_string(client.ReadBytes());
        Log.d("String_gson", str);
        GET =  obm.readValue(str, p_dtf.class);
        Log.d("String_gson", Convert.byte_array_to_string(GET.Data));
        var json_r_user = Convert.byte_array_to_string(GET.Data);
        Log.d("server_read", json_r_user);
        return obm.readValue(json_r_user, r_user.class);
    }
    public r_user login(p_user user) throws IOException{
        String json_p_user = obm.writeValueAsString(user);
        p_dtf SET = new p_dtf();
        p_dtf GET = new p_dtf();
        p_transport_command ptc = new p_transport_command();
        //
        SET.NameUser = getDeviceName();
        SET.DataFormat = p_fdt.Json;
        SET.Request = p_rt.POST;
        ptc.Command = "login";
        //
        Log.d("server_read", json_p_user);
        ptc.ExData = Convert.string_to_byte_array(json_p_user);
        SET.Data = Convert.string_to_byte_array(obm.writeValueAsString(ptc));
        //
        client.WriteBytes(Convert.string_to_byte_array(obm.writeValueAsString(SET)));
        String str = Convert.byte_array_to_string(client.ReadBytes());
        //
        Log.d("String_gson", str);
        GET =  obm.readValue(str, p_dtf.class);
        var json_r_user = Convert.byte_array_to_string(GET.Data);
        Log.d("server_read", json_r_user);
        //
        return obm.readValue(json_r_user, r_user.class);
    }
    public p_img_u_info get_img_user_info(p_user user) throws IOException {
        p_img_u_info r_piui = new p_img_u_info();
        p_cmu obj_pcmu = new p_cmu();
        obj_pcmu.command = "user_get_info_img";
        obj_pcmu.user = user;
        String json_psmu = obm.writeValueAsString(obj_pcmu);
        //init
        p_dtf SET = new p_dtf();
        p_dtf GET = new p_dtf();
        //
        p_transport_command ptc = new p_transport_command();
        SET.NameUser = getDeviceName();
        SET.DataFormat = p_fdt.Json;
        SET.Request = p_rt.POST;
        ptc.Command = "user_get_info_img";
        //
        ptc.ExData = Convert.string_to_byte_array(json_psmu);
        SET.Data = Convert.string_to_byte_array(obm.writeValueAsString(ptc));
        //
        client.WriteBytes(Convert.string_to_byte_array(obm.writeValueAsString(SET)));
        String str = Convert.byte_array_to_string(client.ReadBytes());
        //
        GET =  obm.readValue(str, p_dtf.class);
        var json_r_p_img_u_info = Convert.byte_array_to_string(GET.Data);
        //
        return obm.readValue(json_r_p_img_u_info, p_img_u_info.class);
    }
    public String change_img_profile(p_user user, byte[] image) throws IOException {
        p_cm_img obj_p_cm_img = new p_cm_img();
        obj_p_cm_img.user = user;
        obj_p_cm_img.image = image;
        obj_p_cm_img.command = "user_set_img_profile";
        //
        String json_psmimg = obm.writeValueAsString(obj_p_cm_img);
        //init
        p_dtf SET = new p_dtf();
        p_dtf GET = new p_dtf();
        //
        p_transport_command ptc = new p_transport_command();
        SET.NameUser = getDeviceName();
        SET.DataFormat = p_fdt.Json;
        SET.Request = p_rt.POST;
        ptc.Command = "user_set_img_profile";
        //
        ptc.ExData = Convert.string_to_byte_array(json_psmimg);
        SET.Data = Convert.string_to_byte_array(obm.writeValueAsString(ptc));
        //
        client.WriteBytes(Convert.string_to_byte_array(obm.writeValueAsString(SET)));
        String str = Convert.byte_array_to_string(client.ReadBytes());
        //
        GET =  obm.readValue(str, p_dtf.class);
        var json_r_p_img_u_info = Convert.byte_array_to_string(GET.Data);
        //
        return json_r_p_img_u_info;
    }
}
