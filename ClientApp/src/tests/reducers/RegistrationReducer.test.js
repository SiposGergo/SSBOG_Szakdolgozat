import {registration} from "../../reducers/RegistrationReducer"

test("should set default state", () => {
    const state = registration(undefined, { type: "@@INIT" });
    expect(state).toEqual({});
})

test("should set registering flag when request", () => {
    const state = registration(undefined, { type: "USERS_REGISTER_REQUEST" });
    expect(state).toEqual({registering:true});
})

test("when register success", () => {
    const state = registration(undefined, { type: "USERS_REGISTER_SUCCESS" });
    expect(state).toEqual({});
})

test("when register fails", () => {
    const state = registration(undefined, { type: "USERS_REGISTER_FAILURE" });
    expect(state).toEqual({});
})