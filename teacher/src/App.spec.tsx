import React from "react";
import { render, screen } from "@testing-library/react";
import App from "./App";

describe('App', () => {
    it('should render the app list', () => {
        render(<App />);
        const element = screen.getByTestId("list");
        expect(element).toBeInTheDocument();
    })
});