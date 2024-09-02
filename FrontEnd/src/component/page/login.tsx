import React from "react";
import styles from '../styles/login.module.css'; // Import CSS module
import { useFormik } from "formik";
import * as Yup from "yup";
import axios from "axios";
import { useDispatch } from "react-redux";
import { login, setAutenticated } from "../../state/counter/counterSlice"; // Pastikan jalur ini benar

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
    const dispatch = useDispatch(); // Inisialisasi useDispatch

    const formik = useFormik({
        initialValues: {
            email: "",
            password: "",
        },
        validationSchema: validationSchema,
        onSubmit: async (values) => {
            try {
                const response = await axios.post("https://localhost:7073/api/Auth/Login", {
                    email: values.email,
                    password: values.password,
                });

                if (response.status === 200) {
                    const token = response.data.data.token;
                    console.log("Login successful:", response);
                    console.log("Token:", token);

                    // Simpan token di local storage
                    localStorage.setItem('userToken', token);

                    const result = await axios.get("https://localhost:7073/api/Auth/user", {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    });

                    if (result.status === 200) {
                        alert("WELCOME");
                        dispatch(login({ name: result.data.user_id, token })); // Update state Redux
                        dispatch(setAutenticated(true)); // Update status autentikasi
                    }
                }
                // Handle successful login, e.g., store token, redirect, etc.
            } catch (ex) {
                console.log("Login failed", ex);
                // Handle login failure
            }
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
};

export default Login;
