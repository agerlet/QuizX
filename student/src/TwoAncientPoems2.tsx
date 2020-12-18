import React, {useState, useCallback, useEffect} from 'react';
import './TwoAncientPoems.css';
import multiSelects from './two-ancient-poem-7.json';
import api from './api';
import {debounce} from 'lodash';

function TwoAncientPoems2({studentId} : Quiz) {
    const [options, setOptions] = useState<string[]>(['', '', '', '', '']);

    const updateBlanks = () => {
        api.postAnswers({
            studentId: studentId,
            quizId: "TwoAncientPoems_7",
            answers: [...options],
        });
    };
    
    function handleClick(i : number, id: string) {
        setOptions(_ => {
            _[i] = id;
            return [...options];
        });
    }
    
    const delayedQuery = useCallback(debounce(updateBlanks, 200), [options]);
    
    useEffect(() => {
        delayedQuery();
        return delayedQuery.cancel;
    }, [options, delayedQuery]);
    
    function emphase(topic:string, highlight: string) {
        const array = topic
            .replace(highlight, `.[${highlight}].`)
            .split('.')
        return (<>
            {array.map((_, i) => <>
                {_.startsWith('[') && _.endsWith(']') && <>    
                    <span className={"highlight"}>.</span>
                    {_.replace("[", "").replace("]", "")}
                </>
                }
                {!_.startsWith("[") && !_.endsWith("]") && _}
            </>
            )}
        </>);
    };
    
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
                {multiSelects.map((_, i) =>
                    <tr key={i}>
                        <td className={"id-column"}>
                            ({i+1})
                        </td>
                        <td>
                            <span className={"pinyin"}>
                                {_.pinyin}
                            </span>
                            {emphase(_.topic, _.highlight)}
                        </td>
                        <td className={"options"}>
                            <ul>
                                {_.options.map((o, j) =>
                                    <li key={j}>
                                        <a 
                                            href={"#"}
                                            onClick={() => handleClick(i, o.id)}
                                            className={options[i] === o.id ? "selected" : ""}
                                        >
                                            {o.id}.
                                            <span className={"container"}>
                                                <span className={"pinyin"}>
                                                    {o.pinyin}
                                                </span>
                                                    {o.option}
                                            </span>
                                        </a>
                                    </li>
                                )}
                            </ul>
                        </td>
                    </tr>    
                )}
                </tbody>
            </table>
        </div>
    );
}

export default TwoAncientPoems2;