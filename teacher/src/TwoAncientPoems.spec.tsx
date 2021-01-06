import React from "react";
import { render, screen, act } from "@testing-library/react";
import TwoAncientPoems from "./TwoAncientPoems";
import { AxiosResponse } from "axios";
import api from "./api";
import BabyWhiteCloud from "./BabyWhiteCloud";

const mockApi = jest.spyOn(api, "getAnswers");

describe('two ancient poems', () => {

    afterEach(() => {
        mockApi.mockClear();
    });
    
    it('should render the container', () => {
       render(<TwoAncientPoems students={[]} />);
       const element = screen.getByTestId("two-ancient-poems-container");
       expect(element).toBeInTheDocument();
    });
    
    it("renders the students info", async () => {
        // Arrange
        const students: Student[] = [
            {
                studentId: "STUDENT",
                englishName: "STUDENT",
                chineseName: "学生",
            },
        ];

        // Act
        render(<TwoAncientPoems students={students} />);
        const element = screen.getByText("STUDENT");

        // Assert
        expect(element).toBeInTheDocument();
    });
    it("renders the students answer field", async () => {
        // Arrange
        const students: Student[] = [
            {
                studentId: "STUDENT",
                englishName: "STUDENT",
                chineseName: "学生",
            },
        ];

        mockApi.mockImplementationOnce((quizId: string) =>
            Promise.resolve<AxiosResponse<Answer[]>>({
                data: [
                    {
                        studentId: "STUDENT",
                        quizId: quizId,
                        answers: ["a", "b", "c", "d", "e"],
                        arriveAt: new Date("2020-11-26T02:40:59.878269Z"),
                        completeAt: null,
                    },
                ],
                status: 200,
                statusText: "OK",
                headers: {},
                config: {},
            })
        );

        // Act
        await act(async () => {
            render(<TwoAncientPoems students={students} />);
        });
        const element = screen.getByTestId("STUDENT-answers");

        // Assert
        expect(element).toBeInTheDocument();
        expect(element).not.toBeEmptyDOMElement();
        expect(element).toHaveTextContent("ab, cd");
    });

    it("renders the students elapse field", async () => {
        // Arrange
        const students: Student[] = [
            {
                studentId: "STUDENT",
                englishName: "STUDENT",
                chineseName: "学生",
            },
        ];

        mockApi.mockImplementationOnce((quizId: string) =>
            Promise.resolve<AxiosResponse<Answer[]>>({
                data: [
                    {
                        studentId: "STUDENT",
                        quizId: quizId,
                        answers: ["a", "b", "c", "d", "e"],
                        arriveAt: new Date("2020-11-26T02:40:59.878269Z"),
                        completeAt: new Date("2020-11-26T02:52:15.384726Z"),
                    },
                ],
                status: 200,
                statusText: "OK",
                headers: {},
                config: {},
            })
        );

        // Act
        await act(async () => {
            render(<TwoAncientPoems students={students} />);
        });
        const element = screen.getByTestId("STUDENT-elapse");

        // Assert
        expect(element).not.toBeEmptyDOMElement();
        expect(element).toHaveTextContent("11:16");
    });

    it("renders the students elapse field with empty result", () => {
        // Arrange
        const students: Student[] = [
            {
                studentId: "STUDENT",
                englishName: "STUDENT",
                chineseName: "学生",
            },
        ];

        mockApi.mockImplementationOnce((quizId: string) =>
            Promise.resolve<AxiosResponse<Answer[]>>({
                data: [],
                status: 200,
                statusText: "OK",
                headers: {},
                config: {},
            })
        );

        // Act
        act(() => {
            render(<TwoAncientPoems students={students} />);
        });
        const element = screen.getByTestId("STUDENT-elapse");

        // Assert
        expect(element).toBeInTheDocument();
    });
})