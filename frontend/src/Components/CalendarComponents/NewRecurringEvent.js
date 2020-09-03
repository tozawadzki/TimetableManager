import React, { Component } from "react";
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown'

export default class NewRecurringEvent extends Component {
    constructor(props) {
        super(props);
        this.state = {
            title: "",
            allDay: false,
            monday: false,
            tuesday: false,
            wednesday: false,
            thursday: false,
            friday: false,
            start: "",
            end: "",
            color: "blue",
            dropdownTitle: "School"
        };
    }

    componentDidMount() {

    }

    handleChange = (event) => {
        console.log(event.target.name)
        const value = event.target.type === 'checkbox' ? event.target.checked : event.target.value;
        const name = event.target.name;
        this.setState({
            [name]: value
        });
    }

    onSubmit = () => {
        let days = []
        if (this.state.monday === true)
            days.push(1)
        if (this.state.tuesday === true)
            days.push(2)
        if (this.state.wednesday === true)
            days.push(3)
        if (this.state.thursday === true)
            days.push(4)
        if (this.state.friday === true)
            days.push(5)

        let newEvent = {
            title: this.state.title,
            allDay: this.state.allDay,
            startTime: this.state.start,
            endTime: this.state.end,
            daysOfWeek: days,
            color: this.state.color
        }

        console.log(newEvent)

        this.props.createEvent(newEvent)
        this.props.hideWindow()
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

    render() {
        if (this.props.isVisible === true) {
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
                            type="time"
                            className="form-control"
                            value={this.state.start}
                            disabled={this.state.allDay}
                            onChange={this.handleChange} />
                    </div>
                    <div className="form-group">
                        <label>End</label>
                        <input name="end"
                            type="time"
                            className="form-control"
                            value={this.state.end}
                            disabled={this.state.allDay}
                            onChange={this.handleChange} />
                    </div>
                    <div className="form-group">
                        <label>Repeat every</label>
                        <ul class="list-group">
                            <li class="list-group-item rounded-0">
                                <div class="custom-control custom-checkbox">
                                    <input class="custom-control-input" id="monday" name="monday" type="checkbox" onChange={this.handleChange} />
                                    <label class="custom-control-label" for="monday">Monday</label>
                                </div>
                            </li>
                            <li class="list-group-item">
                                <div class="custom-control custom-checkbox">
                                    <input class="custom-control-input" id="tuesday" name="tuesday" type="checkbox" onChange={this.handleChange} />
                                    <label class="custom-control-label" for="tuesday">Tuesday</label>
                                </div>
                            </li>
                            <li class="list-group-item">
                                <div class="custom-control custom-checkbox">
                                    <input class="custom-control-input" id="wednesday" name="wednesday" type="checkbox" onChange={this.handleChange} />
                                    <label class="custom-control-label" for="wednesday">Wednesday</label>
                                </div>
                            </li>
                            <li class="list-group-item">
                                <div class="custom-control custom-checkbox">
                                    <input class="custom-control-input" id="thursday" name="thursday" type="checkbox" onChange={this.handleChange} />
                                    <label class="custom-control-label" for="thursday">Thursday</label>
                                </div>
                            </li>
                            <li class="list-group-item">
                                <div class="custom-control custom-checkbox">
                                    <input class="custom-control-input" id="friday" name="friday" type="checkbox" onChange={this.handleChange} />
                                    <label class="custom-control-label" for="friday">Friday</label>
                                </div>
                            </li>
                        </ul>
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
                            onClick={this.onSubmit}>Add routine</button>
                    </div>
                </form>
            )
        }
        else {
            return null
        }

    }
}