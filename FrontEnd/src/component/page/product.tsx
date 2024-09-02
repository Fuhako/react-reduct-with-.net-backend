import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { RootState, AppDispatch } from "../../state/store";
import {
    fetchProducts,
    addProduct,
    updateProduct,
    deleteProduct
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
    TextField
} from "@mui/material";
import { useFormik } from "formik";
import { Product } from "../../state/counter/counterSlice";

const ProductComponent: React.FC = () => {
    const dispatch: AppDispatch = useDispatch();
    const { products, loading, error } = useSelector((state: RootState) => state.products);

    const [open, setOpen] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);

    useEffect(() => {
        dispatch(fetchProducts());
    }, [dispatch]);

    const formik = useFormik({
        initialValues: {
            id: 0,
            plu: '',
            product_category_id: 0,
            created_user: ''
        },
        onSubmit: async (values) => {
            var payload = {
                id: values.id,
                plu: values.plu,
                product_category_id: values.product_category_id,
                created_user: values.created_user // Pastikan ini diisi dengan nilai yang sesuai
            }
            try {
                if (selectedProduct) {
                    await dispatch(updateProduct(payload)).unwrap();
                } else {
                    await dispatch(addProduct(payload)).unwrap();
                }
                // Fetch the updated product list after adding or updating
                dispatch(fetchProducts());
                handleClose();
            } catch (error) {
                console.error("Failed to save product:", error);
                // Handle the error if necessary
            }
        },
    });

    const handleOpen = (product: Product | null = null) => {
        setSelectedProduct(product);
        if (product) {
            formik.setValues(product);
        } else {
            formik.resetForm();
        }
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        setSelectedProduct(null);
        formik.resetForm();
    };

    return (
        <Container maxWidth="md">
            <Typography variant="h4" gutterBottom>
                Product List
            </Typography>
            <Button variant="contained" color="primary" onClick={() => handleOpen()}>
                Add Product
            </Button>
            <Paper elevation={3} style={{ marginTop: "16px" }}>
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>ID</TableCell>
                                <TableCell>PLU</TableCell>
                                <TableCell>Category ID</TableCell>
                                <TableCell>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {products.map((product) => (
                                <TableRow key={product.id}>
                                    <TableCell>{product.id}</TableCell>
                                    <TableCell>{product.plu}</TableCell>
                                    <TableCell>{product.product_category_id}</TableCell>
                                    <TableCell>
                                        <Button
                                            variant="outlined"
                                            color="primary"
                                            onClick={() => handleOpen(product)}
                                        >
                                            Edit
                                        </Button>
                                        <Button
                                            variant="outlined"
                                            color="secondary"
                                            onClick={() => dispatch(deleteProduct(product))}
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
                <DialogTitle>{selectedProduct ? "Edit Product" : "Add Product"}</DialogTitle>
                <form onSubmit={formik.handleSubmit}>
                    <DialogContent>
                        <TextField
                            margin="dense"
                            id="plu"
                            label="PLU"
                            type="text"
                            fullWidth
                            {...formik.getFieldProps('plu')}
                        />
                        <TextField
                            margin="dense"
                            id="product_category_id"
                            label="Category ID"
                            type="number"
                            fullWidth
                            {...formik.getFieldProps('product_category_id')}
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose} color="primary">
                            Cancel
                        </Button>
                        <Button type="submit" color="primary">
                            {selectedProduct ? "Update" : "Add"}
                        </Button>
                    </DialogActions>
                </form>
            </Dialog>
        </Container>
    );
};

export default ProductComponent;
