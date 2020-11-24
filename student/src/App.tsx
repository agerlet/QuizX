import React, {useState} from 'react';
import './App.css';
//@ts-ignore
import {DragDropContainer, DropTarget} from 'react-drag-drop-container';

function App() {
    const [blanks, setBlanks] = useState<any[]>(['','','','','','']);

    function handleDrop(e: any) {
        setBlanks(_ => {
            _[e.dropData.id] = e.dragData.label;
            return [..._];
        });
    }

    return (
        <div className="quiz">
            <ul className="options">
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "凉凉"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        凉凉
                    </li>
                </DragDropContainer>
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "雪花"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        雪花
                    </li>
                </DragDropContainer>
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "尝一尝"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        尝一尝
                    </li>
                </DragDropContainer>
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "变成"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        变成
                    </li>
                </DragDropContainer>
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "甜"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        甜
                    </li>
                </DragDropContainer>
            </ul>
            <div className="body">
                <p>
                    亮亮：冬冬快看！下雪了！
                </p>
                <p>
                    冬冬：
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 0}}
                    >
                        <span className="blank">
                            {blanks[0]}
                        </span>
                    </DropTarget>
                    白白的，真好看！
                </p>
                <p>
                    亮亮：你知道雪是什么
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 1}}
                    >
                        <span className="blank">
                            {blanks[1]}
                        </span>
                    </DropTarget>
                    的吗？
                </p>
                <p>
                    冬冬：是水，对吗？
                </p>
                <p>
                    亮亮：对！雪
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 2}}
                    >
                        <span className="blank">
                            {blanks[2]}
                        </span>
                    </DropTarget>吗？
                </p>
                <p>
                    冬冬：我们
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 3}}
                    >
                        <span className="blank">
                            {blanks[3]}
                        </span>
                    </DropTarget>就知道了。
                </p>
                <p>
                    亮亮：雪不
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 4}}
                    >
                        <span className="blank">
                            {blanks[4]}
                        </span>
                    </DropTarget>也不咸，吃在嘴里
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 5}}
                    >
                        <span className="blank">
                            {blanks[5]}
                        </span>
                    </DropTarget>
                    的。
                </p>
            </div>
        </div>
    );
}

export default App;
