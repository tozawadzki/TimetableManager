import React from 'react';
import '../../Styles/App.css';
import FullCalendar from '@fullcalendar/react';
import timeGridPlugin from '@fullcalendar/timegrid';
import dayGridPlugin from '@fullcalendar/daygrid';
import listPlugin from '@fullcalendar/list';
import interactionPlugin from '@fullcalendar/interaction';
import axios from "axios";
import history from '../../History.js';
import { toast } from "react-toastify";
import { eventsUrl, newEventUrl, newRoutineEventUrl, baseEventsUrl, spentHoursUrl } from "../../const"
import NewEvent from './NewEvent';
import NewRecurringEvent from './NewRecurringEvent';
import Report from './Report';
export default class Calendar extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            events: [],
            businessHours: [],
            report: [],
            newEventVisible: false,
            newRecurringEventVisible: false,
            reportVisible: false
        }
    }

    componentDidMount() {
        this.getEvents();
        this.setBasicReport()
    }

    setBasicReport() {
        if (this.calendar != null) {
            let left = this.calendar._calendarApi.currentDataManager.state.dateProfile.activeRange.start
            let right = this.calendar._calendarApi.currentDataManager.state.dateProfile.activeRange.end
            let interval = {
                start: left,
                end: right
            }
            this.getReport(interval)
        }
    }

    getEvents() {
        axios.get(eventsUrl)
            .then((response) => {
                this.setState({
                    events: response.data
                })
            })
            .catch((error) => {
                toast.error("There is a problem with loading your events ", error.response)
                history.push("/unauthorized");
            })
    }

    getReport(interval) {
        axios.post(spentHoursUrl, interval)
            .then((response) => {
                this.setState({
                    report: response.data
                })
            })
            .catch((error) => {
                toast.error("There is a problem with loading your report", error.response)
                history.push("/unauthorized");
            })
    }

    handleSelect = (eventInfo) => {
        this.setState({
            displayNewEventWindow: true
        })

        let title = prompt('Please enter a new title for your event')
        let calendarApi = eventInfo.view.calendar
        calendarApi.unselect()

        if (title) {
            let eventToAdd = {
                title: title,
                start: eventInfo.startStr,
                end: eventInfo.endStr,
                allDay: eventInfo.allDay,
                daysOfWeek: [0, 1, 2, 3, 4]
            }
            calendarApi.addEvent(eventToAdd)
            this.addEvent(eventToAdd)
        }
        console.log(this.state.report)
    }

    addEvent = (eventToAdd) => {
        axios.post(newEventUrl, eventToAdd)
            .then(({ data }) => {
                const { events } = this.state;
                const newEventsList = [...events, data]
                this.setState({
                    events: newEventsList
                })
            })
            .catch((error) => {
                toast.error("There is a problem with creating a new event: ", error.response)
            })
    }

    addRoutineEvent = (eventToAdd) => {
        axios.post(newRoutineEventUrl, eventToAdd)
            .then(({ data }) => {
                const { events } = this.state;
                const newEventsList = [...events, data]
                this.setState({
                    events: newEventsList
                })
            })
            .catch((error) => {
                toast.error("There is a problem with creating a new routine event: ", error.response)
            })
    }

    updateEvent = (eventToUpdate) => {
        let updatedEventId = eventToUpdate.event._def.publicId
        let updateRequest = `${baseEventsUrl}/${updatedEventId}`
        console.log(eventToUpdate)
        let updatedEvent = {
            title: eventToUpdate.event._def.title,
            start: eventToUpdate.event._instance.range.start,
            end: eventToUpdate.event._instance.range.end,
            allDay: eventToUpdate.event._def.allDay
        }
        console.log(updatedEvent)

        axios.put(updateRequest, updatedEvent)
            .catch((error) => {
                toast.error("There is a problem with updating this event:", error.response)
            })
    }

    deleteEvent = (eventToDelete) => {
        let id = eventToDelete.event._def.publicId
        let deleteRequest = `${baseEventsUrl}/${id}`
        axios.delete(deleteRequest)
            .catch((error) => {
                toast.error("There is a problem with deleting this event:", error.response)
            })
    }


    handleEventClick = (clickInfo) => {
        console.log(clickInfo)
        if (window.confirm(`Are you sure you want to delete the event '${clickInfo.event.title}'`)) {
            clickInfo.event.remove()
        }
    }

    handleViewChange = () => {
        if (this.calendar != null) {
            let left = this.calendar._calendarApi.currentDataManager.state.dateProfile.activeRange.start
            let right = this.calendar._calendarApi.currentDataManager.state.dateProfile.activeRange.end
            let interval = {
                start: left,
                end: right
            }
            this.getReport(interval)
        }

    }

    hideNewEvent = () => {
        this.setState({
            newEventVisible: false
        })
    }

    hideNewRecurringEvent = () => {
        this.setState({
            newRecurringEventVisible: false
        })
    }

    // tak zrobić reszte z ...this.state
    render() {
        return (
            <div>
                <NewEvent isVisible={this.state.newEventVisible} createEvent={this.addEvent} hideWindow={this.hideNewEvent} />
                <NewRecurringEvent isVisible={this.state.newRecurringEventVisible} createEvent={this.addRoutineEvent} hideWindow={this.hideNewRecurringEvent} />

                <Report {...this.state} />
                <FullCalendar ref={calendar => this.calendar = calendar}
                    timeZone='UTC+2'
                    datesAboveResources={true}
                    headerToolbar={{
                        left: 'title',
                        right: 'reportButton, newEventButton, newRecurringEventButton, prev, next, today, timeGridDay, timeGridWeek, dayGridMonth, listWeek'
                    }}
                    initialView='timeGridWeek'
                    views={{
                        listWeek: { buttonText: 'agenda' }
                    }}
                    editable={true}
                    selectable={true}
                    selectMirror={true}
                    select={this.handleSelect}
                    slotMinTime='06:00:00'
                    slotMaxTime='24:00:00'
                    weekends={false}
                    plugins={[timeGridPlugin, interactionPlugin, dayGridPlugin, listPlugin]}
                    events={this.state.events}
                    eventChange={this.updateEvent}
                    eventRemove={this.deleteEvent}
                    eventClick={this.handleEventClick}
                    dayHeaderDidMount={this.handleViewChange}
                    customButtons={{
                        newEventButton: {
                            text: 'New event',
                            click: function () {
                                this.setState({
                                    newEventVisible: !this.state.newEventVisible,
                                    newRecurringEventVisible: false,
                                    reportVisible: false
                                })
                            }.bind(this)
                        },
                        newRecurringEventButton: {
                            text: 'New recurring event',
                            click: function () {
                                this.setState({
                                    newRecurringEventVisible: !this.state.newRecurringEventVisible,
                                    newEventVisible: false,
                                    reportVisible: false
                                })
                            }.bind(this)
                        },
                        reportButton: {
                            text: 'Report',
                            click: function () {
                                this.setState({
                                    newEventVisible: false,
                                    newRecurringEventVisible: false,
                                    reportVisible: !this.state.reportVisible
                                })
                            }.bind(this)
                        }
                    }}
                />
            </div>
        )
    }
}
