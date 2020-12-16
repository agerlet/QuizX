/// <reference types="react-scripts" />

interface Student {
    chineseName: string
    englishName: string
    studentId: string
}

interface Students {
    students: Student[]
}

interface Answer {
    studentId: string,
    quizId: string,
    answers: string[],
    arriveAt: Date,
    completeAt: Date | null
}