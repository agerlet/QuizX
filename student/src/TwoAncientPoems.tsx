import React, {useState, useCallback, useEffect} from 'react';
import './TwoAncientPoems.css';
import draggables from './two-ancient-poem-5.json';
//@ts-ignore
import {DragDropContainer, DropTarget} from 'react-drag-drop-container';
import api from './api';
import {debounce} from 'lodash';

function TwoAncientPoems({studentId}: Quiz) {
    const [blanks, setBlanks] = useState<string[]>(['', '', '', '', '', '', '', '', '', '', '', '']);
    const [showNext, setShowNext] = useState<boolean>(false);

    const updateBlanks = () => {
        api.postAnswers({
            studentId: studentId,
            quizId: "TwoAncientPoems_5",
            answers: [...blanks],
        });
    };

    const delayedQuery = useCallback(debounce(updateBlanks, 200), [blanks]);

    function handleDrop(e: any) {
        setBlanks(_ => {
            _[e.dropData.id] = e.dragData.label;
            return [...blanks];
        });
        if (blanks.every(_ => _.trim().length > 0)) {
            setShowNext(true);
        }
    }

    useEffect(() => {
        delayedQuery();
        return delayedQuery.cancel;
    }, [blanks, delayedQuery]);

    function renderDraggables() {
        return (
            <>
                {draggables.draggables.map((_, i) =>
                    <tr
                        key={i}
                    >
                        {_.map((x, y) =>
                            <td key={y}>
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
                    </tr>
                )}
            </>
        );
    }

    function renderDropTarget() {
        return (
            <tr>
            {[...Array(draggables.slots)].map((_, i) =>
                <td
                    key={i}
                    data-testid={i * 2}
                >
                    <DropTarget
                        key={i * 2}
                        targetKey="blank"
                        dropData={{id: i * 2}}
                    >
                        <span className="blank">
                            {blanks[i * 2]}
                        </span>
                    </DropTarget>
                    <DropTarget
                        key={i * 2 + 1}
                        targetKey="blank"
                        dropData={{id: i * 2 + 1}}
                    >
                        <span className="blank">
                            {blanks[i * 2 + 1]}
                        </span>
                    </DropTarget>
                </td>
            )}
            </tr>
        );
    }

    function renderNextQuestion() {
        return (
            <>
                {showNext &&
                <tfoot>
                    <tr>
                        <td
                            colSpan={draggables.slots}
                            style={{textAlign: "right"}}
                        >
                            <a href={`/quiz/TwoAncientPoems/${studentId}/2`}>Next</a>
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
            data-testid={"app"}
        >
            <h1>连一连，组词语</h1>
            <table
                data-testid={"host-table"}
            >
                <tbody>
                {renderDraggables()}
                {renderDropTarget()}
                </tbody>
                {renderNextQuestion()}
            </table>
        </div>
    );
}

export default TwoAncientPoems;