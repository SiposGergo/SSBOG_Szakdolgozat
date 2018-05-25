import React from "react";
import { shallow } from "enzyme";
import { HikeListPage } from "../../components/HikeList/HikeListPage";
import { input } from "../fixtures/testHikes";


test('HikeListPage should render correctly', () => {
    const wrapper = shallow(<HikeListPage {...input} fetchData={() => { }} />);
    expect(wrapper).toMatchSnapshot();
});

test('HikeListPage should render correctly while loading hikes', () => {
    const wrapper = shallow(<HikeListPage {...input} isLoading={true} fetchData={() => { }} />);
    expect(wrapper).toMatchSnapshot();
});

test('HikeListPage should render correctly with errors', () => {
    const wrapper = shallow(<HikeListPage {...input} hasErrored={true} fetchData={() => { }} />);
    expect(wrapper).toMatchSnapshot();
});