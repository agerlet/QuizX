import axios, { AxiosResponse } from 'axios';
import api from './api';

it('should post the answers', async () => {
    // Arrange
    const answer = {
        studentId: 'abcd123',
        answers: ['a', 'b', 'c', 'd', 'e']
    };
    const apiRequest = jest.spyOn(axios, 'request').mockImplementationOnce(async _ => 
        Promise.resolve({data: {answer: answer}}));
    
    // Act
    const result = await api.postAnswers(answer);
    
    // Assert
    expect(apiRequest).toHaveBeenCalledTimes(1);
    expect(apiRequest).toHaveBeenCalledWith(expect.objectContaining({
        'method': 'POST',
        'headers': {
            'Content-Type': 'application/json'
        },
        'url': 'https://localhost:5001/api/quiz'
    }));
    expect(result.data.answer).toStrictEqual(answer);
    
    apiRequest.mockRestore();
});