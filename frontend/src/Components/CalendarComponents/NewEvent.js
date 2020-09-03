import React, { Component } from "react";
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown'

export default class NewEvent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            title: "",
            allDay: false,
            start: "",
            end: "",
            color: "blue",
            dropdownTitle: "School"
        };
    }

    componentDidMount() {

    }

    handleChange = (event) => {
        const value = event.target.type === 'checkbox' ? event.target.checked : event.target.value;
        const name = event.target.name;
        this.setState({
            [name]: value
        });
        console.log(this.state)
    }

    handleColorSelect = (e) => {
        let title = ""
        switch (e) {
            case "blue":
                title = "School"
                break;
            case "green":
                title = "Work"
                break;
            case "red":
                title = "Workout"
                break;
            case "black":
                title = "Self improvement"
                break;
            case "yellow":
                title = "Entertainment"
                break;
            default:
                title = "School"
                break;
        }
        this.setState({
            color: e,
            dropdownTitle: title
        })
        console.log(e)
    }

    onSubmit = () => {
        let newEvent = {
            title: this.state.title,
            allDay: this.state.allDay,
            start: this.state.start,
            end: this.state.end,
            color: this.state.color
        }
        this.props.createEvent(newEvent)
        this.props.hideWindow()
    }

    render() {
        if (this.props.isVisible) {
            return (
                <form>
                    <div className="form-group">
                        <label>Title</label>
                        <input name="title"
                            type="text"
                            className="form-control"
                            placeholder="Event title"
                            value={this.state.email}
                            onChange={this.handleChange} />
                    </div>
                    <div className="form-group">
                        <label>Is all day?</label>
                        <input name="allDay"
                            type="checkbox"
                            className="form-check-input col-md-1"
                            placeholder="Event title"
                            data-toggle="switch"
                            onChange={this.handleChange} />
                    </div>
                    <div className="form-group">
                        <label>Start</label>
                        <input name="start"
                            type="datetime-local"
                            className="form-control"
                            value={this.state.start}
                            disabled={this.state.allDay}
                            onChange={this.handleChange} />
                    </div>
                    <div className="form-group">
                        <label>End</label>
                        <input name="end"
                            type="datetime-local"
                            className="form-control"
                            value={this.state.end}
                            disabled={this.state.allDay}
                            onChange={this.handleChange} />
                    </div>
                    <div className="form-group">
                        <label>Event type</label>
                        <DropdownButton
                            id="colorDropdown"
                            onSelect={this.handleColorSelect}
                            title={this.state.dropdownTitle}>
                            <Dropdown.Item eventKey="blue">School</Dropdown.Item>
                            <Dropdown.Item eventKey="green">Work</Dropdown.Item>
                            <Dropdown.Item eventKey="red">Workout</Dropdown.Item>
                            <Dropdown.Item eventKey="black">Self improvement</Dropdown.Item>
                            <Dropdown.Item eventKey="yellow">Entertainment</Dropdown.Item>
                        </DropdownButton>
                    </div>
                    <p />
                    <div>
                        <button type="button"
                            className="btn btn-primary btn-block"
                            onClick={this.onSubmit}>Add event</button>
                    </div>
                </form>
            )
        }
        else {
            return null
        }

    }
}