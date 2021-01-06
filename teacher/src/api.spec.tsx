import axios, { AxiosResponse } from 'axios';
import api from './api';

it('should fire an api call', async () => {
    // Arrange
    var apiRequest = jest.spyOn(axios, 'request').mockImplementationOnce(async _ => 
        Promise.resolve({data: {}}));
    
    // Act
    await api.getAnswers("BabyWhiteCloud");
    
    // Assert
    expect(apiRequest).toHaveBeenCalledTimes(1);
    
    apiRequest.mockClear();
});

it('should call api with GET', async () => {
    // Arrange
    var apiRequest = jest.spyOn(axios, 'request').mockImplementationOnce(async _ => 
        Promise.resolve({data: {}}));
    
    // Act
    await api.getAnswers("BabyWhiteCloud");
    
    // Assert
    expect(apiRequest).toHaveBeenCalledWith(expect.objectContaining({'method': 'GET'}));
    
    apiRequest.mockClear();
});

it('should call api with correct url', async () => {
    // Arrange
    var apiRequest = jest.spyOn(axios, 'request').mockImplementationOnce(async _ => 
        Promise.resolve({data: {}}));
    
    // Act
    await api.getAnswers(`BabyWhiteCloud`);
    
    // Assert
    expect(apiRequest).toHaveBeenCalledWith(
      expect.objectContaining({
        url: `http://localhost:5000/api/quiz/BabyWhiteCloud`,
      })
    );

    apiRequest.mockClear();
});

it('should call api with application/json header', async () => {
    // Arrange
    var apiRequest = jest.spyOn(axios, 'request').mockImplementationOnce(async _ => 
        Promise.resolve({data: {}}));
    
    // Act
    await api.getAnswers("BabyWhiteCloud");
    
    // Assert
    expect(apiRequest).toHaveBeenCalledWith(expect.objectContaining({'headers': { 'Content-Type': "application/json"}}));

    apiRequest.mockClear();
});
