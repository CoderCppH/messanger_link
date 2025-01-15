package com.example.ss.DataBase

import android.annotation.SuppressLint
import android.content.ContentValues
import android.content.Context
import com.example.ss.Patterns.p_user


class UserRepository(context: Context) {
    private val dbHelper = DatabaseHelper(context)

    // Добавление пользователя
    fun addUser (user: p_user) {
        if(getUser().Id == -1){
            dbHelper.writableDatabase.use { db ->
                val values = ContentValues().apply {
                    put(DatabaseHelper.COLUMN_FIRSTNAME, user.FirstName)
                    put(DatabaseHelper.COLUMN_LASTNAME, user.LastName)
                    put(DatabaseHelper.COLUMN_EMAIL, user.Gmail)
                    put(DatabaseHelper.COLUMN_PASSWORD, user.Password)
                }
                db.insert(DatabaseHelper.TABLE_USERS, null, values)
            }
        }
    }

    // Получение всех пользователей
    @SuppressLint("Range")
    fun getUser(): p_user {
        var r_user:p_user = p_user()
        r_user.Id = -1
        dbHelper.readableDatabase.use { db->
        val cursor:  android. database.Cursor = db.query(DatabaseHelper.TABLE_USERS, null, null, null, null, null, null)

        if (cursor.moveToFirst()) {
                val id = cursor.getInt(cursor.getColumnIndex(DatabaseHelper.COLUMN_ID))
                val firstname = cursor.getString(cursor.getColumnIndex(DatabaseHelper.COLUMN_FIRSTNAME))
                val lastname = cursor.getString(cursor.getColumnIndex(DatabaseHelper.COLUMN_LASTNAME))
                val email = cursor.getString(cursor.getColumnIndex(DatabaseHelper.COLUMN_EMAIL))
                val password = cursor.getString(cursor.getColumnIndex(DatabaseHelper.COLUMN_PASSWORD))

                val user = p_user()
                user.Id = id
                user.Gmail = email
                user.LastName = lastname
                user.FirstName = firstname
                user.Password = password
                r_user = user
        }
        cursor.close()
        }
        return r_user
    }

    fun deleteUser () {
       dbHelper.writableDatabase.use {  db->
        db.delete(DatabaseHelper.TABLE_USERS, "${DatabaseHelper.COLUMN_ID} = ?", arrayOf(1.toString()))
       }
    }
}