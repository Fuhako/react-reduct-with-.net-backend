
import React, { useState } from "react";
import styles from '../styles/login.module.css'; // Import CSS module
import { useFormik } from "formik";
import * as Yup from "yup";


// Schema validasi Yup
const validationSchema = Yup.object({
    email: Yup.string()
        .email("Invalid email address")
        .required("Email is required"),
    password: Yup.string()
        .min(8, "Password must be at least 8 characters")
        .required("Password is required"),
});

const Login = () => {
    const formik = useFormik({
        initialValues: {
            email: "",
            password: "",
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            console.log("Email:", values.email);
            console.log("Password:", values.password);
            // Implementasikan logika login di sini
        },
    });

    return (
        <div className={styles.container}>
            <form onSubmit={formik.handleSubmit} className={styles.form}>
                <h2>Login</h2>
                <div className={styles.inputGroup}>
                    <label htmlFor="email">Email:</label>
                    <input
                        type="email"
                        id="email"
                        {...formik.getFieldProps('email')}
                        className={styles.input}
                    />
                    {formik.touched.email && formik.errors.email ? (
                        <div className={styles.error}>{formik.errors.email}</div>
                    ) : null}
                </div>
                <div className={styles.inputGroup}>
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        {...formik.getFieldProps('password')}
                        className={styles.input}
                    />
                    {formik.touched.password && formik.errors.password ? (
                        <div className={styles.error}>{formik.errors.password}</div>
                    ) : null}
                </div>
                <button type="submit" className={styles.button}>Login</button>
            </form>
        </div>
    );
}


export default Login;