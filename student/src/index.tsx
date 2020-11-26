import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {
    BrowserRouter as Router,
    Route,
    RouteComponentProps,
    Switch
} from 'react-router-dom';

const renderApp=({match}:RouteComponentProps<any>) => {
    return (
        <App studentId={match.params.studentId} />
    );
}

ReactDOM.render(
    <React.StrictMode>
        <Router>
            <Switch>
                <Route path="/quiz/:studentId">
                    {renderApp}
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
