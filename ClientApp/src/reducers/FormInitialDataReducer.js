const LOAD = 'UserForm/LOAD';
const UNLOAD = "UserForm/UNLOAD"

const reducer = (state = {}, action) => {
  switch (action.type) {

    case LOAD:
      return {
        data: action.data,
      };

    case UNLOAD:
      return {
        data: null
      };

    default:
      return state;
  }
};

export const load = data => ({ type: LOAD, data });
export const unload = () => ({ type: UNLOAD })

export default reducer;