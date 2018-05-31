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
      const userWithNewRegistration = {...state.user, registrations: [...state.user.registrations, action.registration]};
      localStorage.setItem("user", JSON.stringify(userWithNewRegistration))
      return {
          ...state,
          user :userWithNewRegistration
      }
      
      case "HIKE_DETAILS_DELETE_OLD_REGISTRATION":
      const registrations = state.user.registrations; 
      const newRegistrations = registrations.filter(reg=> reg.id != action.idToDelete);
      const userWithRemovedOldRegistration = {...state.user, registrations: newRegistrations };
      localStorage.setItem("user", JSON.stringify(userWithRemovedOldRegistration))
      return {
        ...state,
          user :userWithRemovedOldRegistration
      }
  
      default:
      return state
  }
}