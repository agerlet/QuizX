import axios, { AxiosResponse} from "axios";
import config from './config.json';

interface Answer {
    studentId: string,
    answers: string[]
}

const getHeaders = () => {
    let headers : any = {
        "Content-Type": "application/json"
    };
    return headers;
};

export default {
    postAnswers(answer: Answer) : Promise<AxiosResponse<{answer: Answer}>> {
        return axios.request({
            url: `${config.serviceBaseUrl}/${config.quiz}`,
            method: 'POST',
            headers: getHeaders(),
            data: answer
        });
    } 
};