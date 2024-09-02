import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { RootState, AppDispatch } from "../../state/store";
import {
    fetchProductVariant,
    addProductVariant,
    updateProductVariant,
    deleteProductVariant,
    toggleActiveVariant
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
import { ProductVariant, login } from "../../state/counter/counterSlice";
import LoadingPage from "./loading"; // Import LoadingPage component

const ProductVariantComponent: React.FC = () => {
    const dispatch: AppDispatch = useDispatch();
    const { productVariant, isLoading, error } = useSelector((state: RootState) => state.productVariant);

    const [open, setOpen] = useState<boolean>(false);
    const [selectedProductVariant, setSelectedProductVariant] = useState<ProductVariant | null>(null);
    const userId = useSelector((state: RootState) => state.login.name);

    useEffect(() => {
        dispatch(fetchProductVariant());
    }, [dispatch]);

    const formik = useFormik({
        initialValues: {
            id: 0,
            product_id: 0,
            code: '',
            name: '',
            qty: 0,
            price: 0,
            active: true,
            created_user: ''
        },
        onSubmit: async (values) => {
            const payloadVariant: ProductVariant = {
                id: values.id,
                product_id: values.product_id,
                code: values.code,
                name: values.name,
                qty: values.qty,
                price: values.price,
                active: values.active,
                created_user: userId
            };
            try {
                if (selectedProductVariant) {
                    await dispatch(updateProductVariant(payloadVariant)).unwrap();
                } else {
                    await dispatch(addProductVariant(payloadVariant)).unwrap();
                }
                dispatch(fetchProductVariant());
                handleClose();
            } catch (error) {
                console.error("Failed to save ProductVariant:", error);
            }
        },
    });

    const handleOpen = (productVariant: ProductVariant | null = null) => {
        setSelectedProductVariant(productVariant);
        if (productVariant) {
            formik.setValues(productVariant);
        } else {
            formik.resetForm();
        }
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        setSelectedProductVariant(null);
        formik.resetForm();
    };

    const handleToggleActiveVariant = (VariantId: number) => {
        dispatch(toggleActiveVariant(VariantId));
    };

    return (
        <Container maxWidth="md">
            {isLoading && <LoadingPage />}

            <Typography variant="h4" gutterBottom>
                Product Variant List
            </Typography>
            <Button variant="contained" color="primary" onClick={() => handleOpen()}>
                Add Product Variant
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
                            {productVariant.map((Variant) => (
                                <TableRow key={Variant.id}>
                                    <TableCell>{Variant.id}</TableCell>
                                    <TableCell>{Variant.name}</TableCell>
                                    <TableCell>{Variant.active ? 'Yes' : 'No'}</TableCell>
                                    <TableCell>
                                        <Button
                                            variant="outlined"
                                            color="primary"
                                            onClick={() => handleOpen(Variant)}
                                        >
                                            Edit
                                        </Button>
                                        <Button
                                            variant="outlined"
                                            color="secondary"
                                            onClick={() => dispatch(deleteProductVariant(Variant.id))}
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
                <DialogTitle>{selectedProductVariant ? "Edit ProductVariant" : "Add ProductVariant"}</DialogTitle>
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
                            {selectedProductVariant ? "Update" : "Add"}
                        </Button>
                    </DialogActions>
                </form>
            </Dialog>
        </Container>
    );
};

export default ProductVariantComponent;
