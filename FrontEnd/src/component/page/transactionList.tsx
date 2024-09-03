import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { AppDispatch, RootState } from '../../state/store';
import { fetchTransactionsDetail } from '../../state/counter/counterSlice';
import { Container, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from '@mui/material';

const TransactionList: React.FC = () => {
    const dispatch: AppDispatch = useDispatch();
    const { transactions } = useSelector((state: RootState) => state.transactions);

    useEffect(() => {
        dispatch(fetchTransactionsDetail());
    }, [dispatch]);
    console.log(transactions, 'ini transactions');
    return (
        <Container>
            <h1>Transaction List</h1>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Transaction ID</TableCell>
                            <TableCell>Transaction No</TableCell>
                            <TableCell>Category</TableCell>
                            <TableCell>Product Name</TableCell>
                            <TableCell>Qty</TableCell>
                            <TableCell>Subtotal</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {transactions.map((transaction) => (
                            <TableRow key={transaction.transactionId}>
                                <TableCell>{transaction.transactionId}</TableCell>
                                <TableCell>{transaction.transactionNo}</TableCell>
                                <TableCell>{transaction.category}</TableCell>
                                <TableCell>{transaction.productName}</TableCell>
                                <TableCell>{transaction.qty}</TableCell>
                                <TableCell>{transaction.subtotal.toFixed(2)}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
    );
};

export default TransactionList;
