import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import DatePicker from 'react-bootstrap-date-picker';
import * as consts from '../constants/common';
import * as utils from '../utils/common';

// Компонент панели фильтров
export default class Filters extends React.Component {
    constructor(props) {
        super(props);
        this.dateChanged = this.dateChanged.bind(this);
        this.numberChanged = this.numberChanged.bind(this);
        this.filterChanged = this.filterChanged.bind(this);
        this.applyFilter = this.applyFilter.bind(this);
        this.planTask = this.planTask.bind(this);;
        this.state = {
            numberFilter: '',
            dateFilter: '',
            numberIsValid: true
        };
    }

    timer = null;

    // функция отсрочки задания с возможностью прерывания
    planTask(task) {
        if (this.timer) {
            clearTimeout(this.timer);
        }
        this.timer = setTimeout(task, 500);
    }
    applyFilter() {
        var filterObj = {
            text: this.textFilter.value,
            date: this.state.dateFilter,
            number: this.state.numberFilter,
            filterMode: this.filterMode.checked ? consts.FILTER_MODES.SERVER : consts.FILTER_MODES.CLIENT,
        }
        this.props.filterChanged(filterObj);
    }
    
    filterChanged(event) {
        this.planTask(this.applyFilter);
    }
    numberChanged(event) {
        var value = event.target.value;
        var isValid = utils.isNumber(value);
        if (isValid) {
            this.setState({ numberFilter: value > 0 ? value : "" }, () => this.planTask(this.applyFilter));
        }
        this.setState({ numberIsValid: isValid });
    }

    dateChanged(value, formattedValue) {
        this.setState({ dateFilter: value || '' }, () => this.planTask(this.applyFilter));
    }

    render() {
        return (<div className="form-group row">
            <div className="col-xs-3">
                <label>ФИО</label>
                <input type="text"
                    ref={(elem) => this.textFilter = elem}
                    className="form-control"
                    onChange={this.filterChanged}
                />
            </div>
            <div className="col-xs-3">
                <label>Дата рождения:</label>
                <DatePicker
                    value={this.state.dateFilter}
                    dateFormat="MM-DD-YYYY"
                    onChange={this.dateChanged} />
            </div>
            <div className="col-xs-3">
                <label>Номер группы</label>
                <input type="text"
                    value={this.state.numberFilter}
                    className="form-control"
                    onChange={this.numberChanged}
                    style={{ borderColor: this.state.numberIsValid ? "#ccc" : "red" }}
                />
            </div>
            <div className="col-xs-3">
                <label>Фильтрация на сервере</label>
                &nbsp;
                <input type="checkbox"
                    ref={(elem) => this.filterMode = elem}
                    onChange={this.filterChanged}
                />
            </div>
        </div>);
    }
}
