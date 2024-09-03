// components/ManageTransactions.tsx
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Container, Table, TableBody, TableCell, TableHead, TableRow, Typography, Paper } from '@mui/material';
import { RootState, AppDispatch } from '../../state/store';
import { fetchTransactions } from '../../state/counter/counterSlice';

const ManageTransactions: React.FC = () => {
    const dispatch = useDispatch<AppDispatch>();
    const { manageTransactions, isLoading, error } = useSelector((state: RootState) => state.manageTransactions);

    useEffect(() => {
        dispatch(fetchTransactions());
    }, [dispatch]);

    return (
        <Container>
            <Typography variant="h4" gutterBottom>
                Manage Transactions
            </Typography>
            <Paper>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>transaction_no</TableCell>
                            <TableCell>Date</TableCell>
                            <TableCell>Amount</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {manageTransactions.map((transaction) => (
                            <TableRow key={transaction.id}>
                                <TableCell>{transaction.id}</TableCell>
                                <TableCell>{transaction.transaction_no}</TableCell>
                                <TableCell>{transaction.created_date}</TableCell>
                                <TableCell>{transaction.total_amount.toFixed(2)}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </Paper>
        </Container>
    );
};

export default ManageTransactions;
