import React, {useState, useCallback, useEffect} from 'react';
import './TwoAncientPoems.css';
import questions from './two-ancient-poem-6.json';
import api from './api';
import {debounce} from 'lodash';

function TwoAncientPoems3({studentId} : Quiz) {
    const [options, setOptions] = useState<string[]>(['', '', '', '']);

    const updateBlanks = () => {
        api.postAnswers({
            studentId: studentId,
            quizId: "TwoAncientPoems_6",
            answers: [...options],
        });
    };
    
    function handleClick(i : number, tick: string) {
        setOptions(_ => {
            _[i] = tick;
            return [...options];
        });
    }
    
    const delayedQuery = useCallback(debounce(updateBlanks, 200), [options]);
    
    useEffect(() => {
        delayedQuery();
        return delayedQuery.cancel;
    }, [options, delayedQuery]);
    
    return (
        <div
            className={"two-ancient-poems-container"}
        >
            <h1>
                <span className={"pinyin"}>
                    xuǎn chū xià liè jù zi zhõng jiā diǎn zì de zhèng què jiě shì
                </span>
                选出下列句子中加点字的正确解释
            </h1>
            <table className={"topics"}>
                <tbody>
                {questions.map((_, i) =>
                    <tr key={i}>
                        <td className={"id-column"}>
                            ({i+1})
                        </td>
                        <td className={"sentences"}>
                            <span className={"pinyin"}>
                                {_.pinyin}
                            </span>
                            {_.topic}
                        </td>
                        <td className={"tick-or-cross"}>
                            <a 
                                href={"#"}
                                onClick={() => handleClick(i, "v")}
                                className={options[i] === "v" ? "selected" : ""}
                            >
                                ✔
                            </a>
                            <a
                                href={"#"}
                                onClick={() => handleClick(i, "x")}
                                className={options[i] === "x" ? "selected" : ""}
                            >
                                ✘
                            </a>
                        </td>
                    </tr>    
                )}
                </tbody>
            </table>
        </div>
    );
}

export default TwoAncientPoems3;