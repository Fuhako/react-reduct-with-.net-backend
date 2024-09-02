import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { RootState, AppDispatch } from "../../state/store";
import {
    fetchProductCategory,
    addProductCategory,
    updateProductCategory,
    deleteProductCategory,
    toggleActiveCategory
} from "../../state/counter/counterSlice";
import {
    Container,
    Typography,
    Paper,
    Button,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
    Switch
} from "@mui/material";
import { useFormik } from "formik";
import { ProductCategory, login } from "../../state/counter/counterSlice";
import LoadingPage from "./loading"; // Import LoadingPage component

const ProductCategoryComponent: React.FC = () => {
    const dispatch: AppDispatch = useDispatch();
    const { productCategory, isLoading, error } = useSelector((state: RootState) => state.productCategory);

    const [open, setOpen] = useState<boolean>(false);
    const [selectedProductCategory, setSelectedProductCategory] = useState<ProductCategory | null>(null);
    const userId = useSelector((state: RootState) => state.login.name);

    useEffect(() => {
        dispatch(fetchProductCategory());
    }, [dispatch]);

    const formik = useFormik({
        initialValues: {
            id: 0,
            name: '',
            active: true,
            created_user: ''
        },
        onSubmit: async (values) => {
            const payloadCategory: ProductCategory = {
                id: values.id,
                name: values.name,
                active: values.active,
                created_user: userId
            };
            try {
                if (selectedProductCategory) {
                    await dispatch(updateProductCategory(payloadCategory)).unwrap();
                } else {
                    await dispatch(addProductCategory(payloadCategory)).unwrap();
                }
                dispatch(fetchProductCategory());
                handleClose();
            } catch (error) {
                console.error("Failed to save ProductCategory:", error);
            }
        },
    });

    const handleOpen = (productCategory: ProductCategory | null = null) => {
        setSelectedProductCategory(productCategory);
        if (productCategory) {
            formik.setValues(productCategory);
        } else {
            formik.resetForm();
        }
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        setSelectedProductCategory(null);
        formik.resetForm();
    };

    const handleToggleActiveCategory = (categoryId: number) => {
        dispatch(toggleActiveCategory(categoryId));
    };

    return (
        <Container maxWidth="md">
            {isLoading && <LoadingPage />}

            <Typography variant="h4" gutterBottom>
                Product Category List
            </Typography>
            <Button variant="contained" color="primary" onClick={() => handleOpen()}>
                Add Product Category
            </Button>
            <Paper elevation={3} style={{ marginTop: "16px" }}>
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>ID</TableCell>
                                <TableCell>Name</TableCell>
                                <TableCell>Active</TableCell>
                                <TableCell>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {productCategory.map((category) => (
                                <TableRow key={category.id}>
                                    <TableCell>{category.id}</TableCell>
                                    <TableCell>{category.name}</TableCell>
                                    <TableCell>{category.active ? 'Yes' : 'No'}</TableCell>
                                    <TableCell>
                                        <Button
                                            variant="outlined"
                                            color="primary"
                                            onClick={() => handleOpen(category)}
                                        >
                                            Edit
                                        </Button>
                                        <Button
                                            variant="outlined"
                                            color="secondary"
                                            onClick={() => dispatch(deleteProductCategory(category.id))}
                                            style={{ marginLeft: 8 }}
                                        >
                                            Delete
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Paper>

            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>{selectedProductCategory ? "Edit ProductCategory" : "Add ProductCategory"}</DialogTitle>
                <form onSubmit={formik.handleSubmit}>
                    <DialogContent>
                        <TextField
                            margin="dense"
                            id="name"
                            label="Name"
                            type="text"
                            fullWidth
                            {...formik.getFieldProps('name')}
                        />
                        <Switch
                            checked={formik.values.active}
                            onChange={(e) => formik.setFieldValue('active', e.target.checked)}
                            name="active"
                            color="primary"
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose} color="primary">
                            Cancel
                        </Button>
                        <Button type="submit" color="primary">
                            {selectedProductCategory ? "Update" : "Add"}
                        </Button>
                    </DialogActions>
                </form>
            </Dialog>
        </Container>
    );
};

export default ProductCategoryComponent;
