import React, {useState, useCallback, useEffect} from 'react';
import './TwoAncientPoems.css';
import questions from './two-ancient-poem-6.json';
import api from './api';
import {debounce} from 'lodash';

function TwoAncientPoems3({studentId} : Quiz) {
    const [options, setOptions] = useState<string[]>(['', '', '', '']);
    const [showNext, setShowNext] = useState<boolean>(false);

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
        if (options.every(_ => _.trim().length > 0)) {
            setShowNext(true);
        }
    }
    
    const delayedQuery = useCallback(debounce(updateBlanks, 200), [options]);
    
    useEffect(() => {
        delayedQuery();
        return delayedQuery.cancel;
    }, [options, delayedQuery]);
    
    function renderNextQuestion() {
        return (
            <>
                {showNext &&
                <tfoot>
                <tr>
                    <td
                        colSpan={3}
                        style={{textAlign: "right"}}
                    >
                        <a href={`/quiz/TwoAncientPoems/${studentId}/4`}>Next</a>
                    </td>
                </tr>
                </tfoot>
                }
            </>
        );
    }
    return (
        <div
            className={"two-ancient-poems-container"}
        >
            <h1>
                <span className={"pinyin"}>
                    pàn duàn jù zi, duì de xuǎn "✔", cuò de xuǎn "✘"
                </span>
                判断句子，对的选"✔"，错的选"✘"
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
                {renderNextQuestion()}
            </table>
        </div>
    );
}

export default TwoAncientPoems3;