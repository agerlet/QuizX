import React, {useState, useCallback, useEffect} from 'react';
import './TwoAncientPoems.css';
import draggables from './two-ancient-poem-5.json';
//@ts-ignore
import {DragDropContainer, DropTarget} from 'react-drag-drop-container';
import api from './api';
import {debounce} from 'lodash';

function TwoAncientPoems({studentId}: Quiz) {
    const [blanks, setBlanks] = useState<any[]>([' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ']);

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
            console.log(e);
            _[e.dropData.id] = e.dragData.label;
            return [...blanks];
        });
    }

    useEffect(() => {
        delayedQuery();

        return delayedQuery.cancel;
    }, [blanks, delayedQuery]);

    return (
        <div
            className={"two-ancient-poems-container"}
            data-testid={"app"}
        >
            <table 
                data-testid={"host-table"}
            >
                <tbody>
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
                </tbody>
            </table>
        </div>
    );
}

export default TwoAncientPoems;