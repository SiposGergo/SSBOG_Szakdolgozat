import getVisibleHikes from "../../selectors/HikeListSelector";
import testHikes from "./testHikes";
import moment from "moment";

test("Selector should return all hikes, by date", () => {
    const input = {
        hikes: testHikes,
        text: "",
        sortBy: "date",
        startDate: null,
        endDate: null,
        isOldHikesVisible: true,
        slider: [0, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([testHikes[1], testHikes[0], testHikes[2]]);
});

test("Selector should return all hikes, by name", () => {
    const input = {
        hikes: testHikes,
        text: "",
        sortBy: "name",
        startDate: null,
        endDate: null,
        isOldHikesVisible: true,
        slider: [0, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([testHikes[2], testHikes[0], testHikes[1]]);
});

test("Selector should return only Mátrahegy by search text", () => {
    const input = {
        hikes: testHikes,
        text: "Mátrahegy",
        sortBy: "date",
        startDate: null,
        endDate: null,
        isOldHikesVisible: true,
        slider: [0, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([testHikes[0]]);
});

test("Selector should return only Mátrahegy and Patai Mátra by search text, sorted by name", () => {
    const input = {
        hikes: testHikes,
        text: "Mátra",
        sortBy: "name",
        startDate: null,
        endDate: null,
        isOldHikesVisible: true,
        slider: [0, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([testHikes[0], testHikes[1]]);
});

test("Selector should return only Patai Mátra by distynce, set on slider", () => {
    const input = {
        hikes: testHikes,
        text: "",
        sortBy: "name",
        startDate: null,
        endDate: null,
        isOldHikesVisible: true,
        slider: [50, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([testHikes[1]]);
});

test("Selector should return only Mátrahegy and Patai Mátra by search text, sorted by name", () => {
    const input = {
        hikes: testHikes,
        text: "Mátra",
        sortBy: "name",
        startDate: null,
        endDate: null,
        isOldHikesVisible: true,
        slider: [0, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([testHikes[0], testHikes[1]]);
});

test("Selector should return only Andezit, by date", () => {
    const input = {
        hikes: testHikes,
        text: "",
        sortBy: "name",
        startDate: moment("2014-01-01"),
        endDate: moment("2014-10-01"),
        isOldHikesVisible: true,
        slider: [0, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([testHikes[2]]);
});

test("Selector should return no hike #1", () => {
    const input = {
        hikes: testHikes,
        text: "nincs ilyen",
        sortBy: "name",
        startDate: moment("2010-01-01"),
        endDate: moment("2018-10-01"),
        isOldHikesVisible: true,
        slider: [0, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([]);
});

test("Selector should return no hike #2", () => {
    const input = {
        hikes: testHikes,
        text: "",
        sortBy: "name",
        startDate: moment("2019-01-01"),
        endDate: moment("2019-10-01"),
        isOldHikesVisible: true,
        slider: [0, 100],
    }
    const result = getVisibleHikes(input);
    expect(result).toEqual([]);
});