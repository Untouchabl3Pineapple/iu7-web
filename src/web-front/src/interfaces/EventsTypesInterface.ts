import axios from "axios";

axios.defaults.headers.common['Access-Control-Allow-Headers'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = '*';

export interface EventType {
    id: number,
    eventType: string
}

const client = axios.create({
    baseURL: 'http://localhost:5000/api/v1/eventstypes',
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
    getById(id: number) {
        return this.execute('get', `/${id}`)
    },

    deleteById(id: number) {
        return this.execute('delete', `/${id}`)
    },

    addEventType(eventType: string) {
        return this.execute('post', '/', {eventType});
    },
}