let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? { loggedIn: true, user } : {};

export const authentication = (state = initialState, action) => {
  switch (action.type) {
    case 'USERS_LOGIN_REQUEST':
      return {
        loggingIn: true
      };

    case 'USERS_LOGIN_SUCCESS':
      return {
        loggedIn: true,
        user: action.user
      };

    case 'USERS_LOGIN_FAILURE':
      return {
        loggedIn: false
      };

    case 'USERS_LOGOUT':
      return {
        loggedIn: false,
        user: null
      };

    case "USER_UPDATE_SUCCESS":
      return {
        loggedIn: true,
        user : {...action.user , password:""}
      };
    case "HIKE_DETAILS_LOAD_NEW_REGISTRATION":
    console.log(action.registration);
      return {
          ...state,
          user: {...state.user, registrations: [...state.user.registrations, action.registration]}
      }
      case "HIKE_DETAILS_DELETE_OLD_REGISTRATION":
      const registrations = state.user.registrations; 
      const newRegistrations = registrations.filter(reg=> reg.id != action.idToDelete);
      return {
        ...state,
          user: {...state.user, registrations: newRegistrations }
      }
    default:
      return state
  }
}