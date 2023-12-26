import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

export interface ButtonEvent {
    id: number,
    number: number,
    buttonColor: number
}

const client = axios.create({
    baseURL: 'http://localhost:5000/api/v1/buttonsevents',
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
    async getById(id: number) {
        return await this.execute('get', `/${id}`)
    },
    addButtonEvent(number: number, buttonColor: string) {
        return this.execute('post', '/', {number, buttonColor});
    },
}