import React from 'react';
import {render, screen, act} from '@testing-library/react';
import App from './App';
import {AxiosResponse} from 'axios';
import api from './api';

it('renders app', () => {
    // Act
    render(<App students={[]}/>);
    const element = screen.getByTestId("app");

    // Assert
    expect(element).toBeInTheDocument();
});

it('renders the students info', async () => {
    // Arrange
    const students: Student[] = [
        {
            "studentId": "STUDENT",
            "englishName": "STUDENT",
            "chineseName": "学生",
        }
    ];

    // Act
    await act(async () => {
        render(<App students={students}/>);
    });
    const element = screen.getByText('STUDENT');

    // Assert
    expect(element).toBeInTheDocument();
});

it('renders the students answer field', async () => {
    // Arrange
    const students: Student[] = [
        {
            "studentId": "STUDENT",
            "englishName": "STUDENT",
            "chineseName": "学生",
        }
    ];

    var apiSpyOn = jest.spyOn(api, 'getAnswers').mockImplementationOnce(() =>
        Promise.resolve<AxiosResponse<Answer[]>>({
            data: [
                {
                    "studentId": "STUDENT",
                    "answers": ['a', 'b', 'c', 'd', 'e'],
                    "arriveAt": new Date("2020-11-26T02:40:59.878269Z"),
                    "completeAt": null
                }
            ],
            status: 200,
            statusText: 'OK',
            headers: {},
            config: {}
        }));

    // Act
    await act(async () => {
        render(<App students={students}/>);
    });
    const element = screen.getByTestId('STUDENT-answers');

    // Assert
    expect(element).toBeInTheDocument();
    expect(element).not.toBeEmptyDOMElement();
    expect(element).toHaveTextContent("a, b, c, d, e");
});

it('renders the students Elapse field', async () => {
    // Arrange
    const students: Student[] = [
        {
            "studentId": "STUDENT",
            "englishName": "STUDENT",
            "chineseName": "学生",
        }
    ];
    
    // Act
    await act(async () => {
        render(<App students={students}/>);
    });
    const element = screen.getByTestId('STUDENT-elapse');

    // Assert
    expect(element).toBeInTheDocument();
    expect(element).toBeEmptyDOMElement();
});

it('renders the students elapse field', async () => {
    // Arrange
    const students: Student[] = [
        {
            "studentId": "STUDENT",
            "englishName": "STUDENT",
            "chineseName": "学生",
        }
    ];

    var apiSpyOn = jest.spyOn(api, 'getAnswers').mockImplementationOnce(() =>
        Promise.resolve<AxiosResponse<Answer[]>>({
            data: [
                {
                    "studentId": "STUDENT",
                    "answers": ['a', 'b', 'c', 'd', 'e'],
                    "arriveAt": new Date("2020-11-26T02:40:59.878269Z"),
                    "completeAt": new Date("2020-11-26T02:52:15.384726Z")
                }
            ],
            status: 200,
            statusText: 'OK',
            headers: {},
            config: {}
        }));

    // Act
    await act(async () => {
        render(<App students={students}/>);
    });
    const element = screen.getByTestId('STUDENT-elapse');

    // Assert
    expect(element).not.toBeEmptyDOMElement();
    expect(element).toHaveTextContent("11:16");
});

