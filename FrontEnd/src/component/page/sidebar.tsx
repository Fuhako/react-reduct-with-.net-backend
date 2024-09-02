import React from "react";
import { Link } from "react-router-dom";
import { Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, IconButton, Divider } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import HomeIcon from "@mui/icons-material/Home";
import CategoryIcon from "@mui/icons-material/Category";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useMediaQuery, useTheme } from "@mui/material";

const Sidebar: React.FC = () => {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down("sm"));

    const [open, setOpen] = React.useState(false);

    const handleDrawerToggle = () => {
        setOpen(!open);
    };

    const drawer = (
        <div>
            <List>
                <ListItem disablePadding>
                    <ListItemButton component={Link} to="/">
                        <ListItemIcon><HomeIcon /></ListItemIcon>
                        <ListItemText primary="Home" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton component={Link} to="/products">
                        <ListItemIcon><CategoryIcon /></ListItemIcon>
                        <ListItemText primary="Products" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton component={Link} to="/product-category">
                        <ListItemIcon><CategoryIcon /></ListItemIcon>
                        <ListItemText primary="Product Categories" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton component={Link} to="/product-variant">
                        <ListItemIcon><CategoryIcon /></ListItemIcon>
                        <ListItemText primary="Product Variants" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton component={Link} to="/profile">
                        <ListItemIcon><AccountCircleIcon /></ListItemIcon>
                        <ListItemText primary="Profile" />
                    </ListItemButton>
                </ListItem>
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
