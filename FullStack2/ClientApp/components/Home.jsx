import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Filters from './Filters';
import Student from './Student';
import * as consts from '../constants/common';

// Главный компонент, является контейнером.
export class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            students: [],
            filteredStudents: [],
        };
        this.loadData();
        this.filterData = this.filterData.bind(this);
    }

    filterData(filter) {
        if (filter.filterMode === consts.FILTER_MODES.CLIENT) {
            this.filterOnClient(filter);
        }
        else {
            this.filterOnServer(filter);
        }
    }

    filterOnClient(filter) {
        if (filter.text !== "" || filter.date !== "" || filter.number !== "") {
            let filteredData = this.state.students
                .filter((student) => { return student.fullName.toLowerCase().indexOf(filter.text.toLowerCase()) !== -1 })
                .filter((student) => { return (student.dateOfBirth >= filter.date) });
            if (filter.number !== "") {
                filteredData = filteredData.filter((student) => { return (student.groupNum === +filter.number) });
            }
            this.setState({ filteredStudents: filteredData });
        }
        else {
            this.setState(function (prevState, props) { return { filteredStudents: prevState.students } });
        }
    }

    filterOnServer(filter) {
        var students;
        let url = consts.API_URL +
            "/filter?name=" + filter.text +
            "&dateOfBirth=" + filter.date +
            "&group=" + filter.number;
        fetch(url, {
            method: 'GET',
            headers: {
                "Content-Type": "text/plain"
            }
        })
            .then(function (response) {
                return response.json();
            })
            .then((respJson) => {
                this.setState({ students: respJson, filteredStudents: respJson });
            },
            (error) => {
                return null;
            });
    }

    loadData() {
        let url = consts.API_URL + "/getstudents";
        fetch(url, {
            method: 'GET',
            headers: {
                "Content-Type": "text/plain"
            }
        })
            .then(function (response) {
                return response.json();
            })
            .then((respJson) => {
                this.setState({ students: respJson, filteredStudents: respJson });
            },
            (error) => {
                var e = error;
            });
    }

    render() {
        return <div>
            <h2>Список студентов</h2>
            <div >
                <Filters filterChanged={this.filterData} />
            </div>
            <table className="table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>ФИО</th>
                        <th>Дата рождения</th>
                        <th>Номер читательского</th>
                        <th>Группа</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.state.filteredStudents &&
                        this.state.filteredStudents.map((student) => <Student key={student.id} student={student} />)
                    }
                </tbody>
            </table>
        </div>
    }
}
