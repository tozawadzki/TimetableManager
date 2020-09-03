import axios from 'axios';
import history from '../../History.js';
import React, { Component } from "react";
import { loginUrl } from "../../const.js"
import { IsUserSignedIn, setAuthorizationToken, jwtToLocalStorage } from "../../Helpers";
import { toast } from "react-toastify";

export default class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            success: false,
            token: "",
        };
    }

    handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;
        this.setState({
            [name]: value
        });
    }

    handleSubmit = (e) => {
        e.preventDefault();
        this.handleSigningIn();
    }

    handleSigningIn = () => {
        this.loginUser()
            .then((response) => {
                jwtToLocalStorage(response)
                setAuthorizationToken(response)
            })
            .then(() => {
                if (IsUserSignedIn()) {
                    history.push("/calendar")
                }
            });
    }

    loginUser = () => {
        let loginData = {
            email: this.state.email,
            password: this.state.password
        }

        return axios.post(loginUrl, loginData)
            .then((response) => {
                return response.data;
            })
            .catch(() => {
                toast.error("Invalid username / password")
                history.push("/unauthorized");
            });
    }

    render() {
        return (
            <form>
                <div className="form-group">
                    <label>Email</label>
                    <input name="email"
                        type="email"
                        className="form-control"
                        placeholder="Enter your email"
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
                        onClick={this.handleSubmit}>Sign in</button>
                </div>
            </form>
        );
    }
}