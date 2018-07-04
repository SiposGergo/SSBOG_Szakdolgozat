import validator from "validator";
import moment from "moment";

const validate = (values, props) => {
    console.log(values)
    const errors = {};
    if (!values.name) {
        errors.name = 'A táv nevét kötelező megadni!';
    }

    if (!values.placeOfStart) {
        errors.placeOfStart = 'A rajt helyét kötelező megadni!';
    }

    if (!values.placeOfFinish) {
        errors.placeOfFinish = 'A cél helyét kötelező megadni!';
    }

    if (!values.distance) {
        errors.distance = 'A távot kötelező megadni!';
    }

    if (values.distance && values.distance < 500) {
        errors.distance = 'A távot méterben add meg!';
    }

    if (!values.elevation) {
        errors.elevation = 'A táv szintemelkedését kötelező megadni!';
    }

    if (!values.price) {
        errors.price = 'A táv nevezési díját kötelező megadni!';
    }

    if (!values.maxNumOfHikers) {
        errors.maxNumOfHikers = 'A nevezési létszámkorlátot kötelező megadni!';
    }

    if (!values.beginningOfStart) {
        errors.beginningOfStart = 'A rajt kezdetét kötelező megadni!';
    }

    if (!values.endOfStart) {
        errors.endOfStart = 'A rajt végét kötelező megadni!';
    }


    if ( values.beginningOfStart 
        && values.endOfStart 
        && typeof (values.endOfStart) != "string"
        && typeof (values.beginningOfStart) != "string") {
        const begginingDate = values.beginningOfStart.toDate();
        const endDate = values.endOfStart.toDate();
        if (begginingDate > endDate) {
            errors.endOfStart = 'Nem lehet vége a rajtnak mielőtt elkezdődött volna!!';
        }
    }

    if (!values.limitTime) {
        errors.limitTime = 'A szintidőt kötelező megadni!';
    }

    if (!values.registerDeadline) {
        errors.registerDeadline = 'A nevezés határidejét kötelező megadni!';
    }

    if (values.registerDeadline && typeof (values.registerDeadline) != "string") {
        const baseDateInDate = moment(props.baseDate).toDate();
        const newMoment = moment(values.registerDeadline.format());
        const registerDeadlineInDate = newMoment.add(1, "days").toDate();
        if (baseDateInDate < registerDeadlineInDate) {
            errors.registerDeadline = 'A nevezés határideje csak a túra napja előtt lehet!';
        }
    }

    if (!values.checkPoints || (values.checkPoints && values.checkPoints.length < 2)) {
        errors.checkPoints = { _error: "Legalább a rajtot és a célt meg kell adni ellenőrzőpontként!" }
    } else {
        const checkPointArrayErrors = []
        values.checkPoints.forEach((checkpoint, index) => {
            const checkPointErrors = {}

            if (!checkpoint || !checkpoint.name) {
                checkPointErrors.name = 'Az ellenőrzőpont nevét kötelező megadni!'
                checkPointArrayErrors[index] = checkPointErrors
            }

            if (index == 0 && checkpoint.distanceFromStart !=0) {
                checkPointErrors.distanceFromStart = "A start távolsága a starttól 0.";
                checkPointArrayErrors[index] = checkPointErrors
            } 

            if (!checkpoint || !checkpoint.distanceFromStart) {
                if (checkpoint.distanceFromStart === "" || ((!checkpoint.distanceFromStart) && checkpoint.distanceFromStart !== 0)) {
                    checkPointErrors.distanceFromStart = 'Az ellenőrzőpont távolságát kötelező megadni!'
                    checkPointArrayErrors[index] = checkPointErrors
                }
            }

            if (!checkpoint || !checkpoint.open) {
                checkPointErrors.open = 'Az ellenőrzőpont nyitását kötelező megadni!'
                checkPointArrayErrors[index] = checkPointErrors
            }

            if (!checkpoint || !checkpoint.close) {
                checkPointErrors.close = 'Az ellenőrzőpont zárását kötelező megadni!'
                checkPointArrayErrors[index] = checkPointErrors
            }

            if (checkpoint && checkpoint.open && checkpoint.close && typeof (checkpoint.open) != "string") {
                if (checkpoint.open.toDate() >= checkpoint.close.toDate()) {
                    checkPointErrors.close = 'Nem megfelelő sorrend!'
                    checkPointArrayErrors[index] = checkPointErrors
                }
            }

            if (checkPointArrayErrors.length) {
                errors.checkPoints = checkPointArrayErrors;
            }
        });
    }

    return errors;
};

export default validate;
