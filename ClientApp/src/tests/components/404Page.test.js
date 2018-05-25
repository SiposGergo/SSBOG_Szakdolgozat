import React from "react";
import { shallow } from "enzyme";
import ErrorPage from "../../components/404Page";

test('Should render 404 page correctly', () => {
    const wrapper = shallow(<ErrorPage />);
    expect(wrapper).toMatchSnapshot();
});

