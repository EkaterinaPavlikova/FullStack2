import * as React from 'react';
import * as utils from '../utils/common';

// компонент для вывода информации модели
export default ({ student }) => (
    <tr>
        <td>{student.fullName}</td>
        <td>{utils.formatDate(new Date(student.dateOfBirth))}</td>
        <td>{student.userRecordBookID}</td>
        <td>{student.groupNum}</td>
    </tr>
);