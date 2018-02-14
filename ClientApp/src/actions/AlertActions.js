export const alertActions = {
    success,
    error,
    clear
};

const success = (message) => ({ type: 'ALERT_SUCCESS', message });

const error = (message) => ({ type: 'ALERT_ERROR', message });

const clear = () => ({ type: 'ALERT_CLEAR' });
