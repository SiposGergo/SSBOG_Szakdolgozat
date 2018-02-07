import moment from "moment";

const getVisibleHikes = ({ hikes, text, sortBy, startDate, endDate, isOldHikesVisible, slider }) => {
    return hikes.filter((hike) => {
        const createdAtMoment = moment(hike.date);
        const startDateMatch = startDate ? startDate.isSameOrBefore(createdAtMoment, "day") : true;
        const endDateMatch = endDate ? endDate.isSameOrAfter(createdAtMoment, "day") : true;
        const TextMatch = text === "" || (hike.name.toLowerCase().includes(text.toLowerCase()));
        const visbleByCheckBox = isOldHikesVisible ||
            !isOldHikesVisible && createdAtMoment.isSameOrAfter(moment.now())
        const distancesBySlider = 
            hike.courses.some((course) => { 
                return ((course.distance / 1000) >= slider[0] && (course.distance / 1000) <= slider[1])
            })

        return startDateMatch && endDateMatch && TextMatch && visbleByCheckBox && distancesBySlider;
    }).sort((a, b) => {
        if (sortBy === "date") {
            return a.date < b.date ? 1 : -1;
        }
        else if (sortBy === "name") {
            return a.name > b.name ? 1 : -1;
        }
    });
}

export default getVisibleHikes;