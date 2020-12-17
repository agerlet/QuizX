import React from 'react';
import { render, screen } from '@testing-library/react';
import TwoAncientPoems from './TwoAncientPoems';

describe('two ancient poems', () => {
    it('should render the container', () => {
        render(<TwoAncientPoems studentId={"random"} />);
        const container = screen.getByTestId("app");
        expect(container).toBeInTheDocument();
    });
    
    it('should render the table to host the dragables', () => {
       render(<TwoAncientPoems studentId={"random"} />);
       const table = screen.getByTestId("host-table");
       expect(table).toBeInTheDocument();
    });
    
    it('should render three rows in the table', () => {
        render(<TwoAncientPoems studentId={"random"} />);
        const table = screen.getByTestId("host-table");
        expect(table.childElementCount).toBe(1);
        expect(table.children[0].childElementCount).toBe(3);
    });
})