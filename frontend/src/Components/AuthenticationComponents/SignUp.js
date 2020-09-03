import React, { Component } from "react";
import { Link } from "react-router-dom";
import { newUserUrl } from "../../const.js"
import history from '../../History.js';
import axios from 'axios';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

toast.configure();

export default class SignUp extends Component {
    constructor(props) {
        super(props);
        this.state = {
            firstName: "",
            lastName: "",
            email: "",
            password: ""
        }
    }

    handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;
        this.setState({
            [name]: value
        });
    }

    handleSubmit = () => {
        this.createNewUser();
        history.push('/sign-in')
    }

    createNewUser = () => {
        let postData = {
            FirstName: this.state.firstName,
            LastName: this.state.lastName,
            Email: this.state.email,
            Password: this.state.password
        }

        axios.post(newUserUrl, postData)
            .catch(() => {
                toast.error("There is a problem with creating a new user", { position: toast.POSITION.TOP_CENTER });
            })
    }

    render() {
        return (
            <form>
                <h3>Sign up</h3>

                <div className="form-group">
                    <label>First Name</label>
                    <input name="firstName"
                        type="text"
                        className="form-control"
                        placeholder="Enter your first name"
                        value={this.state.firstName}
                        onChange={this.handleChange} />
                </div>

                <div className="form-group">
                    <label>Last Name</label>
                    <input name="lastName"
                        type="text"
                        className="form-control"
                        placeholder="Enter your last name"
                        value={this.state.lastName}
                        onChange={this.handleChange} />
                </div>

                <div className="form-group">
                    <label>Email address</label>
                    <input name="email"
                        type="email"
                        className="form-control"
                        placeholder="you@example.com"
                        value={this.state.email}
                        onChange={this.handleChange} />
                </div>

                <div className="form-group">
                    <label>Password</label>
                    <input name="password"
                        type="password"
                        className="form-control"
                        placeholder="Enter your password"
                        value={this.state.password}
                        onChange={this.handleChange} />
                </div>
                <div>
                    <button type="submit"
                        className="btn btn-primary btn-block"
                        onClick={this.handleSubmit}>
                        Sign up
                    </button>
                </div>

                <p className="forgot-password text-right"  >
                    Already registered?
                    <Link className="nav-link" to={"/sign-in"}>Sign in</Link>
                </p>
            </form>
        );
    }
}