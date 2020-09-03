import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "../../Styles/SignIn.css";

export default class Unauthorized extends Component {
    render() {
        return (
            <form>
                <div className="form-group">
                    <label type="text" style={{ fontSize: '38px' }} className="unauthorized">401 - Unauthorized</label>
                    <br></br>
                    <label type="text" className="unauthorized">Access denied, please sign in again</label>
                    <br></br>
                </div>

                <p className="sign-in text-right">
                    <Link className="nav-link" to="/sign-in">Sign in</Link>
                </p>
            </form>
        );
    }
}
