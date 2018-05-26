import { authentication } from "../../reducers/AuthenticationReducer";

test("should set default state", () => {
    const state = authentication(undefined, { type: "@@INIT" });
    expect(state).toEqual({});
})

test("should set logging in when request in", () => {
    const state = authentication(undefined, { type: "USERS_LOGIN_REQUEST" });
    expect(state).toEqual({ loggingIn: true });
})

test("should set user and loggedn when success", () => {
    const state = authentication(undefined, { type: "USERS_LOGIN_SUCCESS", user: {} });
    expect(state).toEqual({ loggedIn: true, user: {} });
})

test("should set user and loggedn when user data update success", () => {
    const state = authentication(undefined, { type: "USER_UPDATE_SUCCESS", user: {} });
    expect(state).toEqual({ loggedIn: true, user: {password:""} });
})

test("should set user and loggedn after logout", () => {
    const state = authentication(undefined, { type: "USERS_LOGOUT", });
    expect(state).toEqual({ loggedIn: false, user: null });
})

test("should return {} when login fails", () => {
    const state = authentication(undefined, { type: "USERS_LOGIN_FAILURE", });
    expect(state).toEqual({ loggedIn: false });
})

