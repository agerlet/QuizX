import React from 'react';
import { render, screen } from '@testing-library/react';
import BabyWhiteCloud from './BabyWhiteCloud';

test('renders quiz options', () => {
  render(<BabyWhiteCloud />);
  const options = screen.getByTestId(/options/i);
  expect(options).toBeInTheDocument();
});

test('renders quiz body', () => {
  render(<BabyWhiteCloud />);
  render(<BabyWhiteCloud />);
  const quizBody = screen.getByTestId(/body/i);
  expect(quizBody).toBeInTheDocument();
});

