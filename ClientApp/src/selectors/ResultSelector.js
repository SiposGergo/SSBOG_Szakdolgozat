
const getVisibleRegistrations =
    ({ registrations, nameText, startNumberText, gender, justFinishers, time, sortBy }) => {
        return registrations.filter(reg => {

            const nameTextResult =
                nameText == "" || reg.hiker.name.toLowerCase().includes(nameText.toLowerCase());

            const startNumberTextResult =
                startNumberText == "" || reg.startNumber.includes(startNumberText);

            let genderResult = true;
            if (gender == "female") { genderResult = (reg.hiker.gender == "Female") }
            if (gender == "male") { genderResult = (reg.hiker.gender == "Male") }

            const justFinisherResult = !justFinishers || (reg.passes[0] && reg.passes[reg.passes.length - 1].timeStamp != null)

            return nameTextResult && startNumberTextResult && genderResult && justFinisherResult;
        }).sort((a, b) => {
            if(sortBy == "name") {
                return a.hiker.name > b.hiker.name ? 1: -1;
            }
            if(sortBy == "nettoTime") {
                return a.avgSpeed < b.avgSpeed ? 1: -1;
            }
        })
    }

export default getVisibleRegistrations;