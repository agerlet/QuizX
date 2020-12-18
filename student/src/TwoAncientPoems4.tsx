import React, {useState, useCallback, useEffect} from 'react';
import './TwoAncientPoems.css';
import questions from './two-ancient-poem-35.json';
//@ts-ignore
import {DragDropContainer, DropTarget} from 'react-drag-drop-container';
import api from './api';
import {debounce} from 'lodash';

function TwoAncientPoems4({studentId} : Quiz) {
    const [options, setOptions] = useState<string[]>(['', '', '', '', '']);

    const updateBlanks = () => {
        api.postAnswers({
            studentId: studentId,
            quizId: "TwoAncientPoems_35",
            answers: [...options],
        });
    };
    
    function handleDrop(e: any) {
        setOptions(_ => {
            _[e.dropData.id] = e.dragData.label;
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
                    zhǎo chū bù tóng lèi de cí
                </span>
                找出不同类的词
            </h1>
            <table className={"topics"}>
                <tbody>
                {questions.map((_, i) =>
                    <tr key={i}>
                        <td className={"id-column"}>
                            ({i+1})
                        </td>
                        {_.map((x, j) =>
                            <td key={j}>
                                <DragDropContainer
                                    targetKey="blank"
                                    dragData={{label: x.chinese}}
                                    onDrop={handleDrop}
                                >
                                    <span className="item">
                                        <span className="chinese">
                                            <span className="pinyin">{x.pinyin}</span>
                                            {x.chinese}
                                        </span>
                                    </span>
                                </DragDropContainer>
                            </td>
                        )}
                        <td className={"not-belong"}>
                            <DropTarget
                                key={i}
                                targetKey="blank"
                                dropData={{id: i}}
                            >
                                <span className="blank">
                                    {options[i]}
                                </span>
                            </DropTarget>
                        </td>
                    </tr>    
                )}
                </tbody>
            </table>
        </div>
    );
}

export default TwoAncientPoems4;