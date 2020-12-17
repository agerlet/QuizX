import React, {useState, useEffect} from 'react';
import "./TwoAncientPoems.css";
import config from './config.json';
import api from './api';
import Moment from 'react-moment';
import 'moment-timezone';

function TwoAncientPoems({ students }: Students) {
    const [answers_0, setAnswers_0] = useState<Answer[]>();

    useEffect(() => {
        api
            .getAnswers("TwoAncientPoems_5")
            .then((response) => setAnswers_0(response.data));
    }, []);

    const getAnswer = (studentId: string) => {
        if (!answers_0) return null;

        for (let i = 0; i < answers_0.length; i++) {
            if (answers_0[i].studentId === studentId) {
                return answers_0[i];
            }
        }

        return null;
    };

    function joinAnswers(studentId: string) {
        const answer: Answer | null = getAnswer(studentId);
        if (!!answer) {
            let answers = [];
            for(let i = 0; i < answer.answers.length - 1; i += 2)
            {
                answers.push(answer.answers[i] + answer.answers[i + 1]);
            }
            return answers.map((_) => (_.length === 0 ? "''" : _)).join(", ");
        }
        return "";
    }

    return (
        <div className={"two-ancient-poems"} data-testid={"two-ancient-poems-container"}>
            <table>
                <thead>
                <tr>
                    <td className={"chinese-name"}>Chinese Name</td>
                    <td className={"english-name"}>English Name</td>
                    <td className={"url"}>URL</td>
                    <td className={"results"}>Results</td>
                    <td className={"Elapse"}>Elaspe</td>
                </tr>
                </thead>
                <tbody>
                {students
                    .map((_: Student) => {
                        return {
                            student: _,
                            answers: joinAnswers(_.studentId),
                            answer: getAnswer(_.studentId),
                        };
                    })
                    .map((_) => (
                        <tr key={_.student.studentId}>
                            <td>{_.student.chineseName}</td>
                            <td>{_.student.englishName}</td>
                            <td>
                                {config.studentPortalUrl}/TwoAncientPoems/{_.student.studentId}
                            </td>
                            <td data-testid={_.student.studentId + "-answers"}>
                                {_.answer && _.answer.completeAt && (
                                    <span style={{ color: "green" }}>{_.answers}</span>
                                )}
                                {(!_.answer || !_.answer.completeAt) && _.answers}
                            </td>
                            <td data-testid={_.student.studentId + "-elapse"}>
                                {_.answer && _.answer.completeAt && (
                                    <Moment
                                        duration={_.answer.arriveAt}
                                        date={_.answer.completeAt}
                                    />
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>);
}

export default TwoAncientPoems;