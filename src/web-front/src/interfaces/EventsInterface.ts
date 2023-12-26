import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

export interface Event {
    id: number,
    buttonEvent_ID: number,
    eventType_ID: number,
    eventDescription: string,
    detectingTime: Date,
    fixingTime: Date,
    timeUpdate: Date,
    user_Login: string,
}

const client = axios.create({
    baseURL: 'http://localhost:5000/api/v1/events',
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
    put(id: number, eventType_ID: number, eventDescription: string) {
        return this.execute('put', `/${id}`, {eventType_ID, eventDescription});
    },
}