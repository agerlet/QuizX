import React, {useState, useCallback, useEffect} from 'react';
import './App.css';
//@ts-ignore
import {DragDropContainer, DropTarget} from 'react-drag-drop-container';
import api from './api';
import {debounce} from 'lodash';

interface Quiz {
    studentId: string
}

function App({studentId} : Quiz) {
    const [blanks, setBlanks] = useState<any[]>(['','','','','','']);
    
    const updateBlanks = () => {
        api.postAnswers({
          studentId: studentId,
          quizId: "BabyWhiteCloud",
          answers: [...blanks],
        });       
    };

    const delayedQuery = useCallback(debounce(updateBlanks, 200), [blanks]);
    
    function handleDrop(e: any) {
        setBlanks(_ => {
            _[e.dropData.id] = e.dragData.label;
            return [...blanks];
        });
    }
    
    useEffect(() => {
        delayedQuery();
        
        return delayedQuery.cancel;
    }, [blanks, delayedQuery]);

    return (
        <div className="quiz">
            <ul 
                className="options" 
                data-testid="options"
            >
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "凉凉"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        <span className="chinese">
                            <span className="pinyin">liáng liáng</span>
                            凉凉
                        </span>
                    </li>
                </DragDropContainer>
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "雪花"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        <span className="chinese">
                            <span className="pinyin">xuě huā</span>
                            雪花
                        </span>
                    </li>
                </DragDropContainer>
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "尝一尝"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        <span className="chinese">
                            <span className="pinyin">cháng yī cháng</span>
                            尝一尝
                        </span>
                    </li>
                </DragDropContainer>
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "变成"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        <span className="chinese">
                            <span className="pinyin">biàn chéng</span>
                            变成
                        </span>
                    </li>
                </DragDropContainer>
                <DragDropContainer
                    targetKey="blank"
                    dragData={{label: "甜"}}
                    onDrop={handleDrop}
                >
                    <li className="item">
                        <span className="chinese">
                            <span className="pinyin">tián</span>
                            甜
                        </span>
                    </li>
                </DragDropContainer>
            </ul>
            <div 
                className="body"
                data-testid={"body"}
            >
                <p>
                    <span className="chinese">
                        <span className="pinyin">liàng liàng</span>
                        亮亮：
                    </span>
                    <span className="chinese">
                        <span className="pinyin">dōng dōng kuài kàn</span>
                        冬冬快看！
                    </span>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; xià &nbsp; xuě &nbsp; le</span>
                        下雪了！
                    </span>
                </p>
                <p>
                    <span className="chinese">
                        <span className="pinyin">dōng dōng</span>
                        冬冬：
                    </span> 
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 0}}
                    >
                        <span className="blank">
                            {blanks[0]}
                        </span>
                    </DropTarget>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; bái &nbsp; bái &nbsp; de</span>
                        白白的，
                    </span>
                    <span className="chinese">
                        <span className="pinyin">zhēn hǎo kàn</span>
                        真好看！
                    </span>
                </p>
                <p>
                    <span className="chinese">
                        <span className="pinyin">liàng liàng</span>
                        亮亮：
                    </span>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; nǐ &nbsp; &nbsp; zhī &nbsp; dào xuě &nbsp; shì shén me</span>
                        你知道雪是什么
                    </span>
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 1}}
                    >
                        <span className="blank">
                            {blanks[1]}
                        </span>
                    </DropTarget>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; de &nbsp; ma</span>
                        的吗？
                    </span>
                </p>
                <p>
                    <span className="chinese">
                        <span className="pinyin">dōng dōng</span>
                        冬冬：
                    </span>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; shì shuǐ</span>
                        是水，
                    </span>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; duì &nbsp; ma</span>
                        对吗？
                    </span>
                </p>
                <p>
                    <span className="chinese">
                        <span className="pinyin">liàng liàng</span>
                        亮亮：
                    </span>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; duì</span>
                        对！
                    </span>
                    <span className="chinese">
                        <span className="pinyin">xuě</span>
                        雪
                    </span>
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 2}}
                    >
                        <span className="blank">
                            {blanks[2]}
                        </span>
                    </DropTarget>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; ma</span>
                        吗？
                    </span>
                </p>
                <p>
                    <span className="chinese">
                        <span className="pinyin">dōng dōng</span>
                        冬冬：
                    </span>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; wǒ men</span>
                        我们
                    </span>
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 3}}
                    >
                        <span className="blank">
                            {blanks[3]}
                        </span>
                    </DropTarget>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; jiù &nbsp; zhī &nbsp;&nbsp; dào &nbsp; le</span>
                        就知道了。
                    </span>
                </p>
                <p>
                    <span className="chinese">
                        <span className="pinyin">liàng liàng</span>
                        亮亮：
                    </span>
                    <span className="chinese">
                        <span className="pinyin">xuě &nbsp; bù</span>
                        雪不
                    </span>
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 4}}
                    >
                        <span className="blank">
                            {blanks[4]}
                        </span>
                    </DropTarget>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; yě &nbsp; bù &nbsp; xuán</span>
                        也不咸，
                    </span>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; chī &nbsp; cài &nbsp; zuǐ &nbsp; &nbsp; lǐ</span>
                        吃在嘴里
                    </span>
                    <DropTarget
                        targetKey="blank"
                        dropData={{id: 5}}
                    >
                        <span className="blank">
                            {blanks[5]}
                        </span>
                    </DropTarget>
                    <span className="chinese">
                        <span className="pinyin">&nbsp; de</span>
                        的。
                    </span>
                </p>
            </div>
        </div>
    );
}

export default App;
