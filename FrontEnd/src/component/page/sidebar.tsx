import React from "react";
import { Link } from "react-router-dom";
import { Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, IconButton, Divider } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import HomeIcon from "@mui/icons-material/Home";
import CategoryIcon from "@mui/icons-material/Category";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useMediaQuery, useTheme } from "@mui/material";
import { useSelector } from "react-redux";
import { RootState } from '../../state/store';

const Sidebar: React.FC = () => {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down("sm"));
    const [open, setOpen] = React.useState(false);

    const menuAccess = useSelector((state: RootState) => state.login.menuAccessByRoleId);

    const handleDrawerToggle = () => {
        setOpen(!open);
    };

    const menuItems = [
        { id: 1, text: "Home", icon: <HomeIcon />, path: "/" },
        { id: 2, text: "Products", icon: <CategoryIcon />, path: "/products" },
        { id: 3, text: "Product Categories", icon: <CategoryIcon />, path: "/product-category" },
        { id: 4, text: "Product Variants", icon: <CategoryIcon />, path: "/product-variant" },
        { id: 5, text: "Profile", icon: <AccountCircleIcon />, path: "/profile" },
    ];

    const filteredMenuItems = menuItems.filter((item) =>
        menuAccess.some((access) => access.menu_id === item.id && access.active)
    );
    console.log(menuAccess, 'filteredMenuItems');

    const drawer = (
        <div>
            <List>
                {filteredMenuItems.map((item) => (
                    <ListItem key={item.id} disablePadding>
                        <ListItemButton component={Link} to={item.path}>
                            <ListItemIcon>{item.icon}</ListItemIcon>
                            <ListItemText primary={item.text} />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
            <Divider />
            {/* Add more items if needed */}
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
                            keepMounted: true, // Better open performance on mobile.
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
