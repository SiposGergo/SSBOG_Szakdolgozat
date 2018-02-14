import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import moment from "moment";
import { userActions } from '../actions/UserActions';
// dátum választó
import "react-dates/initialize";
import { SingleDatePicker } from "react-dates";
// validáció
import validator from 'validator';


class RegisterPage extends React.Component {

    state = {
        user: {
            name: '',
            userName: '',
            password: '',
            email: '',
            gender: 'Male',
            phoneNumber: '',
            town: "",
            dateOfBirth: moment().startOf('year')
        },
        submitted: false,
        calendarFocused: false
    };

    handleChange = (event) => {
        const { name, value } = event.target;
        const { user } = this.state;
        console.log(value);
        this.setState({
            user: {
                ...user,
                [name]: value
            }
        });
    }

    checkProperties = (obj) => {
        for (var key in obj) {
            if (!obj[key])
                return false;
        }
        return true;
    }

    handleSubmit = (event) => {
        event.preventDefault();

        this.setState({ submitted: true });
        const { user } = this.state;
        const { dispatch } = this.props;
        if (this.checkProperties(user)) {
            dispatch(userActions.register(user, this.props.history));
        }
    }

    render() {
        const { registering } = this.props;
        const { user, submitted } = this.state;
        return (
            <div className="col-md-6 col-md-offset-3">
                <h2>Regisztráció</h2>
                <form name="form" onSubmit={this.handleSubmit}>

                    {/* NÉV */}
                    <div className={'form-group' + (submitted && !user.name ? ' has-error' : '')}>
                        <label htmlFor="name">Név</label>
                        <input type="text" className="form-control" name="name" value={user.name} onChange={this.handleChange} />
                        {submitted && !user.name &&
                            <div className="help-block">A név megadása szükséges.</div>
                        }
                    </div>

                    {/* Felhasználónév */}
                    <div className={'form-group' + (submitted && !user.userName ? ' has-error' : '')}>
                        <label htmlFor="userName">Felhasználónév</label>
                        <input type="text" className="form-control" name="userName" value={user.userName} onChange={this.handleChange} />
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
                            onDateChange={date => this.setState({ user: { ...user, dateOfBirth: date } })} // PropTypes.func.isRequired
                            focused={this.state.calendarFocused} // PropTypes.bool
                            onFocusChange={({ focused }) => this.setState({ calendarFocused: focused })} // PropTypes.func.isRequired
                            numberOfMonths={1}
                        />
                    </div>

                    {/* település */}
                    <div className={'form-group' + (submitted && !user.town ? ' has-error' : '')}>
                        <label htmlFor="town">Település</label>
                        <input type="text" className="form-control" name="town" value={user.town} onChange={this.handleChange} />
                        {submitted && !user.town &&
                            <div className="help-block">A település megadása szükséges.</div>
                        }
                    </div>

                    {/* telefonszám + validator */}
                    <div className={'form-group' + (submitted && !user.phoneNumber ||
                        (submitted && user.phoneNumber && !validator.isMobilePhone(user.phoneNumber, "hu-HU")) ? ' has-error' : '')}>
                        <label htmlFor="phoneNumber">Telefonszám</label>
                        <input type="text" className="form-control" name="phoneNumber" value={user.phoneNumber} onChange={this.handleChange}
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
                        <input type="password" className="form-control" name="password" value={user.password} onChange={this.handleChange} />
                        {submitted && !user.password &&
                            <div className="help-block">A jelszó megadása szükséges.</div>
                        }
                    </div>

                    {/* email + validator */}
                    <div className={'form-group' + (submitted && !user.email || (submitted && user.email && !validator.isEmail(user.email)) ?
                        ' has-error' : '')}>
                        <label htmlFor="email">E-mail cím</label>
                        <input type="text" className="form-control" name="email" value={user.email} onChange={this.handleChange} />
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
                            <select value={user.gender} onChange={this.handleChange} name="gender">
                                <option value="Male">Férfi</option>
                                <option value="Female">Nő</option>
                            </select>
                        </div>
                        {submitted && !user.gender &&
                            <div className="help-block">A nemed megadása kötelező.</div>
                        }
                    </div>

                    <div className="form-group">
                        <button className="btn btn-primary">Regisztráció</button>
                        {registering &&
                            <img src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
                        }
                        <Link to="/login" className="btn btn-link">Mégse</Link>
                    </div>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { registering } = state.registration;
    return {
        registering
    };
}

const connectedRegisterPage = connect(mapStateToProps)(RegisterPage);
export { connectedRegisterPage as RegisterPage };