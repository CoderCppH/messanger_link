package com.example.ss.Patterns;

import com.fasterxml.jackson.annotation.JsonCreator;

public enum p_rt {
    GET(202),
    POST(303),
    PUT(404),
    DELETE(505),
    NULL(444);
    private final int code;

    p_rt(int code) {
        this.code = code;
    }
    @JsonCreator
    public static p_rt fromCode(int code) {
        for (p_rt rt : p_rt.values()) {
            if (rt.code == code) {
                return rt;
            }
        }
        throw new IllegalArgumentException("Invalid code: " + code);
    }

    public int getCode() {
        return code;
    }
}
