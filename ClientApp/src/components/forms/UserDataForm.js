import React from "react";
import validator from "validator";

// dátum választó
import "react-dates/initialize";
import { SingleDatePicker } from "react-dates";

const UserDataForm = (props) => {
    const registering = props.registering;
    const { user, submitted, calendarFocused } = props.state;
    const handleChange = props.handleChange;
    const onDateChange = props.onDateChange;
    const onFocusChange = props.onFocusChange;
    return (
        <div className="col-md-6 col-md-offset-3">

            {/* NÉV */}
            <div className={'form-group' + (submitted && !user.name ? ' has-error' : '')}>
                <label htmlFor="name">Név</label>
                <input type="text" className="form-control" name="name" value={user.name} onChange={handleChange} />
                {submitted && !user.name &&
                    <div className="help-block">A név megadása szükséges.</div>
                }
            </div>

            {/* Felhasználónév */}
            <div className={'form-group' + (submitted && !user.userName ? ' has-error' : '')}>
                <label htmlFor="userName">Felhasználónév</label>
                <input type="text" className="form-control" name="userName" value={user.userName} onChange={handleChange} />
                {submitted && !user.userName &&
                    <div className="help-block">A felhasználónév megadása szükséges.</div>
                }
            </div>

            {/* Születési dátum */}
            <div className={'form-group'}>
                <div>
                    <label htmlFor="dateOfBirth">Születési dátum</label>
                </div>
                <SingleDatePicker
                    isOutsideRange={() => false}
                    displayFormat="YYYY.MM.DD"
                    date={user.dateOfBirth} // momentPropTypes.momentObj or null
                    onDateChange={date => onDateChange(date)} // PropTypes.func.isRequired
                    focused={calendarFocused} // PropTypes.bool
                    onFocusChange={({ focused }) => onFocusChange(focused)} // PropTypes.func.isRequired
                    numberOfMonths={1}
                />
            </div>

            {/* település */}
            <div className={'form-group' + (submitted && !user.town ? ' has-error' : '')}>
                <label htmlFor="town">Település</label>
                <input type="text" className="form-control" name="town" value={user.town} onChange={handleChange} />
                {submitted && !user.town &&
                    <div className="help-block">A település megadása szükséges.</div>
                }
            </div>

            {/* telefonszám + validator */}
            <div className={'form-group' + (submitted && !user.phoneNumber ||
                (submitted && user.phoneNumber && !validator.isMobilePhone(user.phoneNumber, "hu-HU")) ? ' has-error' : '')}>
                <label htmlFor="phoneNumber">Telefonszám</label>
                <input type="text" className="form-control" name="phoneNumber" value={user.phoneNumber} onChange={handleChange}
                    placeholder="Pl.: +36301234567" />
                {submitted && !user.phoneNumber &&
                    <div className="help-block">A telefonszám megadása szükséges, hogy a rendezők veszély esetén el bírjanak érni a túra folyamán.</div>
                }
                {submitted && user.phoneNumber && !validator.isMobilePhone(user.phoneNumber, "hu-HU") &&
                    <div className="help-block">Érvényes telefonszámot adj meg.</div>
                }
            </div>

            {/* jelszó */}
            <div className={'form-group' + (submitted && !user.password ? ' has-error' : '')}>
                <label htmlFor="password">Jelszó</label>
                <input type="password" className="form-control" name="password" value={user.password} onChange={handleChange} />
                {submitted && !user.password &&
                    <div className="help-block">A jelszó megadása szükséges.</div>
                }
            </div>

            {/* email + validator */}
            <div className={'form-group' + (submitted && !user.email || (submitted && user.email && !validator.isEmail(user.email)) ?
                ' has-error' : '')}>
                <label htmlFor="email">E-mail cím</label>
                <input type="text" className="form-control" name="email" value={user.email} onChange={handleChange} />
                {submitted && !user.email &&
                    <div className="help-block">Az email cím megadása szükséges.</div>
                }
                {submitted && user.email && !validator.isEmail(user.email) &&
                    <div className="help-block">Érvényes e-mail címet adj meg!.</div>
                }
            </div>

            {/* NEM */}
            <div className={'form-group' + (submitted && !user.gender ? ' has-error' : '')}>
                <label htmlFor="gender">Nem</label>
                <div>
                    <select value={user.gender} onChange={handleChange} name="gender">
                        <option value="Male">Férfi</option>
                        <option value="Female">Nő</option>
                    </select>
                </div>
                {submitted && !user.gender &&
                    <div className="help-block">A nemed megadása kötelező.</div>
                }
            </div>
        </div>
    )
};

export default UserDataForm;