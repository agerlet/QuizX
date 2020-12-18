import React, {useState, useEffect} from 'react';
import "./TwoAncientPoems.css";
import config from './config.json';
import api from './api';
import Moment from 'react-moment';
import 'moment-timezone';

function TwoAncientPoems({ students }: Students) {
    const [answers_0, setAnswers_0] = useState<Answer[]>();
    const [answers_1, setAnswers_1] = useState<Answer[]>();

    useEffect(() => {
        api
            .getAnswers("TwoAncientPoems_5")
            .then((response) => setAnswers_0(response.data));
    }, []);
    
    useEffect(() => {
        api
            .getAnswers("TwoAncientPoems_7")
            .then((response) => setAnswers_1(response.data));
    }, []);
    
    const lookupAnswer = (answers : Answer[] | undefined, studentId : string) => {
        if (!answers) return null;
        for (let i = 0; i < answers.length; i++) {
            if (answers[i].studentId === studentId) {
                return answers[i];
            }
        }
        return null;
    };
    
    function joinAnswers_0(studentId: string) {
        const answer: Answer | null = lookupAnswer(answers_0, studentId);
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

    function joinAnswers(answers: Answer[] | undefined, studentId: string) {
        const answer: Answer | null = lookupAnswer(answers, studentId);
        if (!answer) return "";
        return answer.answers.map(_ => (_.length === 0 ? "''" : _)).join(", ");
    }
    
    function renderAnswers(answer : (Answer | null)[], answers : string[]) {
        var collection = answer.map((a, i) => {
            return {
                answer: a, 
                answers: answers[i]
            };
        });
        
        return (
            <>
                {collection.map((_, i) => {
                    return (
                        <>
                            {i > 0 && <br />}
                            {_.answer && _.answer.completeAt && (
                                <span style={{ color: "green" }}>{_.answers}</span>
                            )}
                            {(!_.answer || !_.answer.completeAt) && _.answers}
                        </>
                    )
                })}
            </>
        )
    }
    
    function renderElaspe(answers: (Answer | null)[]) {
        return (
            <>
                {answers.map(_ => _ && _.completeAt && (
                    <Moment
                        duration={_.arriveAt}
                        date={_.completeAt}
                    />)
                )}
            </>
        );
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
                            answers: [
                                joinAnswers_0(_.studentId),
                                joinAnswers(answers_1, _.studentId)
                            ],
                            answer: [
                                lookupAnswer(answers_0, _.studentId),
                                lookupAnswer(answers_1, _.studentId)
                            ]
                        };
                    })
                    .map(_ => (
                        <tr key={_.student.studentId}>
                            <td>{_.student.chineseName}</td>
                            <td>{_.student.englishName}</td>
                            <td>
                                {config.studentPortalUrl}/TwoAncientPoems/{_.student.studentId}
                            </td>
                            <td data-testid={_.student.studentId + "-answers"}>
                                {renderAnswers(_.answer, _.answers)}
                            </td>
                            <td data-testid={_.student.studentId + "-elapse"}>
                                {renderElaspe(_.answer)}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>);
}

export default TwoAncientPoems;