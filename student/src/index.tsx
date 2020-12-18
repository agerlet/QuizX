import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import BabyWhiteCloud from './BabyWhiteCloud';
import TwoAncientPoems from './TwoAncientPoems';
import TwoAncientPoems2 from './TwoAncientPoems2';
import TwoAncientPoems3 from './TwoAncientPoems3';
import TwoAncientPoems4 from './TwoAncientPoems4';
import reportWebVitals from './reportWebVitals';
import {
    BrowserRouter as Router,
    Route,
    RouteComponentProps,
    Switch
} from 'react-router-dom';

const renderBabyWhiteCloud=({match}:RouteComponentProps<any>) => {
    return (
        <BabyWhiteCloud studentId={match.params.studentId} />
    );
};

const renderTwoAncientPoems=({match}:RouteComponentProps<any>) => {
    return (
        <>
            {(match.params.pageId ?? "1") === "1" && <TwoAncientPoems studentId={match.params.studentId} />}
            {match.params.pageId === "2" && <TwoAncientPoems2 studentId={match.params.studentId} />}
            {match.params.pageId === "3" && <TwoAncientPoems3 studentId={match.params.studentId} />}
            {match.params.pageId === "4" && <TwoAncientPoems4 studentId={match.params.studentId} />}
        </>
    );
};

ReactDOM.render(
    <React.StrictMode>
        <Router>
            <Switch>
                <Route path="/quiz/BabyWhiteCloud/:studentId">
                    {renderBabyWhiteCloud}
                </Route>
                <Route path="/quiz/TwoAncientPoems/:studentId/:pageId?" exact={true}>
                    {renderTwoAncientPoems}
                </Route>
            </Switch>
        </Router>
    </React.StrictMode>,
    document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
