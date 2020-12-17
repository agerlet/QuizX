import { render } from "@testing-library/react";
import React from "react";

export default function App () {
    return (
        <ul data-testid="list">
            <li><a href="/BabyWhiteCloud/">白云娃娃练习</a></li>
            <li><a href="/TwoAncientPoems/">古诗二首</a></li>
        </ul>
    );
}