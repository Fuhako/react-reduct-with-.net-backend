import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, IconButton, Divider, CircularProgress, Alert } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import HomeIcon from "@mui/icons-material/Home";
import CategoryIcon from "@mui/icons-material/Category";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useNavigate } from 'react-router-dom';

import { useMediaQuery, useTheme } from "@mui/material";
import { useSelector, useDispatch } from "react-redux";
import { RootState, AppDispatch } from '../../state/store';
import { fetchMenu } from '../../state/counter/counterSlice';
import ProductIcon from '@mui/icons-material/Store';
import VariantIcon from '@mui/icons-material/Widgets';
import TransactionsIcon from '@mui/icons-material/AccountBalance';

const routeConfig = [
    { path: '/products', icon: <ProductIcon />, label: 'Products' },
    { path: '/products-category', icon: <CategoryIcon />, label: 'Product Categories' },
    { path: '/product-variant', icon: <VariantIcon />, label: 'Product Variants' },
    { path: '/manage-transactions', icon: <TransactionsIcon />, label: 'Manage Transactions' },
];
const Sidebar: React.FC = () => {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down("sm"));
    const [open, setOpen] = React.useState(false);

    const dispatch: AppDispatch = useDispatch();
    const { Menu, isLoading, error } = useSelector((state: RootState) => state.menu);
    const menuAccess = useSelector((state: RootState) => state.login.menuAccessByRoleId);

    const handleDrawerToggle = () => {
        setOpen(!open);
    };

    useEffect(() => {
        dispatch(fetchMenu());
    }, [dispatch]);

    const getIcon = (iconName: string) => {
        switch (iconName) {
            case 'HomeIcon':
                return <HomeIcon />;
            case 'CategoryIcon':
                return <CategoryIcon />;
            case 'AccountCircleIcon':
                return <AccountCircleIcon />;
            default:
                return <MenuIcon />;
        }
    };

    const filteredMenuItems = Menu
        .filter(item => menuAccess.some(access => access.menu_id === item.id && access.active))
        .map(item => ({
            ...item,
            icon: getIcon(item.iconName)
        }));

    const drawer = (
        <div>
            {isLoading && <CircularProgress />}
            {error && <Alert severity="error">{error}</Alert>}
            <List>
                {filteredMenuItems.map((item) => (
                    <ListItem key={item.id} disablePadding>
                        <ListItemButton component={Link} to={item.path}>
                            <ListItemIcon>{item.icon}
                            </ListItemIcon>
                            <ListItemText primary={item.name} />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
            <Divider />
        </div>
    );

    return (
        <>
            {isMobile ? (
                <>
                    <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        onClick={handleDrawerToggle}
                        edge="start"
                        sx={{ mr: 2 }}
                    >
                        <MenuIcon />
                    </IconButton>
                    <Drawer
                        variant="temporary"
                        open={open}
                        onClose={handleDrawerToggle}
                        ModalProps={{
                            keepMounted: true,
                        }}
                        sx={{
                            display: { xs: 'block', sm: 'none' },
                            '& .MuiDrawer-paper': {
                                width: 240,
                            },
                        }}
                    >
                        {drawer}
                    </Drawer>
                </>
            ) : (
                <Drawer
                    variant="permanent"
                    sx={{
                        width: 240,
                        flexShrink: 0,
                        '& .MuiDrawer-paper': {
                            width: 240,
                            boxSizing: 'border-box',
                        },
                    }}
                    open
                >
                    {drawer}
                </Drawer>
            )}
        </>
    );
};

export default Sidebar;
