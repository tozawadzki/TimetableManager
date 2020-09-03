import React, { Component } from "react";

export default class Report extends Component {
    constructor(props) {
        super(props);
        this.state = {
            school: 0,
            work: 0,
            workout: 0,
            selfImprovement: 0,
            entertainment: 0
        };
    }

    componentDidMount() {
    }

    componentWillReceiveProps() {
        this.getHours()
    }

    getHours() {
        let report = this.props.report
        console.log(report)
        this.setState({
            school: report.school,
            work: report.work,
            workout: report.workout,
            selfImprovement: report.selfImprovement,
            entertainment: report.entertainment
        })
    }

    render() {
        if (this.props.reportVisible) {
            return (
                <form>
                    <div className="form-group">
                        <div>
                            <label>School: {this.state.school} hours</label>
                        </div>
                        <div>
                            <label>Work: {this.state.work} hours</label>
                        </div>
                        <div>
                            <label>Workout: {this.state.workout} hours</label>
                        </div>
                        <div>
                            <label>Self improvement: {this.state.selfImprovement} hours</label>
                        </div>
                        <div>
                            <label>Entertainment: {this.state.entertainment} hours</label>
                        </div>
                    </div>

                </form>
            )
        }
        else {
            return null
        }
    }
}