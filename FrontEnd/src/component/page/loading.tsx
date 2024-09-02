import React from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import { useSelector } from 'react-redux';
import { RootState } from '../../state/store';

const LoadingPage: React.FC = () => {
    const isLoading = useSelector((state: RootState) => state.loading.isLoading);

    if (!isLoading) return null;

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh',
                position: 'fixed',
                top: 0,
                left: 0,
                right: 0,
                bottom: 0,
                backgroundColor: 'rgba(255, 255, 255, 0.8)', // background semi-transparent
                zIndex: 1300, // Ensure it's above other components
            }}
        >
            <CircularProgress />
        </Box>
    );
};

export default LoadingPage;
