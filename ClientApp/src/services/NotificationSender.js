import { reducer as notifReducer, actions as notifActions, Notifs } from 'redux-notifications';
const { notifSend } = notifActions;

export const SendDanger = (notifText) => {
    return notifSend(
        {
            message: notifText,
            kind: 'danger',
            dismissAfter: 3000
        }
    )
};

export const SendSuccess = (notifText) => {
    return notifSend(
        {
            message: notifText,
            kind: 'success',
            dismissAfter: 3000
        }
    )
};

export const SendWarning = (notifText) => {
    return notifSend(
        {
            message: notifText,
            kind: 'warning',
            dismissAfter: 5000
        }
    )
};