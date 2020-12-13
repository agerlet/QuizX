import React, {useState, useEffect} from 'react';
import "./BabyWhiteCloud.css";
import config from './config.json';
import api from './api';
import Moment from 'react-moment';
import 'moment-timezone';

function BabyWhiteCloud({ students }: Students) {
  const [answers, setAnswers] = useState<Answer[]>();

  useEffect(() => {
    api
      .getAnswers("BabyWhiteCloud")
      .then((response) => setAnswers(response.data));
  }, []);

  const getAnswer = (studentId: string) => {
    if (!answers) return null;

    for (let i = 0; i < answers.length; i++) {
      if (answers[i].studentId === studentId) {
        return answers[i];
      }
    }

    return null;
  };

  function joinAnswers(studentId: string) {
    const answer: Answer | null = getAnswer(studentId);
    if (!!answer)
      return answer.answers.map((_) => (_ === "" ? "''" : _)).join(", ");
    return "";
  }

  return (
    <div className="BabyWhiteCloud" data-testid={"app"}>
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
                  {config.studentPortalUrl}/BabyWhiteCloud/{_.student.studentId}
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
    </div>
  );
}

export default BabyWhiteCloud;
