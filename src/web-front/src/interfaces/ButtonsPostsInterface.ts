import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

export interface ButtonPost {
    post: number,
    leftSide: number,
    rightSide: number,
    leftColor: string,
    rightColor: string
}

const client = axios.create({
    baseURL: 'http://localhost:5000/api/v1/buttonsposts',
    validateStatus: function (status: number) {
        return status < 500;
    }
})

export default {
    execute(method: any, resource: any, data?: any) {
        return client({
            method,
            url: resource,
            data,
            headers: {}
        });
    },
    getAll() {
        return this.execute('get', '/');
    },
}