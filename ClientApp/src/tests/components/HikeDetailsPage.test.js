import React from "react";
import { shallow } from "enzyme";
import {HikeDetailsPage} from "../../components/HikeDetails/HikeDetailsPage";
import {testHikeDetails} from "../fixtures/testHikeDetails"

test('Should render HikeDetails page when error', () => {
    const wrapper = shallow(<HikeDetailsPage 
        hasErrored={true} 
        dispatch={()=>{}}
        match={{params:{id:2}}} 
        />);
    expect(wrapper).toMatchSnapshot();
});

test('Should render HikeDetails page when loading', () => {
    const wrapper = shallow(<HikeDetailsPage 
        isLoading={true} 
        dispatch={()=>{}}
        match={{params:{id:2}}} 
        />);
    expect(wrapper).toMatchSnapshot();
});

test('Should render HikeDetails page with test hike', () => {
    const wrapper = shallow(<HikeDetailsPage 
        hike={testHikeDetails} 
        dispatch={()=>{}}
        match={{params:{id:2}}} 
        />);
    expect(wrapper).toMatchSnapshot();
});