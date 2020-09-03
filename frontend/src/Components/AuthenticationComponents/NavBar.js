import React, { Component } from "react";
import history from '../../History.js';

export default class NavBar extends Component {
    handleSubmit = (element) => {
        history.push("/sign-up")
    }

    render() {
        return (
            <nav className="navbar navbar-expand-sm bg-light" >
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav ml-auto">
                        <li className="sign-button" style={{ padding: '5px' }}>
                            <button
                                type="submit"
                                className="btn btn-primary btn-block"
                                onClick={() => { history.push("/sign-in") }}>Sign in</button>
                        </li>
                        <li className="sign-button" style={{ padding: '5px' }}>
                            <button
                                type="submit"
                                className="btn btn-primary btn-block"
                                onClick={() => { history.push("/sign-up") }}>Sign up</button>
                        </li>
                    </ul>
                </div>
            </nav >
        )
    }
}
