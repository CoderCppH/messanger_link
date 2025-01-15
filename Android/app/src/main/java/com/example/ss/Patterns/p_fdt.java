package com.example.ss.Patterns;

public enum p_fdt {
    File(0),
    Json(1),
    Raw(2),
    NULL(444);
    private final int code;
    p_fdt(int code)
    {
        this.code = code;
    }
    public int getCode() {
        return code;
    }
}
