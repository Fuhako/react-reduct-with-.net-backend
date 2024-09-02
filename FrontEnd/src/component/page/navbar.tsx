import { useSelector, useDispatch } from 'react-redux';
import { logout } from '../../state/counter/counterSlice';
import { RootState } from '../../state/store';
import styles from '../styles/navbar.module.css'; // Import CSS module

const Navbar = () => {

    const dispatch = useDispatch();
    const { name, isAutenticated } = useSelector((state: RootState) => state.login);

    return (
        <nav className={styles.navbar}>
            <div className={styles.logo}>
                <a href="/">MyApp</a>
            </div>
            <ul className={styles.navLinks}>
                <li><a href="/">Home</a></li>
                <li><a href="/about">About</a></li>
                <li><a href="/contact">Contact</a></li>
                {!isAutenticated ? (
                    <li><a href="/login">Login</a></li>
                ) : (
                    <>
                        <li><a href="/dashboard">Dashboard</a></li>
                        <li><a href="/profile">Profile</a></li>
                        <li>Hello, {name}</li>
                        <li>
                            <button onClick={() => dispatch(logout())}>
                                Logout
                            </button>
                        </li>
                    </>
                )}
            </ul>
        </nav>
    );
}

export default Navbar;
