import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders quiz options', () => {
  render(<App />);
  const options = screen.getByTestId(/options/i);
  expect(options).toBeInTheDocument();
});

test('renders quiz body', () => {
  render(<App />);
  const quizBody = screen.getByTestId(/body/i);
  expect(quizBody).toBeInTheDocument();
});

