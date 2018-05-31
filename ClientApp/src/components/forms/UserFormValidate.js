import validator from "validator";

export const validate = values => {
    const errors = {}

    if (!values.userName) {
        errors.userName = 'Felhasználónév megadása szükséges.'
    }

    if (!values.name) {
        errors.name = 'Név megadása szükséges.'
    }

    if (!values.email) {
        errors.email = 'E-mail cím megadása szükséges.'
    } else if (!validator.isEmail(values.email)) {
        errors.email = 'Érvénytelen e-mail cím.'
    }

    if (!values.town) {
        errors.town = 'Település megadása szükséges, a lakhelyed csak az eredménylistákban fog szerepelni.'
    }

    if (!values.phoneNumber) {
        errors.phoneNumber = 'Telefonszám megadása szükséges, hogy a túra szervezők veszélyhelyzetben elérhessenek.'
    } else if (!validator.isMobilePhone(values.phoneNumber, "hu-HU")) {
        errors.phoneNumber = "Érvénytelen telefonszám, helyes formátum: +36301234567"
    }

    if (!values.gender) {
        errors.gender = 'Nem megadása szükséges.'
    }

    if (!values.password) {
        errors.password = 'Jelszó megadása szükséges.'
    }

    if (!values.dateOfBirth) {
        errors.dateOfBirth = 'Születési dátum megadása szükséges.'
    }

    return errors
}